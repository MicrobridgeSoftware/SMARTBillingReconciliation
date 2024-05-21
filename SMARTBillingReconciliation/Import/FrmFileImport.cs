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
using Telerik.WinControls.UI;
using System.Runtime.InteropServices;
using SMARTBillingReconciliation.ApplicationUtil;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using ApplicationSecurity;

namespace SMARTBillingReconciliation.Import
{
    public partial class FrmFileImport : Telerik.WinControls.UI.RadForm
    {
        ContractorManagementEntities dbContext = new ContractorManagementEntities();
        List<RegionView> regions;
        List<MonthOfTheYear> months;
        List<CM_Config> config;

        public FrmFileImport()
        {
            InitializeComponent();
        }

        private void FrmFileImport_Load(object sender, EventArgs e)
        {
            config = new List<CM_Config>();

            config = dbContext.CM_Config.AsNoTracking().ToList();

            this.rbeFile.DialogType = BrowseEditorDialogType.OpenFileDialog;
            OpenFileDialog dialog = (OpenFileDialog)rbeFile.Dialog;
            dialog.Filter = "Excel Files| *.xls; *.xlsx; *.xlsm";

            regions = new List<RegionView>();
            months = new List<MonthOfTheYear>();

            regions = dbContext.RegionViews.AsNoTracking().ToList();
            months = dbContext.MonthOfTheYears.AsNoTracking().ToList();

            bindingSourceMonth.DataSource = months;
            bindingSourceRegion.DataSource = regions;

            ddlCategory.SelectedIndex = -1;
            ddlPeriod.SelectedIndex = -1;
            ddlRegion.SelectedIndex = -1;

            ddlRegion.DropDownListElement.AutoCompleteAppend.LimitToList = true;
            ddlPeriod.DropDownListElement.AutoCompleteAppend.LimitToList = true;
            ddlCategory.DropDownListElement.AutoCompleteAppend.LimitToList = true;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (rbeFile.Value == null || config.Count <= 0)
                return;

            if (config[0].ReconKey == null || config[0].ReconKey.Trim() == string.Empty)
            {
                RadMessageBox.Show("No key found to perform this recon. Contact system administrator!", Application.ProductName);
                return;
            }

            if (ddlCategory.SelectedIndex < 0)
            {
                RadMessageBox.Show("The category for the items being imported are required!", Application.ProductName);
                return;
            }

            if (ddlPeriod.SelectedValue == null || Convert.ToInt32(ddlPeriod.SelectedValue) == 0)
            {
                RadMessageBox.Show("The period for the items being imported are required!", Application.ProductName);
                return;
            }

            if (ddlRegion.SelectedValue == null || Convert.ToInt32(ddlRegion.SelectedValue) == 0)
            {
                RadMessageBox.Show("The region for the items being imported are required!", Application.ProductName);
                return;
            }

            if (maskYear.Text.Trim().Replace("_","") == string.Empty || maskYear.Text.Trim().Replace("_", "").Length != 4)
            {
                RadMessageBox.Show("Invalid import year detected!", Application.ProductName);
                return;
            }

            if (!config[0].ReconKey.Trim().ToUpper().Equals("TICKET NO") && !config[0].ReconKey.Trim().ToUpper().Equals("SERVICE ORDER NO"))
            {
                RadMessageBox.Show("Invalid key supplied!", Application.ProductName);
                return;
            }

            string category = "123";
            int monthId = Convert.ToInt32(ddlPeriod.SelectedValue);
            int regionId = Convert.ToInt32(ddlRegion.SelectedValue);
            int year = Convert.ToInt32(maskYear.Text);
                        
            bool suchRecordsExist = false;

            //if (ddlCategory.SelectedIndex == 0)
                suchRecordsExist = dbContext.BillingTransactionsListingViews.Where(x => x.Jobdate.Value.Month == monthId && x.RegionId == regionId
                                   && x.Jobdate.Value.Year == year && !x.DutyCode.Trim().Contains(category)).Any();
            //else if (ddlCategory.SelectedIndex == 1)
            //    suchRecordsExist = dbContext.BillingTransactionsListingViews.Where(x => x.Jobdate.Value.Month == monthId && x.RegionId == regionId
            //                       && x.Jobdate.Value.Year == year && x.DutyCode.Trim().Contains(category)).Any();

            if (!suchRecordsExist)
            {
                RadMessageBox.Show("Cannot find corresponding record(s) to reconcile against in Contractor Management", Application.ProductName);
                return;
            }

            DialogResult promptUser = RadMessageBox.Show("Are you sure you want to import this file for reconciliation?", Application.ProductName, MessageBoxButtons.YesNo);

            if (promptUser == DialogResult.No)
                return;

            try
            {
                if (!bgwProcessImport.IsBusy)
                {
                    FrmWaitDialogue waitDialogue = new FrmWaitDialogue();

                    List<object> bgwargument = new List<object>();
                    bgwargument.Add(monthId);
                    bgwargument.Add(regionId);
                    bgwargument.Add(year);

                    bgwProcessImport.RunWorkerAsync(bgwargument);
                    waitDialogue.ShowDialog();
                }
            }
            catch //(Exception _exp)
            {
                //RadMessageBox.Show(_exp.InnerException == null ? _exp.Message : _exp.InnerException.Message);
            }            
        }

