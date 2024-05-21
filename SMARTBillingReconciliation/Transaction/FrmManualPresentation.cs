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
    public partial class FrmManualPresentation : Telerik.WinControls.UI.RadForm
    {
        ContractorManagementEntities dbContext = new ContractorManagementEntities();
        List<BillingTransaction> getTicketRecord;
        List<CM_Config> config;

        public FrmManualPresentation()
        {
            InitializeComponent();
        }

        private void FrmManualPresentation_Load(object sender, EventArgs e)
        {
            config = new List<CM_Config>();
            config = dbContext.CM_Config.AsNoTracking().ToList();

            if (config[0].ReconKey != null || config[0].ReconKey.Trim() != string.Empty)
                radLabel1.Text = config[0].ReconKey.Trim();
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

            string ticketNumber = txtTicketNo.Text.Trim().ToUpper();
            string category = rdBtnJobClaims.IsChecked ? "Job Claims" : "Survey 123";
            getTicketRecord = new List<BillingTransaction>();

            if (config[0].ReconKey.Trim().ToUpper().Equals("TICKET NO"))
                getTicketRecord = dbContext.BillingTransactions.Where(x => x.TicketNo.Trim().ToUpper().Equals(ticketNumber) && x.ServiceCategory.Trim().Equals("category")).AsNoTracking().ToList();
            else if (config[0].ReconKey.Trim().ToUpper().Equals("SERVICE ORDER NO"))
                getTicketRecord = dbContext.BillingTransactions.Where(x => x.ServiceOrderNo.Trim().ToUpper().Equals(ticketNumber) && x.ServiceCategory.Trim().Equals("category")).AsNoTracking().ToList();

            //getTicketRecord = dbContext.BillingTransactions.Where(x => x.TicketNo.Trim().ToUpper().Equals(ticketNumber) && x.ServiceCategory.Trim().Equals("category")).AsNoTracking().ToList();
            //getTicketRecord = dbContext.BillingTransactions.Where(x => x.ServiceOrderNo.Trim().ToUpper().Equals(ticketNumber) && x.ServiceCategory.Trim().Equals(category)).AsNoTracking().ToList();

            if (getTicketRecord.Count <= 0)
                RadMessageBox.Show("No data found to match supplied criteria!", Application.ProductName);
            else
            {
                DialogResult promptUser = RadMessageBox.Show("Are you sure you want to manually update this transaction status?", Application.ProductName, MessageBoxButtons.YesNo);

                if (promptUser == DialogResult.Yes)
                {
                    foreach (BillingTransaction transaction in getTicketRecord)
                    {
                        var getTransactionRecord = dbContext.BillingTransactions.Where(x => x.BillingTransactionId == transaction.BillingTransactionId).FirstOrDefault();

                        getTransactionRecord.Presented = true;
                        getTransactionRecord.DatePresented = DateTime.Now;
                        getTransactionRecord.DateProcessed = DateTime.Now;
                        getTransactionRecord.ProcessedBy = ApplicationSecurityConstants._activeUser;
                        getTransactionRecord.Status = null;
                        getTransactionRecord.Comment = null;
                        getTransactionRecord.FileName = "Manual Entry";
                    }

                    dbContext.SaveChanges();
                    RadMessageBox.Show("Transaction(s) updated successfully", Application.ProductName);
                }
            }            
        }
    }
}