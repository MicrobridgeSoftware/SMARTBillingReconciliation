using SMARTBillingReconciliation.ApplicationUtil;
using SMARTBillingReconciliation.Transaction;
using SMARTBillingReconciliation.Views;
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

namespace SMARTBillingReconciliation.Exception
{
    public partial class FrmExceptionList : Telerik.WinControls.UI.RadForm
    {
        ContractorManagementEntities dbContext = new ContractorManagementEntities();
        List<MonthOfTheYear> months;
        List<ExceptionListDefinition> transactionList;
        List<FoundReconTransaction> foundTransactionList;

        public FrmExceptionList()
        {
            InitializeComponent();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            //if (maskYear.Text.Trim().Replace("_", "") == string.Empty || maskYear.Text.Trim().Replace("_", "").Length != 4)
            //{
            //    RadMessageBox.Show("Invalid year detected!", Application.ProductName);
            //    return;
            //}

            //if (ddlPeriod.SelectedValue == null || Convert.ToInt32(ddlPeriod.SelectedValue) == 0)
            //{
            //    RadMessageBox.Show("Period is a required field!", Application.ProductName);
            //    return;
            //}

            transactionList = new List<ExceptionListDefinition>();
            foundTransactionList = new List<FoundReconTransaction>();
            //int monthId = Convert.ToInt32(ddlPeriod.SelectedValue);
            //int year = Convert.ToInt32(maskYear.Text);

            if (rdbtnNotFound.IsChecked)
            {
                var getUnfoundItems = dbContext.GetUnfoundImports().ToList();

                foreach (var item in getUnfoundItems)
                    transactionList.Add(new ExceptionListDefinition { fileName = item.FileName, region = item.Location, ticketNo = item.AccountNo, transactionDate = Convert.ToDateTime(item.ValueDate), transactionValue = item.TransactionValue });
            }
            else if (rdbtnRegion.IsChecked)
            {
                var getVariantRegionItems = dbContext.GetVariantRegionImports().ToList();

                foreach (var item in getVariantRegionItems)
                    transactionList.Add(new ExceptionListDefinition { fileName = item.FileName, region = item.Location, ticketNo = item.AccountNo, transactionDate = Convert.ToDateTime(item.ValueDate), transactionValue = item.TransactionValue });
            }
            else if (rdbtnValue.IsChecked)
            {
                var getDuplicatedImports = dbContext.GetDuplicateImports().ToList();

                foreach (var item in getDuplicatedImports)
                    transactionList.Add(new ExceptionListDefinition { fileName = item.FileName, region = item.Location, ticketNo = item.AccountNo, transactionDate = Convert.ToDateTime(item.ValueDate), transactionValue = item.TransactionValue });
            }
            else if (rdBtnFound.IsChecked)            
                foundTransactionList = dbContext.FoundReconTransactions.AsNoTracking().ToList();
            

            if (transactionList.Count > 0)
            {
                FrmExceptionDisplay display = new FrmExceptionDisplay(transactionList);
                display.ShowDialog();
            }
            else if (foundTransactionList.Count > 0)
            {
                FrmTicketRecordDisplay recordDisplay = new FrmTicketRecordDisplay(foundTransactionList);
                recordDisplay.ShowDialog();
            }
            else
                RadMessageBox.Show("No Transaction(s) found!", Application.ProductName);
        }

        private void FrmExceptionList_Load(object sender, EventArgs e)
        {
            months = new List<MonthOfTheYear>();
            months = dbContext.MonthOfTheYears.AsNoTracking().ToList();

            bindingSourcePeriod.DataSource = months;

            ddlPeriod.SelectedIndex = -1;
            ddlPeriod.DropDownListElement.AutoCompleteAppend.LimitToList = true;
        }
    }
}