        private void bgwProcessImport_DoWork(object sender, DoWorkEventArgs e)
        {
            List<BillingTransactionsListingView> billingTransactions = new List<BillingTransactionsListingView>();
            List<ImportFileDefinition> fileDefinition = new List<ImportFileDefinition>();
            List<object> argumentList = e.Argument as List<object>;
            int monthId = Convert.ToInt32(argumentList[0]);
            int regionId = Convert.ToInt32(argumentList[1]);
            int year = Convert.ToInt32(argumentList[2]);
            
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelBook = xlApp.Workbooks.Open(rbeFile.Value.ToString());
            object _misValue = System.Reflection.Missing.Value;            

            foreach (Microsoft.Office.Interop.Excel.Worksheet wSheet in excelBook.Worksheets)
            {                
                wSheet.Activate();
                int rowCount = wSheet.UsedRange.Rows.Count;

                for (int row = 2; row <= rowCount; row++)
                {
                    string ticketNo = wSheet.get_Range("A" + row.ToString(), _misValue).Formula.ToString();
                    DateTime transDate = wSheet.get_Range("B" + row.ToString(), _misValue).Value;//.Formula.ToString();                    
                    string transValue = wSheet.get_Range("C" + row.ToString(), _misValue).Formula.ToString();

                    if (ticketNo.Trim() != string.Empty && transDate != null && transValue.Trim() != string.Empty)
                        fileDefinition.Add(new ImportFileDefinition { ticketNumber = ticketNo, transactionAmount = Convert.ToDecimal(transValue), transactionDate = Convert.ToDateTime(transDate) });
                }                
            }

            excelBook.Close();
            xlApp.Workbooks.Close();
            xlApp.Quit();
            Marshal.ReleaseComObject(excelBook);
            Marshal.ReleaseComObject(xlApp);
            
            var getImportedFlowRecords = dbContext.FlowFileImports.ToList();
            var getFoundTransactions = dbContext.FoundReconTransactions.ToList();

            foreach (var record in getImportedFlowRecords)
                dbContext.FlowFileImports.Remove(record);

            foreach(var transaction in getFoundTransactions)
                dbContext.FoundReconTransactions.Remove(transaction);

            if (getImportedFlowRecords.Count > 0 || getFoundTransactions.Count > 0)
                dbContext.SaveChanges();
            
            foreach(ImportFileDefinition import in fileDefinition)
            {
                FlowFileImport flowFile = new FlowFileImport();
                flowFile.FileName = Path.GetFileName(rbeFile.Value.ToString());
                flowFile.RegionId = regionId;
                flowFile.TicketNo = import.ticketNumber;
                flowFile.TransactionDate = import.transactionDate;
                flowFile.TransactionValue = import.transactionAmount;
                dbContext.FlowFileImports.Add(flowFile);
            }

            //get everything from the billing table for the respective period
            var getBillingTransactionsForPeriod = dbContext.BillingTransactions.Where(x => x.Jobdate.Month == monthId && x.Jobdate.Year == year /*&& x.RegionId == regionId*/).AsNoTracking().ToList();
                        
            billingTransactions = dbContext.BillingTransactionsListingViews.Where(x => x.Jobdate.Value.Month == monthId && x.RegionId == regionId
                                  && x.Jobdate.Value.Year == year/* && !x.DutyCode.Trim().Contains("123")*/).AsNoTracking().ToList();
           
            foreach (BillingTransactionsListingView transaction in billingTransactions)
            {
                bool combinationExist = true;

                if (config[0].ReconKey.Trim().ToUpper().Equals("TICKET NO"))
                    combinationExist = getBillingTransactionsForPeriod.Where(x => x.TicketNo.Trim().ToUpper().Equals(transaction.TicketNo.Trim().ToUpper())
                                       && x.Jobdate == transaction.Jobdate && x.Value == transaction.Value).Any();
                else if (config[0].ReconKey.Trim().ToUpper().Equals("SERVICE ORDER NO"))
                    combinationExist = getBillingTransactionsForPeriod.Where(x => x.ServiceOrderNo.Trim().ToUpper().Equals(transaction.ServiceOrderNo.Trim().ToUpper())
                                       && x.Jobdate == transaction.Jobdate && x.Value == transaction.Value).Any();
               
                if (!combinationExist)
                {
                    BillingTransaction billingTransaction = new BillingTransaction();
                    billingTransaction.ClawedBack = false;
                    billingTransaction.Jobdate = Convert.ToDateTime(transaction.Jobdate).Date;
                    billingTransaction.Region = transaction.Region;
                    billingTransaction.RegionId = transaction.RegionId;
                    billingTransaction.ServiceCategory = ddlCategory.Text.Trim();
                    billingTransaction.TicketNo = transaction.TicketNo;
                    billingTransaction.Value = Convert.ToDecimal(transaction.Value);
                    billingTransaction.DiaryTemplateId = transaction.DiaryTemplateId;
                    billingTransaction.JobLogId = transaction.JobLogId;
                    billingTransaction.Presented = false;
                    billingTransaction.DutyCode = transaction.DutyCode.Trim();
                    billingTransaction.ServiceOrderNo = transaction.ServiceOrderNo.Trim();
                    dbContext.BillingTransactions.Add(billingTransaction);
                }
            }

            dbContext.SaveChanges();
            
            DateTime processedDate = DateTime.Now;

            var getFoundJobClaims = dbContext.GetJobClaimTransactionsFromImport().ToList();
            
            foreach (var claim in getFoundJobClaims)//foreach(var claim in getFoundJobClaims.Where(x=> x.Variance == 0))
            {
                List<BillingTransaction> updateBillingRecord = new List<BillingTransaction>();

                if (config[0].ReconKey.Trim().ToUpper().Equals("TICKET NO"))
                    updateBillingRecord = dbContext.BillingTransactions.Where(x => x.TicketNo.Trim().ToUpper() == claim.AccountNo.Trim().ToUpper() && x.Jobdate.Month <= monthId && x.RegionId == regionId).ToList();
                else if (config[0].ReconKey.Trim().ToUpper().Equals("SERVICE ORDER NO"))
                    updateBillingRecord = dbContext.BillingTransactions.Where(x => x.ServiceOrderNo.Trim().ToUpper() == claim.ServiceOrderNo.Trim().ToUpper() && x.Jobdate.Month <= monthId && x.RegionId == regionId).ToList();

                if (updateBillingRecord.Count > 0)
                {
                    foreach (var record in updateBillingRecord)
                    {
                        record.DatePresented = Convert.ToDateTime(claim.FlowValueDate); //processedDate; changed at Mr. Allison's request
                        record.DateProcessed = processedDate;
                        record.ProcessedBy = ApplicationSecurityConstants._activeUser;
                        record.FileName = Path.GetFileName(rbeFile.Value.ToString());
                        record.Presented = true;

                        if (claim.Variance == 0)
                        {
                            record.Status = "Found";
                            record.Comment = "Balance";
                        }
                        else if (claim.Variance < 0)
                        {
                            record.Status = "Found";
                            record.Comment = "Under Paid";
                        }
                        else if (claim.Variance > 0)
                        {
                            record.Status = "Found";
                            record.Comment = "Over Paid";
                        }                            
                    }

                    FoundReconTransaction reconTransaction = new FoundReconTransaction();
                    reconTransaction.AccountNo = claim.AccountNo;
                    reconTransaction.FileName = Path.GetFileName(rbeFile.Value.ToString());
                    reconTransaction.FlowValue = claim.FlowValue;
                    reconTransaction.FlowValueDate = claim.FlowValueDate;
                    reconTransaction.Jobdate = claim.Jobdate;
                    reconTransaction.JobValue = claim.JobValue;
                    reconTransaction.Region = claim.Region;
                    reconTransaction.ServiceCategory = claim.ServiceCategory;
                    reconTransaction.Variance = claim.Variance;
                    reconTransaction.ServiceOrderNo = claim.ServiceOrderNo.Trim();
                    dbContext.FoundReconTransactions.Add(reconTransaction);

                    var getTransaction = dbContext.PaidTransactions.Where(x => x.AccountNo.Trim().Equals(claim.AccountNo)).FirstOrDefault();

                    if (getTransaction != null)
                    {
                        getTransaction.PaidAmount = claim.FlowValue == null ? 0 : Convert.ToDecimal(claim.FlowValue);
                        getTransaction.JobValue = claim.JobValue == null ? 0 : Convert.ToDecimal(claim.JobValue);
                    }
                    else
                    {
                        PaidTransaction transaction = new PaidTransaction();
                        transaction.AccountNo = claim.AccountNo;
                        transaction.JobValue = claim.JobValue == null ? 0 : Convert.ToDecimal(claim.JobValue);
                        transaction.PaidAmount = claim.FlowValue == null ? 0 : Convert.ToDecimal(claim.FlowValue);
                        dbContext.PaidTransactions.Add(transaction);
                    }
                }
            }                            
            
            dbContext.SaveChanges();
            //performReconciliation(billingTransactionList, regionId);
            e.Result = "Number of transaction(s) imported, " + fileDefinition.Count.ToString() + "." + Environment.NewLine +
                       "Value of imported Transaction(s), " + string.Format("{0:C}", fileDefinition.Sum(x => x.transactionAmount));

            MemoryManagement.FlushMemory();
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

        private void bgwProcessImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            for (int index = Application.OpenForms.Count - 1; index >= 0; index--)
            {
                if (Application.OpenForms[index].Name == "FrmWaitDialogue")
                    Application.OpenForms[index].Close();
            }
           
            RadMessageBox.Show("File imported successfully." + Environment.NewLine + e.Result.ToString(), Application.ProductName);
        }
    }
}