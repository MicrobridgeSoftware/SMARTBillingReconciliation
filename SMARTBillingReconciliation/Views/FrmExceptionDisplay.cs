using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SMARTBillingReconciliation.ApplicationUtil;
using Telerik.WinControls;
using Telerik.WinControls.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using System.Linq;
using System.Data.Entity;

namespace SMARTBillingReconciliation.Views
{
    public partial class FrmExceptionDisplay : Telerik.WinControls.UI.RadForm
    {
        private List<ExceptionListDefinition> transactionList;
        ContractorManagementEntities dbContext = new ContractorManagementEntities();

        public FrmExceptionDisplay()
        {
            InitializeComponent();
        }
        
        public FrmExceptionDisplay(List<ExceptionListDefinition> transactionList) : this()
        {
            this.transactionList = transactionList;
        }

        private void FrmExceptionDisplay_Load(object sender, EventArgs e)
        {
            List<string> serviceOrderList = new List<string>();
            serviceOrderList = transactionList.Select(x => x.ticketNo).Distinct().ToList();

            var getRelatedAccNo = dbContext.BillingTransactions.Where(x => serviceOrderList.Contains(x.ServiceOrderNo)).AsNoTracking().ToList();

            grdDataDisplay.DataSource = transactionList;

            for(int i = 0; i<grdDataDisplay.Rows.Count; i++)
            {
                var soNo = grdDataDisplay.Rows[i].Cells["TicketNo"].Value;

                if (soNo != null && soNo.ToString() != string.Empty)
                {
                    string serviceOrderNo = soNo.ToString().Trim().ToUpper();
                    var getAccNo = getRelatedAccNo.Where(x => x.ServiceOrderNo.Trim().ToUpper().Equals(serviceOrderNo)).FirstOrDefault();

                    if (getAccNo != null)
                        grdDataDisplay.Rows[i].Cells["AccountNo"].Value = getAccNo.TicketNo.Trim();
                }
            }           

            GridViewSummaryItem summaryItem = new GridViewSummaryItem("TransactionValue", "{0:C}", GridAggregateFunction.Sum);
            GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
            summaryRowItem.Add(summaryItem);
            this.grdDataDisplay.SummaryRowsBottom.Add(summaryRowItem);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (grdDataDisplay.Rows.Count <= 0)
                return;
            
            string invoiceExportFolder = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Exported Exceptions";
            bool _dataBaseFolderExist = Directory.Exists(invoiceExportFolder);

            if (!_dataBaseFolderExist)
                Directory.CreateDirectory(invoiceExportFolder);

            string fullpath = invoiceExportFolder + "\\" + "exception export" + ".xlsx";
            GridViewSpreadExport spreadExporter = new GridViewSpreadExport(this.grdDataDisplay);
            SpreadExportRenderer exportRenderer = new SpreadExportRenderer();

            spreadExporter.HiddenColumnOption = HiddenOption.DoNotExport;
            spreadExporter.SheetName = "exception export";
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