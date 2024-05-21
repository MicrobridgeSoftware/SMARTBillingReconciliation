using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SMARTBillingReconciliation.Transaction
{
    public partial class FrmTicketRecordDisplay : Telerik.WinControls.UI.RadForm
    {
        private List<FoundReconTransaction> foundTransactionList;

        public FrmTicketRecordDisplay()
        {
            InitializeComponent();
        }

        public FrmTicketRecordDisplay(List<FoundReconTransaction> foundTransactionList) : this()
        {
            this.foundTransactionList = foundTransactionList;
        }

        private void grdDataDisplay_CommandCellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            
        }

        private void FrmTicketRecordDisplay_Load(object sender, EventArgs e)
        {
            grdDataDisplay.DataSource = foundTransactionList;

            GridViewSummaryItem summaryItem = new GridViewSummaryItem("Variance", "{0:C}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItem1 = new GridViewSummaryItem("JobValue", "{0:C}", GridAggregateFunction.Sum);
            GridViewSummaryItem summaryItem2 = new GridViewSummaryItem("FlowValue", "{0:C}", GridAggregateFunction.Sum);
            GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
            summaryRowItem.Add(summaryItem);
            summaryRowItem.Add(summaryItem1);
            summaryRowItem.Add(summaryItem2);
            this.grdDataDisplay.SummaryRowsBottom.Add(summaryRowItem);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (grdDataDisplay.Rows.Count <= 0)
                return;

            string invoiceExportFolder = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Found Transactions Export";
            bool _dataBaseFolderExist = Directory.Exists(invoiceExportFolder);

            if (!_dataBaseFolderExist)
                Directory.CreateDirectory(invoiceExportFolder);

            string fullpath = invoiceExportFolder + "\\" + "found transactions" + ".xlsx";
            GridViewSpreadExport spreadExporter = new GridViewSpreadExport(this.grdDataDisplay);
            SpreadExportRenderer exportRenderer = new SpreadExportRenderer();

            spreadExporter.HiddenColumnOption = HiddenOption.DoNotExport;
            spreadExporter.SheetName = "found transactions";
            spreadExporter.ExportVisualSettings = true;
            spreadExporter.FileExportMode = FileExportMode.CreateOrOverrideFile;

            spreadExporter.RunExport(fullpath, exportRenderer);

            RadMessageBox.Show("File exported successfully!", Application.ProductName);
        }

        private void grdDataDisplay_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                e.CellElement.ForeColor = Color.Red;
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                e.CellElement.Font = new Font("Segoe UI", 8.0f, FontStyle.Bold);
            }
        }
    }
}
