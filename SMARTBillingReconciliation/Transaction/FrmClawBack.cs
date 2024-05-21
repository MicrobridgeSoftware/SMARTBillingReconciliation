using ApplicationSecurity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SMARTBillingReconciliation.Transaction
{
    public partial class FrmClawBack : Telerik.WinControls.UI.RadForm
    {
        ContractorManagementEntities dbContext = new ContractorManagementEntities();
        List<MonthOfTheYear> months;
        List<CM_Config> config;

        public FrmClawBack()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtTicketNo.Text.Trim().Equals(string.Empty))
                return;

            if (config[0].ReconKey == null || config[0].ReconKey.Trim() == string.Empty)
            {
                RadMessageBox.Show("No key found to perform this recon. Contact system administrator!", Application.ProductName);
                return;
            }

            if (!config[0].ReconKey.Trim().ToUpper().Equals("TICKET NO") && !config[0].ReconKey.Trim().ToUpper().Equals("SERVICE ORDER NO"))
            {
                RadMessageBox.Show("Invalid key supplied!", Application.ProductName);
                return;
            }

            if (ddlPeriod.SelectedValue == null || Convert.ToInt32(ddlPeriod.SelectedValue) == 0)
            {
                RadMessageBox.Show("A period is required to continue!", Application.ProductName);
                return;
            }

            int monthId = Convert.ToInt32(ddlPeriod.SelectedValue);
            string ticketNumber = txtTicketNo.Text.Trim().ToUpper();
            List<BillingTransaction> getTicketRecord = new List<BillingTransaction>();

            if (config[0].ReconKey.Trim().ToUpper().Equals("TICKET NO"))
                getTicketRecord = dbContext.BillingTransactions.Where(x => x.TicketNo.Trim().ToUpper().Equals(ticketNumber) && x.Jobdate.Month == monthId).AsNoTracking().ToList();
            else if (config[0].ReconKey.Trim().ToUpper().Equals("SERVICE ORDER NO"))
                getTicketRecord = dbContext.BillingTransactions.Where(x => x.ServiceOrderNo.Trim().ToUpper().Equals(ticketNumber) && x.Jobdate.Month == monthId).AsNoTracking().ToList();

            if (getTicketRecord.Count <= 0)
                RadMessageBox.Show("No data found to match supplied criteria!", Application.ProductName);
            else
            {
                DialogResult promptUser = RadMessageBox.Show("Are you sure you want to perform claw back on this transaction?", Application.ProductName, MessageBoxButtons.YesNo);

                if (promptUser == DialogResult.Yes)
                {
                    DateTime processed = DateTime.Now;

                    foreach (BillingTransaction transaction in getTicketRecord)
                    {
                        var getTransactionRecord = dbContext.BillingTransactions.Where(x => x.BillingTransactionId == transaction.BillingTransactionId).FirstOrDefault();
                        
                        getTransactionRecord.Presented = true;
                        getTransactionRecord.DatePresented = processed;
                        getTransactionRecord.DateProcessed = processed;
                        getTransactionRecord.ProcessedBy = ApplicationSecurityConstants._activeUser;
                        getTransactionRecord.ClawedBack = true;
                        getTransactionRecord.DateClawedBack = processed;
                        getTransactionRecord.Status = "Found";

                        if (getTransactionRecord.Comment == null)
                            getTransactionRecord.Comment = "Clawed back";
                        else
                            getTransactionRecord.Comment = getTransactionRecord.Comment.Trim() + ". Clawed back";
                    }

                    dbContext.SaveChanges();
                    RadMessageBox.Show("Transaction(s) updated successfully", Application.ProductName);
                }
            }
        }

        private void FrmClawBack_Load(object sender, EventArgs e)
        {
            months = new List<MonthOfTheYear>();
            config = new List<CM_Config>();

            config = dbContext.CM_Config.AsNoTracking().ToList();
            months = dbContext.MonthOfTheYears.AsNoTracking().ToList();
            bindingSourceMonth.DataSource = months;

            ddlPeriod.SelectedIndex = -1;
            ddlPeriod.DropDownListElement.AutoCompleteAppend.LimitToList = true;

            if (config[0].ReconKey != null && config[0].ReconKey.Trim() != string.Empty)
                radLabel1.Text = config[0].ReconKey.Trim();
        }
    }
}