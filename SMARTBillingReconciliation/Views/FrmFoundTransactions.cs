using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using Excel = Microsoft.Office.Interop.Excel;

namespace SMARTBillingReconciliation.Views
{
    public partial class FrmFoundTransactions : Telerik.WinControls.UI.RadForm
    {
        ContractorManagementEntities dbContext = new ContractorManagementEntities();
        List<MonthOfTheYear> months;
        List<BillingReconStatusView> transactionList;
        List<RegionView> regions;

        public FrmFoundTransactions()
        {
            InitializeComponent();
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
            spreadExporter.SheetName = "Reconciled transactions";
            spreadExporter.ExportVisualSettings = true;
            spreadExporter.FileExportMode = FileExportMode.CreateOrOverrideFile;

            spreadExporter.RunExport(fullpath, exportRenderer);

            RadMessageBox.Show("File exported successfully!", Application.ProductName);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (maskYear.Text.Trim().Replace("_", "") == string.Empty || maskYear.Text.Trim().Replace("_", "").Length != 4)
            {
                RadMessageBox.Show("Invalid year detected!", Application.ProductName);
                return;
            }

            if (ddlPeriod.SelectedValue == null || Convert.ToInt32(ddlPeriod.SelectedValue) == 0)
            {
                RadMessageBox.Show("Period is a required field!", Application.ProductName);
                return;
            }

            try
            {
                if (!bgwFetchTransactions.IsBusy)
                {
                    FrmWaitDialogue waitDialogue = new FrmWaitDialogue();

                    int monthId = Convert.ToInt32(ddlPeriod.SelectedValue);
                    int year = Convert.ToInt32(maskYear.Text);

                    List<object> bgwargument = new List<object>();
                    bgwargument.Add(monthId);
                    bgwargument.Add(year);
                    bgwargument.Add("Records");                    

                    bgwFetchTransactions.RunWorkerAsync(bgwargument);
                    waitDialogue.ShowDialog();
                }
            }
            catch //(Exception _exp)
            {
                //RadMessageBox.Show(_exp.InnerException == null ? _exp.Message : _exp.InnerException.Message);
            }

            //transactionList = new List<BillingReconStatusView>();
            //int monthId = Convert.ToInt32(ddlPeriod.SelectedValue);
            //int year = Convert.ToInt32(maskYear.Text);
            //List<int> jobIdList = new List<int>();

            //transactionList = dbContext.BillingReconStatusViews.Where(x => x.UnconvertedJobDate.Month == monthId && x.UnconvertedJobDate.Year == year).AsNoTracking().ToList();
            //jobIdList = transactionList.Select(x => x.JobLogId).Distinct().ToList();
            //var getDetailList = dbContext.BillingTransactionsListingViews.Where(x => jobIdList.Contains(x.JobLogId)).AsNoTracking().ToList();

            //grdDataDisplay.DataSource = transactionList;                      

            //bindingSourceJobDetails.DataSource = getDetailList;
        }

