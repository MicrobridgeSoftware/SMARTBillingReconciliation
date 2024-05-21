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
    public partial class FrmWaitDialogue : Telerik.WinControls.UI.RadForm
    {
        public FrmWaitDialogue()
        {
            InitializeComponent();
        }

        private void FrmWaitDialogue_Load(object sender, EventArgs e)
        {
            radWaitingBar.StartWaiting();
        }
    }
}