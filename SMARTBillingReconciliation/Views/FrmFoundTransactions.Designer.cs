namespace SMARTBillingReconciliation.Views
{
    partial class FrmFoundTransactions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.Data.SortDescriptor sortDescriptor1 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.grdDataDisplay = new Telerik.WinControls.UI.RadGridView();
            this.maskYear = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.ddlPeriod = new Telerik.WinControls.UI.RadDropDownList();
            this.btnExport = new Telerik.WinControls.UI.RadButton();
            this.btnSearch = new Telerik.WinControls.UI.RadButton();
            this.btnDiaryExport = new Telerik.WinControls.UI.RadButton();
            this.ddlRegion = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.bgwFetchTransactions = new System.ComponentModel.BackgroundWorker();
            this.bindingSourceRegion = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourcePeriod = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceJobDetails = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataDisplay.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDiaryExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceJobDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // grdDataDisplay
            // 
            this.grdDataDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.grdDataDisplay.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdDataDisplay.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.grdDataDisplay.ForeColor = System.Drawing.Color.Black;
            this.grdDataDisplay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdDataDisplay.Location = new System.Drawing.Point(2, 72);
            // 
            // 
            // 
            this.grdDataDisplay.MasterTemplate.AllowAddNewRow = false;
            this.grdDataDisplay.MasterTemplate.AllowColumnReorder = false;
            this.grdDataDisplay.MasterTemplate.AutoExpandGroups = true;
            this.grdDataDisplay.MasterTemplate.AutoGenerateColumns = false;
            this.grdDataDisplay.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "ServiceOrderNo";
            gridViewTextBoxColumn1.HeaderText = "Service Order No";
            gridViewTextBoxColumn1.Name = "TicketNo";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 54;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.MediumDate;
            gridViewTextBoxColumn2.FieldName = "Jobdate";
            gridViewTextBoxColumn2.FormatString = "{0:dd/MMM/yyyy}";
            gridViewTextBoxColumn2.HeaderText = "Job Date";
            gridViewTextBoxColumn2.Name = "Jobdate";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            gridViewTextBoxColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn2.Width = 45;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "DutyCode";
            gridViewTextBoxColumn3.HeaderText = "Duty Code";
            gridViewTextBoxColumn3.Name = "DutyCode";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 39;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "JobDescription";
            gridViewTextBoxColumn4.HeaderText = "Job Description";
            gridViewTextBoxColumn4.Name = "JobDescription";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 124;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "Region";
            gridViewTextBoxColumn5.HeaderText = "Region";
            gridViewTextBoxColumn5.Name = "Region";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 72;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Fixed;
            gridViewTextBoxColumn6.FieldName = "BillableQuantity";
            gridViewTextBoxColumn6.HeaderText = "Billable Quantity";
            gridViewTextBoxColumn6.Name = "BillableQuantity";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            gridViewTextBoxColumn6.Width = 37;
            gridViewTextBoxColumn7.EnableExpressionEditor = false;
            gridViewTextBoxColumn7.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Fixed;
            gridViewTextBoxColumn7.FieldName = "Rate";
            gridViewTextBoxColumn7.FormatString = "{0:C}";
            gridViewTextBoxColumn7.HeaderText = "Rate";
            gridViewTextBoxColumn7.Name = "Rate";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            gridViewTextBoxColumn7.Width = 44;
            gridViewTextBoxColumn8.EnableExpressionEditor = false;
            gridViewTextBoxColumn8.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Fixed;
            gridViewTextBoxColumn8.FieldName = "Value";
            gridViewTextBoxColumn8.FormatString = "{0:C}";
            gridViewTextBoxColumn8.HeaderText = "Value";
            gridViewTextBoxColumn8.Name = "Value";
            gridViewTextBoxColumn8.ReadOnly = true;
            gridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            gridViewTextBoxColumn8.Width = 65;
            gridViewTextBoxColumn9.EnableExpressionEditor = false;
            gridViewTextBoxColumn9.FieldName = "FileName";
            gridViewTextBoxColumn9.HeaderText = "File Name";
            gridViewTextBoxColumn9.Name = "FileName";
            gridViewTextBoxColumn9.ReadOnly = true;
            gridViewTextBoxColumn9.Width = 56;
            gridViewTextBoxColumn10.FieldName = "Status";
            gridViewTextBoxColumn10.HeaderText = "Status";
            gridViewTextBoxColumn10.Name = "Status";
            gridViewTextBoxColumn10.ReadOnly = true;
            gridViewTextBoxColumn10.Width = 46;
            gridViewTextBoxColumn11.FieldName = "Comment";
            gridViewTextBoxColumn11.HeaderText = "Comment";
            gridViewTextBoxColumn11.Name = "Comment";
            gridViewTextBoxColumn11.ReadOnly = true;
            gridViewTextBoxColumn11.Width = 42;
            gridViewTextBoxColumn12.EnableExpressionEditor = false;
            gridViewTextBoxColumn12.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.MediumDate;
            gridViewTextBoxColumn12.FieldName = "DateProcessed";
            gridViewTextBoxColumn12.FormatString = "{0:dd/MMM/yyyy}";
            gridViewTextBoxColumn12.HeaderText = "Date Processed";
            gridViewTextBoxColumn12.Name = "DateProcessed";
            gridViewTextBoxColumn12.ReadOnly = true;
            gridViewTextBoxColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn12.Width = 44;
            gridViewTextBoxColumn13.EnableExpressionEditor = false;
            gridViewTextBoxColumn13.FieldName = "ProcessedBy";
            gridViewTextBoxColumn13.HeaderText = "Processed By";
            gridViewTextBoxColumn13.Name = "ProcessedBy";
            gridViewTextBoxColumn13.ReadOnly = true;
            gridViewTextBoxColumn13.Width = 44;
            gridViewTextBoxColumn14.EnableExpressionEditor = false;
            gridViewTextBoxColumn14.FieldName = "JobLogId";
            gridViewTextBoxColumn14.HeaderText = "JobLogId";
            gridViewTextBoxColumn14.IsVisible = false;
            gridViewTextBoxColumn14.Name = "JobLogId";
            gridViewTextBoxColumn14.ReadOnly = true;
            gridViewTextBoxColumn15.EnableExpressionEditor = false;
            gridViewTextBoxColumn15.FieldName = "Presented";
            gridViewTextBoxColumn15.HeaderText = "Presented";
            gridViewTextBoxColumn15.Name = "Presented";
            gridViewTextBoxColumn15.ReadOnly = true;
            gridViewTextBoxColumn15.Width = 45;
            this.grdDataDisplay.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10,
            gridViewTextBoxColumn11,
            gridViewTextBoxColumn12,
            gridViewTextBoxColumn13,
            gridViewTextBoxColumn14,
            gridViewTextBoxColumn15});
            this.grdDataDisplay.MasterTemplate.EnableFiltering = true;
            sortDescriptor1.PropertyName = "Jobdate";
            this.grdDataDisplay.MasterTemplate.SortDescriptors.AddRange(new Telerik.WinControls.Data.SortDescriptor[] {
            sortDescriptor1});
            this.grdDataDisplay.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdDataDisplay.Name = "grdDataDisplay";
            this.grdDataDisplay.ReadOnly = true;
            this.grdDataDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdDataDisplay.Size = new System.Drawing.Size(779, 406);
            this.grdDataDisplay.TabIndex = 26;
            this.grdDataDisplay.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.grdDataDisplay_ViewCellFormatting);
            // 
            // maskYear
            // 
            this.maskYear.Location = new System.Drawing.Point(53, 37);
            this.maskYear.Mask = "####";
            this.maskYear.MaskType = Telerik.WinControls.UI.MaskType.Standard;
            this.maskYear.Name = "maskYear";
            this.maskYear.Size = new System.Drawing.Size(125, 20);
            this.maskYear.TabIndex = 24;
            this.maskYear.TabStop = false;
            this.maskYear.Text = "____";
            // 
            // radLabel5
            // 
            this.radLabel5.Location = new System.Drawing.Point(19, 38);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(28, 18);
            this.radLabel5.TabIndex = 23;
            this.radLabel5.Text = "Year";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(9, 13);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(38, 18);
            this.radLabel1.TabIndex = 22;
            this.radLabel1.Text = "Period";
            // 
            // ddlPeriod
            // 
            this.ddlPeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlPeriod.DataSource = this.bindingSourcePeriod;
            this.ddlPeriod.DisplayMember = "MonthName";
            this.ddlPeriod.Location = new System.Drawing.Point(53, 13);
            this.ddlPeriod.Name = "ddlPeriod";
            this.ddlPeriod.Size = new System.Drawing.Size(125, 20);
            this.ddlPeriod.TabIndex = 21;
            this.ddlPeriod.ValueMember = "MonthOfTheYearId";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnExport.Location = new System.Drawing.Point(550, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 34);
            this.btnExport.TabIndex = 25;
            this.btnExport.Text = "Excel Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(241, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(84, 30);
            this.btnSearch.TabIndex = 27;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDiaryExport
            // 
            this.btnDiaryExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiaryExport.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnDiaryExport.Location = new System.Drawing.Point(664, 6);
            this.btnDiaryExport.Name = "btnDiaryExport";
            this.btnDiaryExport.Size = new System.Drawing.Size(110, 34);
            this.btnDiaryExport.TabIndex = 28;
            this.btnDiaryExport.Text = "Diary Export";
            this.btnDiaryExport.Click += new System.EventHandler(this.btnDiaryExport_Click);
            // 
            // ddlRegion
            // 
            this.ddlRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlRegion.DataSource = this.bindingSourceRegion;
            this.ddlRegion.DisplayMember = "RegionDescription";
            this.ddlRegion.Location = new System.Drawing.Point(550, 46);
            this.ddlRegion.Name = "ddlRegion";
            this.ddlRegion.Size = new System.Drawing.Size(224, 20);
            this.ddlRegion.TabIndex = 29;
            this.ddlRegion.ValueMember = "RegionId";
            // 
            // radLabel2
            // 
            this.radLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel2.Location = new System.Drawing.Point(503, 46);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(41, 18);
            this.radLabel2.TabIndex = 30;
            this.radLabel2.Text = "Region";
            // 
            // bgwFetchTransactions
            // 
            this.bgwFetchTransactions.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwFetchTransactions_DoWork);
            this.bgwFetchTransactions.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwFetchTransactions_RunWorkerCompleted);
            // 
            // bindingSourceRegion
            // 
            this.bindingSourceRegion.DataSource = typeof(SMARTBillingReconciliation.RegionView);
            // 
            // bindingSourcePeriod
            // 
            this.bindingSourcePeriod.DataSource = typeof(SMARTBillingReconciliation.MonthOfTheYear);
            // 
            // bindingSourceJobDetails
            // 
            this.bindingSourceJobDetails.DataSource = typeof(SMARTBillingReconciliation.BillingTransactionsListingView);
            // 
            // FrmFoundTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 482);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.ddlRegion);
            this.Controls.Add(this.btnDiaryExport);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.grdDataDisplay);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.maskYear);
            this.Controls.Add(this.radLabel5);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.ddlPeriod);
            this.Name = "FrmFoundTransactions";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Transaction Listing";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmFoundTransactions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataDisplay.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDiaryExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceJobDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadMaskedEditBox maskYear;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList ddlPeriod;
        private Telerik.WinControls.UI.RadButton btnExport;
        private Telerik.WinControls.UI.RadGridView grdDataDisplay;
        private Telerik.WinControls.UI.RadButton btnSearch;
        private System.Windows.Forms.BindingSource bindingSourcePeriod;
        private System.Windows.Forms.BindingSource bindingSourceJobDetails;
        private Telerik.WinControls.UI.RadButton btnDiaryExport;
        private Telerik.WinControls.UI.RadDropDownList ddlRegion;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private System.Windows.Forms.BindingSource bindingSourceRegion;
        private System.ComponentModel.BackgroundWorker bgwFetchTransactions;
    }
}
