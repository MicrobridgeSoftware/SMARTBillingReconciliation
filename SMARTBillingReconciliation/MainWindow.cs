using ApplicationSecurity;
using ApplicationSecurity.UI.Forms;
using SMARTBillingReconciliation.Exception;
using SMARTBillingReconciliation.Import;
using SMARTBillingReconciliation.Transaction;
using SMARTBillingReconciliation.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SMARTBillingReconciliation
{
    public partial class MainWindow : Telerik.WinControls.UI.RadForm
    {
        CurrentUserCredentials _credentials = new CurrentUserCredentials();
        ContractorManagementEntities dbContext = new ContractorManagementEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void mnuPerformRecon_Click(object sender, EventArgs e)
        {
            //FrmFileImport fileImport = new FrmFileImport();
            //fileImport.MdiParent = this;
            //fileImport.Show();
        }

        private void mnuException_Click(object sender, EventArgs e)
        {
            FrmExceptionList exceptionList = new FrmExceptionList();
            exceptionList.MdiParent = this;
            exceptionList.Show();
        }

        private void mnuFound_Click(object sender, EventArgs e)
        {
            FrmFoundTransactions foundTransactions = new FrmFoundTransactions();
            foundTransactions.MdiParent = this;
            foundTransactions.Show();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            UserLoginForm _login = new UserLoginForm(_credentials);
            _login.ShowDialog();

            ApplicationSecurityConstants._activeUser = _credentials._loggedInUserName();
        }

        private void mnuManualPresentation_Click(object sender, EventArgs e)
        {
            FrmManualPresentation manualPresentation = new FrmManualPresentation();
            manualPresentation.MdiParent = this;
            manualPresentation.Show();
        }

        private void mnuJobPayment_Click(object sender, EventArgs e)
        {
            FrmFileImport fileImport = new FrmFileImport();
            fileImport.MdiParent = this;
            fileImport.Show();
        }

        private void mnuClawback_Click(object sender, EventArgs e)
        {
            FrmClawBack clawBack = new FrmClawBack();
            clawBack.MdiParent = this;
            clawBack.Show();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult promptUser = RadMessageBox.Show("Are you sure you want to exit this Application?", Application.ProductName, MessageBoxButtons.YesNo);

                if (promptUser == DialogResult.No)
                    e.Cancel = true;
            }
        }
    }
}