        private void FrmFoundTransactions_Load(object sender, EventArgs e)
        {
            months = new List<MonthOfTheYear>();
            regions = new List<RegionView>();
            months = dbContext.MonthOfTheYears.AsNoTracking().ToList();
            regions = dbContext.RegionViews.AsNoTracking().ToList();
            regions.Add(new RegionView { CompanyId = 0, Description = "ALL", Region = "ALL", RegionDescription = "ALL", RegionId = 0 });

            bindingSourcePeriod.DataSource = months;
            bindingSourceRegion.DataSource = regions.OrderBy(x=> x.RegionId);

            ddlPeriod.SelectedIndex = -1;
            ddlRegion.SelectedIndex = -1;
            ddlPeriod.DropDownListElement.AutoCompleteAppend.LimitToList = true;
            ddlRegion.DropDownListElement.AutoCompleteAppend.LimitToList = true;

            GroupDescriptor groupDescriptor = new GroupDescriptor();
            groupDescriptor.GroupNames.Add("Presented", ListSortDirection.Descending);
            this.grdDataDisplay.GroupDescriptors.Add(groupDescriptor);
            grdDataDisplay.MasterTemplate.ExpandAllGroups();

            grdDataDisplay.MasterTemplate.ShowTotals = true;
            GridViewSummaryItem summaryItem = new GridViewSummaryItem("Value", "{0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
            summaryRowItem.Add(summaryItem);
            this.grdDataDisplay.SummaryRowsBottom.Add(summaryRowItem);
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

        private void performShortPaymentRecon(List<BillingTransaction> transactionList, int regionId)
        {
            Excel.Application _excelApp;
            Excel.Workbooks _excelWorkbooks;
            Excel.Workbook _excelWorkbook;
            object _misValue = System.Reflection.Missing.Value;            
            List<CM_Config> _configDetails = new List<CM_Config>();
            List<int> shortPayJobIds = new List<int>();

            shortPayJobIds = transactionList.Select(x => x.JobLogId).ToList();
            dbContext.Database.CommandTimeout = 0;

            var jobLogList = dbContext.ClientBillingInfoViews.Where(x => shortPayJobIds.Contains(x.JobLogId) && x.LeadContractor == 1).AsNoTracking().ToList();
            _configDetails = dbContext.CM_Config.ToList();

            _excelApp = new Microsoft.Office.Interop.Excel.Application();
            string _applicationFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            string _defaultFolderPath = _applicationFolderPath + "FlowDiaryTemplates";
            string _diaryFileName = _defaultFolderPath + "\\" + "Part Paid Transactions";
            string diary = "Part Paid Transactions";

            _excelWorkbooks = _excelApp.Workbooks;

            _excelWorkbook = _excelWorkbooks.Open(_diaryFileName, _misValue, _misValue, _misValue, _misValue,
                             _misValue, _misValue, _misValue, _misValue, _misValue, _misValue, _misValue,
                             _misValue, _misValue, _misValue);

            Excel.Worksheet _myExcelWorksheet = (Excel.Worksheet)_excelWorkbook.ActiveSheet;
            int _startRow = 14;
            int _headerRow = 14;
            _excelApp.ScreenUpdating = false;
            
            var _jobInformation = jobLogList.ToList();

            if (!_configDetails[0].BillingExportOrder.Trim().Equals("TicketNo"))
                _jobInformation = _jobInformation.OrderBy(x=> x.Region).ThenBy(x => x.Contractor).ThenBy(x => x.JobDate).ToList();
            else
                _jobInformation = _jobInformation.OrderBy(x=> x.Region).ThenBy(x => x.TicketNo).ThenBy(x => x.JobDate).ToList();

            string columnEheaderText = _myExcelWorksheet.get_Range("E" + _headerRow.ToString(), _misValue).Value2;

            List<string> exemptedDutyCodes = new List<string>();
            exemptedDutyCodes.Add("123");
            exemptedDutyCodes.Add("123I");
            exemptedDutyCodes.Add("123R");

            List<string> distinctAccountNos = new List<string>();
            List<PaidTransaction> paymentInfo = new List<PaidTransaction>();
            distinctAccountNos = _jobInformation.Where(x => !exemptedDutyCodes.Contains(x.DutyCode.Trim())).Select(x => x.TicketNo).Distinct().ToList();
            paymentInfo = dbContext.PaidTransactions.Where(x => distinctAccountNos.Contains(x.AccountNo)).AsNoTracking().ToList();

            foreach (string accountNo in distinctAccountNos)
            {
                var _job = _jobInformation.Where(x => x.TicketNo == accountNo && !exemptedDutyCodes.Contains(x.DutyCode.Trim())).FirstOrDefault();
                var _payment = paymentInfo.Where(x => x.AccountNo == accountNo).FirstOrDefault();

                if (_job != null)
                {
                    _startRow++;

                    decimal paid = _payment == null ? 0 : Convert.ToDecimal(_payment.PaidAmount);
                    decimal value = _payment == null ? 0 : Convert.ToDecimal(_payment.JobValue);
                    decimal balance = _payment == null ? 0 : (Convert.ToDecimal(_payment.JobValue) - Convert.ToDecimal(_payment.PaidAmount));

                    _myExcelWorksheet.get_Range("A" + _startRow.ToString(), _misValue).Formula = _job.Region.Trim().ToUpper(); //ddlRegion.Text.Trim();
                    _myExcelWorksheet.get_Range("B" + _startRow.ToString(), _misValue).Formula = _job.JobDate.ToString("MM/dd/yyyy");
                    _myExcelWorksheet.get_Range("C" + _startRow.ToString(), _misValue).Formula = _job.Contractor.Trim();
                    _myExcelWorksheet.get_Range("D" + _startRow.ToString(), _misValue).Formula = _job.ContractorNo.Trim();

                    if (columnEheaderText.Trim().Contains("SERVICE"))
                        _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.ServiceOrderNo.Trim();
                    else if (columnEheaderText.Trim().Contains("TICKET"))
                        _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.ServiceOrderNo.Trim();
                    else if (columnEheaderText.Trim().Contains("INCIDENT"))
                        _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.IncidentNo.Trim();

                    _myExcelWorksheet.get_Range("F" + _startRow.ToString(), _misValue).Formula = _job.TelephoneNo.Trim().Replace("-", "").Replace("_", "");
                    _myExcelWorksheet.get_Range("G" + _startRow.ToString(), _misValue).Formula = _job.TicketNo.Trim();
                    _myExcelWorksheet.get_Range("H" + _startRow.ToString(), _misValue).Formula = _job.SiteAddress.Trim();
                    _myExcelWorksheet.get_Range("J" + _startRow.ToString(), _misValue).Formula = _job.JobDescription.Trim();

                    _myExcelWorksheet.get_Range("P" + _startRow.ToString(), _misValue).Formula = "Part Paid";

                    _myExcelWorksheet.get_Range("Q" + _startRow.ToString(), _misValue).Formula = Math.Round(Convert.ToDecimal(value), 2).ToString();

                    _myExcelWorksheet.get_Range("R" + _startRow.ToString(), _misValue).Formula = Math.Round(Convert.ToDecimal(paid), 2).ToString();
                    _myExcelWorksheet.get_Range("S" + _startRow.ToString(), _misValue).Formula = Math.Round(Convert.ToDecimal(balance), 2).ToString();

                    var getDutyCount = _jobInformation.Where(x => x.TicketNo == accountNo && !exemptedDutyCodes.Contains(x.DutyCode.Trim())).ToList();

                    if (getDutyCount.Count > 1)
                    {
                        StringBuilder builder = new StringBuilder();

                        foreach (var duty in getDutyCount)
                            builder.Append(duty.DutyCode.Trim() + "-" + Math.Round(Convert.ToDecimal(duty.Amount), 2).ToString() + ",");

                        string performedDuies = builder.ToString().Substring(0, builder.ToString().Length - 1);

                        _myExcelWorksheet.get_Range("T" + _startRow.ToString(), _misValue).Formula = performedDuies;
                    }
                    else
                        _myExcelWorksheet.get_Range("T" + _startRow.ToString(), _misValue).Formula = _job.DutyCode.Trim();
                }
            }

            if (_jobInformation.Count > 0)
            {
                _excelApp.ScreenUpdating = true;

                string invoiceExportFolder = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Exported Invoices";
                bool _dataBaseFolderExist = Directory.Exists(invoiceExportFolder);

                if (!_dataBaseFolderExist)
                    Directory.CreateDirectory(invoiceExportFolder);

                string _path = invoiceExportFolder;
                string _regionName = _jobInformation[0].Region.Trim();
                string currentTime = DateTime.Now.ToString("_HH_mm_ss");

                _myExcelWorksheet.SaveAs(_path + "\\" + _regionName + "-" + diary.Trim() + currentTime + ".xlsx", _misValue, _misValue, _misValue,
                                        _misValue, _misValue, _misValue, _misValue, _misValue, _misValue);

                _excelWorkbooks.Close();
                _excelApp.Quit();

                Marshal.ReleaseComObject(_excelWorkbooks);
                Marshal.ReleaseComObject(_excelWorkbooks);
                Marshal.ReleaseComObject(_excelApp);
            }
        }

        private void performNewFormatRecon(List<BillingTransaction> transactionList, int regionId)
        {
            Excel.Application _excelApp;
            Excel.Workbooks _excelWorkbooks;
            Excel.Workbook _excelWorkbook;
            object _misValue = System.Reflection.Missing.Value;
            List<int> JobIds = new List<int>();
            List<CM_Config> _configDetails = new List<CM_Config>();
            //List<int> shortPayJobIds = new List<int>();

            var getDistinctDiaries = transactionList.Select(x => x.DiaryTemplateId).Distinct().ToList();
            JobIds = transactionList.Select(x => x.JobLogId).ToList();
            //shortPayJobIds = transactionList.Where(x => x.Status != null && x.Comment != null && x.Status.Trim().Equals("Found") && x.Comment.Trim().Equals("Under Paid")).Select(x => x.JobLogId).ToList();

            dbContext.Database.CommandTimeout = 0;

            var jobLogList = dbContext.ClientBillingInfoViews.Where(x => JobIds.Contains(x.JobLogId) && x.LeadContractor == 1).AsNoTracking().ToList();
            _configDetails = dbContext.CM_Config.ToList();

            foreach (var diaryId in getDistinctDiaries)
            {
                var getDiaryInfo = dbContext.CM_DiaryTemplate.Where(x => x.DiaryTemplateId == diaryId).FirstOrDefault();

                _excelApp = new Microsoft.Office.Interop.Excel.Application();
                string _applicationFolderPath = AppDomain.CurrentDomain.BaseDirectory;
                string _defaultFolderPath = _applicationFolderPath + "FlowDiaryTemplates";
                string _diaryFileName = _defaultFolderPath + "\\" + getDiaryInfo.Description.Trim();
                string diary = getDiaryInfo.Description.Trim();
                
                _excelWorkbooks = _excelApp.Workbooks;

                _excelWorkbook = _excelWorkbooks.Open(_diaryFileName, _misValue, _misValue, _misValue, _misValue,
                                 _misValue, _misValue, _misValue, _misValue, _misValue, _misValue, _misValue,
                                 _misValue, _misValue, _misValue);

                Excel.Worksheet _myExcelWorksheet = (Excel.Worksheet)_excelWorkbook.ActiveSheet;
                int _startRow = 14;
                int _headerRow = 14;
                _excelApp.ScreenUpdating = false;

                var _jobInformation = jobLogList.Where(x => x.DiaryTemplateId == diaryId).ToList();

                if (!_configDetails[0].BillingExportOrder.Trim().Equals("TicketNo"))
                    _jobInformation = _jobInformation.OrderBy(x => x.Contractor).ThenBy(x => x.JobDate).ToList();
                else
                    _jobInformation = _jobInformation.OrderBy(x=> x.Region).ThenBy(x => x.TicketNo).ThenBy(x => x.JobDate).ToList();

                string columnEheaderText = _myExcelWorksheet.get_Range("E" + _headerRow.ToString(), _misValue).Value2;

                List<string> exemptedDutyCodes = new List<string>();
                exemptedDutyCodes.Add("123");
                exemptedDutyCodes.Add("123I");
                exemptedDutyCodes.Add("123R");

                //List<string> distinctAccountNos = new List<string>();
                //List<PaidTransaction> paymentInfo = new List<PaidTransaction>();
                //distinctAccountNos = _jobInformation.Where(x => shortPayJobIds.Contains(x.JobLogId) && !exemptedDutyCodes.Contains(x.DutyCode.Trim())).Select(x => x.TicketNo).Distinct().ToList();
                //paymentInfo = dbContext.PaidTransactions.Where(x => distinctAccountNos.Contains(x.AccountNo)).AsNoTracking().ToList();
                
                //foreach (string accountNo in distinctAccountNos)
                //{                    
                //    var _job = _jobInformation.Where(x => x.TicketNo == accountNo && !exemptedDutyCodes.Contains(x.DutyCode.Trim())).FirstOrDefault();
                //    var _payment = paymentInfo.Where(x => x.AccountNo == accountNo).FirstOrDefault();

                //    if (_job != null)
                //    {
                //        _startRow++;

                //        decimal paid = _payment == null ? 0 : Convert.ToDecimal(_payment.PaidAmount);
                //        decimal value = _payment == null ? 0 : Convert.ToDecimal(_payment.JobValue);
                //        decimal balance = _payment == null ? 0 : (Convert.ToDecimal(_payment.JobValue) - Convert.ToDecimal(_payment.PaidAmount));

                //        _myExcelWorksheet.get_Range("A" + _startRow.ToString(), _misValue).Formula = ddlRegion.Text.Trim();
                //        _myExcelWorksheet.get_Range("B" + _startRow.ToString(), _misValue).Formula = _job.JobDate.ToString("MM/dd/yyyy");
                //        _myExcelWorksheet.get_Range("C" + _startRow.ToString(), _misValue).Formula = _job.Contractor.Trim();
                //        _myExcelWorksheet.get_Range("D" + _startRow.ToString(), _misValue).Formula = _job.ContractorNo.Trim();

                //        if (columnEheaderText.Trim().Contains("SERVICE"))
                //            _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.ServiceOrderNo.Trim();
                //        else if (columnEheaderText.Trim().Contains("TICKET"))
                //            _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.ServiceOrderNo.Trim(); //_job.TicketNo.Trim();
                //        else if (columnEheaderText.Trim().Contains("INCIDENT"))
                //            _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.IncidentNo.Trim();

                //        _myExcelWorksheet.get_Range("F" + _startRow.ToString(), _misValue).Formula = _job.TelephoneNo.Trim().Replace("-", "").Replace("_", "");
                //        _myExcelWorksheet.get_Range("G" + _startRow.ToString(), _misValue).Formula = _job.TicketNo.Trim(); //_job.CustomerAccNo.Trim();
                //        _myExcelWorksheet.get_Range("H" + _startRow.ToString(), _misValue).Formula = _job.SiteAddress.Trim();
                //        _myExcelWorksheet.get_Range("J" + _startRow.ToString(), _misValue).Formula = _job.JobDescription.Trim();

                //        _myExcelWorksheet.get_Range("P" + _startRow.ToString(), _misValue).Formula = "Part Paid";

                //        _myExcelWorksheet.get_Range("Q" + _startRow.ToString(), _misValue).Formula = Math.Round(Convert.ToDecimal(value), 2).ToString();

                //        _myExcelWorksheet.get_Range("R" + _startRow.ToString(), _misValue).Formula = Math.Round(Convert.ToDecimal(paid), 2).ToString();
                //        _myExcelWorksheet.get_Range("S" + _startRow.ToString(), _misValue).Formula = Math.Round(Convert.ToDecimal(balance), 2).ToString();

                //        //var getDutyCount = _jobInformation.Where(x => x.TicketNo == accountNo && !exemptedDutyCodes.Contains(x.DutyCode)).Select(x => x.DutyCode).Distinct().ToList();
                //        var getDutyCount = _jobInformation.Where(x => x.TicketNo == accountNo && !exemptedDutyCodes.Contains(x.DutyCode.Trim())).ToList();

                //        if (getDutyCount.Count > 1)
                //        {
                //            StringBuilder builder = new StringBuilder();

                //            foreach (var duty in getDutyCount)
                //                builder.Append(duty.DutyCode.Trim() + "-" + Math.Round(Convert.ToDecimal(duty.Amount), 2).ToString() + ",");

                //            string performedDuies = builder.ToString().Substring(0, builder.ToString().Length - 1);

                //            _myExcelWorksheet.get_Range("T" + _startRow.ToString(), _misValue).Formula = performedDuies;
                //        }
                //        else
                //            _myExcelWorksheet.get_Range("T" + _startRow.ToString(), _misValue).Formula = _job.DutyCode.Trim();
                //    }
                //}

                foreach (var _job in _jobInformation)//.Where(x => !shortPayJobIds.Contains(x.JobLogId)))
                {
                    _startRow++;
                    decimal defaultPaidAmt = 0;

                    _myExcelWorksheet.get_Range("A" + _startRow.ToString(), _misValue).Formula = _job.Region.Trim().ToUpper();// ddlRegion.Text.Trim();
                    _myExcelWorksheet.get_Range("B" + _startRow.ToString(), _misValue).Formula = _job.JobDate.ToString("MM/dd/yyyy");
                    _myExcelWorksheet.get_Range("C" + _startRow.ToString(), _misValue).Formula = _job.Contractor.Trim();
                    _myExcelWorksheet.get_Range("D" + _startRow.ToString(), _misValue).Formula = _job.ContractorNo.Trim();

                    if (columnEheaderText.Trim().Contains("SERVICE"))
                        _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.ServiceOrderNo.Trim();
                    else if (columnEheaderText.Trim().Contains("TICKET"))
                        _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.ServiceOrderNo.Trim(); //_job.TicketNo.Trim();
                    else if (columnEheaderText.Trim().Contains("INCIDENT"))
                        _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.IncidentNo.Trim();

                    _myExcelWorksheet.get_Range("F" + _startRow.ToString(), _misValue).Formula = _job.TelephoneNo.Trim().Replace("-", "").Replace("_", "");
                    _myExcelWorksheet.get_Range("G" + _startRow.ToString(), _misValue).Formula = _job.TicketNo.Trim(); //_job.CustomerAccNo.Trim();
                    _myExcelWorksheet.get_Range("H" + _startRow.ToString(), _misValue).Formula = _job.SiteAddress.Trim();
                    _myExcelWorksheet.get_Range("J" + _startRow.ToString(), _misValue).Formula = _job.JobDescription.Trim();

                    //var transStatus = transactionList.Where(x => x.JobLogId == _job.JobLogId).FirstOrDefault();

                    //if (!transStatus.Presented)
                        _myExcelWorksheet.get_Range("P" + _startRow.ToString(), _misValue).Formula = "Not Paid";
                    //else
                    //    _myExcelWorksheet.get_Range("P" + _startRow.ToString(), _misValue).Formula = "Part Paid";

                    _myExcelWorksheet.get_Range("Q" + _startRow.ToString(), _misValue).Formula = Math.Round(Convert.ToDecimal(_job.Amount), 2).ToString();

                    _myExcelWorksheet.get_Range("R" + _startRow.ToString(), _misValue).Formula = defaultPaidAmt.ToString();
                    _myExcelWorksheet.get_Range("S" + _startRow.ToString(), _misValue).Formula = Math.Round(Convert.ToDecimal(_job.Amount), 2).ToString();
                    _myExcelWorksheet.get_Range("T" + _startRow.ToString(), _misValue).Formula = _job.DutyCode.Trim();
                }

                if (_jobInformation.Count > 0)
                {
                    _excelApp.ScreenUpdating = true;

                    string invoiceExportFolder = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Exported Invoices";
                    bool _dataBaseFolderExist = Directory.Exists(invoiceExportFolder);

                    if (!_dataBaseFolderExist)
                        Directory.CreateDirectory(invoiceExportFolder);

                    string _path = invoiceExportFolder;
                    string _regionName = _jobInformation[0].Region.Trim();
                    string currentTime = DateTime.Now.ToString("_HH_mm_ss");

                    _myExcelWorksheet.SaveAs(_path + "\\" + _regionName + "-" + getDiaryInfo.SaveAs.Trim() + currentTime + ".xlsx", _misValue, _misValue, _misValue,
                                            _misValue, _misValue, _misValue, _misValue, _misValue, _misValue);

                    _excelWorkbooks.Close();
                    _excelApp.Quit();

                    Marshal.ReleaseComObject(_excelWorkbooks);
                    Marshal.ReleaseComObject(_excelWorkbooks);
                    Marshal.ReleaseComObject(_excelApp);
                }
            }
        }

        private void performReconciliation(List<BillingTransaction> transactionList, int regionId)
        {
            Excel.Application _excelApp;
            Excel.Workbooks _excelWorkbooks;
            Excel.Workbook _excelWorkbook;
            object _misValue = System.Reflection.Missing.Value;
            List<int> JobIds = new List<int>();
            List<CM_Config> _configDetails = new List<CM_Config>();
            //var clientSubSet = billingInformation.Where(x => x.RegionId == regionId).ToList();

            var getDistinctDiarys = transactionList.Select(x => x.DiaryTemplateId).Distinct().ToList();
            JobIds = transactionList.Select(x => x.JobLogId).ToList();

            dbContext.Database.CommandTimeout = 0;

            var jobLogList = dbContext.ClientBillingInfoViews.Where(x => JobIds.Contains(x.JobLogId)).AsNoTracking().ToList();
            _configDetails = dbContext.CM_Config.ToList();

            foreach (var diaryId in getDistinctDiarys)
            {
                var getDiaryInfo = dbContext.CM_DiaryTemplate.Where(x => x.DiaryTemplateId == diaryId).FirstOrDefault();

                _excelApp = new Microsoft.Office.Interop.Excel.Application();
                string _applicationFolderPath = AppDomain.CurrentDomain.BaseDirectory;
                string _defaultFolderPath = _applicationFolderPath + "FlowDiaryTemplates";
                string _diaryFileName = _defaultFolderPath + "\\" + getDiaryInfo.Description.Trim();
                string diary = getDiaryInfo.Description.Trim();

                _excelWorkbooks = _excelApp.Workbooks;

                _excelWorkbook = _excelWorkbooks.Open(_diaryFileName, _misValue, _misValue, _misValue, _misValue,
                                 _misValue, _misValue, _misValue, _misValue, _misValue, _misValue, _misValue,
                                 _misValue, _misValue, _misValue);

                Excel.Worksheet _myExcelWorksheet = (Excel.Worksheet)_excelWorkbook.ActiveSheet;
                int _startRow = 12;
                _excelApp.ScreenUpdating = false;

                var _jobInformation = jobLogList.Where(x => x.DiaryTemplateId == diaryId).ToList();

                if (!_configDetails[0].BillingExportOrder.Trim().Equals("TicketNo"))
                    _jobInformation = _jobInformation.OrderBy(x => x.Contractor).ThenBy(x => x.JobDate).ToList();

                bool _clientCodeFound = false;

                foreach (var _job in _jobInformation)
                {
                    _startRow++;
                    _clientCodeFound = false;

                    _myExcelWorksheet.get_Range("A" + _startRow.ToString(), _misValue).Formula = _job.JobDate.ToString("MM/dd/yyyy");
                    _myExcelWorksheet.get_Range("B" + _startRow.ToString(), _misValue).Formula = _job.Contractor.Trim();
                    _myExcelWorksheet.get_Range("C" + _startRow.ToString(), _misValue).Formula = _job.TicketNo.Trim();

                    if (diary.Trim() == "PBX Install")
                    {
                        _myExcelWorksheet.get_Range("D" + _startRow.ToString(), _misValue).Formula = string.Empty;
                        _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.TelephoneNo.Trim().Replace("-", "").Replace("_", "");
                        _myExcelWorksheet.get_Range("F" + _startRow.ToString(), _misValue).Formula = _job.SiteAddress.Trim();
                        _myExcelWorksheet.get_Range("H" + _startRow.ToString(), _misValue).Formula = _job.JobDescription.Trim();
                    }
                    else if (diary.Trim() == "REHAB")
                    {
                        _myExcelWorksheet.get_Range("D" + _startRow.ToString(), _misValue).Formula = _job.TicketNo.Trim();
                        _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.TelephoneNo.Trim().Replace("-", "").Replace("_", "");
                        _myExcelWorksheet.get_Range("F" + _startRow.ToString(), _misValue).Formula = _job.SiteAddress.Trim();
                        _myExcelWorksheet.get_Range("H" + _startRow.ToString(), _misValue).Formula = _job.JobDescription.Trim();
                    }
                    else if (diary.Trim() == "IPTV Installation")
                    {
                        _myExcelWorksheet.get_Range("D" + _startRow.ToString(), _misValue).Formula = string.Empty;
                        _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.TelephoneNo.Trim().Replace("-", "").Replace("_", "");
                        _myExcelWorksheet.get_Range("F" + _startRow.ToString(), _misValue).Formula = _job.SiteAddress.Trim();
                        _myExcelWorksheet.get_Range("H" + _startRow.ToString(), _misValue).Formula = _job.JobDescription.Trim();
                    }
                    else if (diary.Trim() == "HFC Install and Repairs" || diary.Trim() == "HFC Install and Repairs ND" || diary.Trim() == "HFC Install and Repairs SRVC")
                    {
                        _myExcelWorksheet.get_Range("D" + _startRow.ToString(), _misValue).Formula = _job.ServiceOrderNo.Trim();//string.Empty;
                        _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.CustomerName.Trim();
                        _myExcelWorksheet.get_Range("F" + _startRow.ToString(), _misValue).Formula = _job.SiteAddress.Trim();
                        _myExcelWorksheet.get_Range("H" + _startRow.ToString(), _misValue).Formula = _job.JobDescription.Trim();
                        _myExcelWorksheet.get_Range("M" + _startRow.ToString(), _misValue).Formula = _job.Service.TrimStart('-');
                        _myExcelWorksheet.get_Range("AG" + _startRow.ToString(), _misValue).Formula = _job.QA100;
                        _myExcelWorksheet.get_Range("AH" + _startRow.ToString(), _misValue).Formula = _job.WRK;
                        _myExcelWorksheet.get_Range("AI" + _startRow.ToString(), _misValue).Formula = _job.RG6M;
                        _myExcelWorksheet.get_Range("AJ" + _startRow.ToString(), _misValue).Formula = _job.RG6l;
                        _myExcelWorksheet.get_Range("AK" + _startRow.ToString(), _misValue).Formula = _job.RG11;
                        _myExcelWorksheet.get_Range("AL" + _startRow.ToString(), _misValue).Formula = _job.AMPS;
                        _myExcelWorksheet.get_Range("AM" + _startRow.ToString(), _misValue).Formula = _job.SPLIT;
                    }
                    else
                    {
                        _myExcelWorksheet.get_Range("D" + _startRow.ToString(), _misValue).Formula = _job.TelephoneNo.Trim().Replace("-", "").Replace("_", "");
                        _myExcelWorksheet.get_Range("E" + _startRow.ToString(), _misValue).Formula = _job.SiteAddress.Trim();
                        _myExcelWorksheet.get_Range("G" + _startRow.ToString(), _misValue).Formula = _job.JobDescription.Trim();
                    }

                    string _cellInfo = string.Empty;

                    if (diary.Trim() != "HFC Install and Repairs" || diary.Trim() == "HFC Install and Repairs ND" || diary.Trim() == "HFC Install and Repairs SRVC")
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("M11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("M" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }

                        if (!_clientCodeFound)
                        {
                            _cellInfo = _myExcelWorksheet.get_Range("N11", _misValue).Formula.ToString();

                            if (_cellInfo == _job.ClientCodeDescription)
                            {
                                _myExcelWorksheet.get_Range("N" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                                _clientCodeFound = true;
                            }
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("O11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("O" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("P11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("P" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("Q11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("Q" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("R11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("R" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("S11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("S" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("T11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("T" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("U11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("U" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("V11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("V" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("W11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("W" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("X11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("X" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("Y11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("Y" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("Z11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("Z" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AA11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AA" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AB11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AB" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AC11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AC" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AD11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AD" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AE11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AE" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AF11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AF" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AG11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AG" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AH11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AH" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AI11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AI" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AJ11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AJ" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AK11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AK" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AL11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AL" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }

                    if (!_clientCodeFound)
                    {
                        _cellInfo = _myExcelWorksheet.get_Range("AM11", _misValue).Formula.ToString();

                        if (_cellInfo == _job.ClientCodeDescription)
                        {
                            _myExcelWorksheet.get_Range("AM" + _startRow.ToString(), _misValue).Formula = _job.BillableQuantity;
                            _clientCodeFound = true;
                        }
                    }
                }

                if (_jobInformation.Count > 0)
                {
                    _excelApp.ScreenUpdating = true;

                    string invoiceExportFolder = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Exported Invoices";
                    bool _dataBaseFolderExist = Directory.Exists(invoiceExportFolder);

                    if (!_dataBaseFolderExist)
                        Directory.CreateDirectory(invoiceExportFolder);

                    string _path = invoiceExportFolder;
                    string _regionName = _jobInformation[0].Region.Trim();
                    string currentTime = DateTime.Now.ToString("_HH_mm_ss");

                    _myExcelWorksheet.SaveAs(_path + "\\" + _regionName + "-" + getDiaryInfo.SaveAs.Trim() + currentTime + ".xlsx", _misValue, _misValue, _misValue,
                                            _misValue, _misValue, _misValue, _misValue, _misValue, _misValue);

                    _excelWorkbooks.Close();
                    _excelApp.Quit();

                    Marshal.ReleaseComObject(_excelWorkbooks);
                    Marshal.ReleaseComObject(_excelWorkbooks);
                    Marshal.ReleaseComObject(_excelApp);
                }
            }
        }

        private void btnDiaryExport_Click(object sender, EventArgs e)
        {
            if (grdDataDisplay.Rows.Count <= 0)
                return;

            if (ddlRegion.SelectedValue == null || Convert.ToInt32(ddlRegion.SelectedValue) == -1)
            {
                RadMessageBox.Show("A region is required for this export!", Application.ProductName);
                return;
            }

            try
            {
                int regionId = Convert.ToInt32(ddlRegion.SelectedValue);
                int monthId = Convert.ToInt32(ddlPeriod.SelectedValue);
                int year = Convert.ToInt32(maskYear.Text);
                bool unfoundTransactionsExist = false;

                if (regionId > 0)
                    unfoundTransactionsExist = dbContext.BillingTransactions.Where(x => x.RegionId == regionId 
                                               && x.Jobdate.Month == monthId && x.Jobdate.Year == year && !x.Presented).AsNoTracking().Any();
                else
                    unfoundTransactionsExist = dbContext.BillingTransactions.Where(x => x.Jobdate.Month == monthId && x.Jobdate.Year == year && !x.Presented).AsNoTracking().Any();

                if (!unfoundTransactionsExist)
                {
                    RadMessageBox.Show("No transaction(s) found!", Application.ProductName);
                    return;
                }

                if (!bgwFetchTransactions.IsBusy)
                {
                    FrmWaitDialogue waitDialogue = new FrmWaitDialogue();  

                    List<object> bgwargument = new List<object>();
                    bgwargument.Add(regionId);
                    bgwargument.Add(0);
                    bgwargument.Add("Diary");

                    bgwFetchTransactions.RunWorkerAsync(bgwargument);
                    waitDialogue.ShowDialog();
                }
            }
            catch //(Exception _exp)
            {
                //RadMessageBox.Show(_exp.InnerException == null ? _exp.Message : _exp.InnerException.Message);
            }

            //int regionId = Convert.ToInt32(ddlRegion.SelectedValue);
            //var unfoundTransactions = dbContext.BillingTransactions.Where(x => x.RegionId == regionId && !x.Presented).AsNoTracking().ToList();

            //if (unfoundTransactions.Count > 0)
            //    performReconciliation(unfoundTransactions, regionId);
            //else
                
        }

        private void bgwFetchTransactions_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> argumentList = e.Argument as List<object>;
            int Id = Convert.ToInt32(argumentList[0]);
            int year = Convert.ToInt32(argumentList[1]);
            string process = argumentList[2].ToString();

            if (process.Equals("Records"))
            {
                transactionList = new List<BillingReconStatusView>();

                dbContext.Database.CommandTimeout = 0;

                transactionList = dbContext.BillingReconStatusViews.Where(x => x.UnconvertedJobDate.Month == Id && x.UnconvertedJobDate.Year == year).AsNoTracking().ToList();                
            }
            else
            {
                int monthId = Convert.ToInt32(ddlPeriod.SelectedValue);
                int requestYear = Convert.ToInt32(maskYear.Text);
                List<BillingTransaction> unfoundTransactions = new List<BillingTransaction>();
                List<BillingTransaction> shortPayments = new List<BillingTransaction>();
                dbContext.Database.CommandTimeout = 0;

                if (Id > 0)
                {
                    unfoundTransactions = dbContext.BillingTransactions.Where(x => x.RegionId == Id
                                              && x.Jobdate.Month == monthId && x.Jobdate.Year == requestYear && !x.Presented).AsNoTracking().ToList();

                    shortPayments = dbContext.BillingTransactions.Where(x => x.RegionId == Id
                                        && x.Jobdate.Month == monthId && x.Jobdate.Year == requestYear && x.Presented && x.Comment.Trim().Equals("Under Paid")
                                        && x.Status.Trim().Equals("Found")).AsNoTracking().ToList();
                }
                else
                {
                    unfoundTransactions = dbContext.BillingTransactions.Where(x => x.Jobdate.Month == monthId && x.Jobdate.Year == requestYear && !x.Presented).AsNoTracking().ToList();

                    shortPayments = dbContext.BillingTransactions.Where(x => x.Jobdate.Month == monthId && x.Jobdate.Year == requestYear && x.Presented && x.Comment.Trim().Equals("Under Paid")
                                    && x.Status.Trim().Equals("Found")).AsNoTracking().ToList();
                }

                //List<BillingTransaction> totalListing = new List<BillingTransaction>();
                //totalListing.AddRange(unfoundTransactions);
                //totalListing.AddRange(shortPayments);

                if (unfoundTransactions.Count > 0)
                    performNewFormatRecon(unfoundTransactions, Id);//performReconciliation(totalListing, Id);

                if (shortPayments.Count > 0)
                    performShortPaymentRecon(shortPayments, Id);
            }

            e.Result = process;
        }

        private void bgwFetchTransactions_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            for (int index = Application.OpenForms.Count - 1; index >= 0; index--)
            {
                if (Application.OpenForms[index].Name == "FrmWaitDialogue")
                    Application.OpenForms[index].Close();
            }

            if (e.Result.ToString().Equals("Records"))
                grdDataDisplay.DataSource = transactionList;
            else if (e.Result.ToString().Equals("Diary"))
                RadMessageBox.Show("Diaries created and exported to your Desktop successfully", Application.ProductName);
        }
    }
}