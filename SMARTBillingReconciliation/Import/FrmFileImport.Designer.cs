namespace SMARTBillingReconciliation.Import
{
    partial class FrmFileImport
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.maskYear = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.rbeFile = new Telerik.WinControls.UI.RadBrowseEditor();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.ddlCategory = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.ddlRegion = new Telerik.WinControls.UI.RadDropDownList();
            this.bindingSourceRegion = new System.Windows.Forms.BindingSource(this.components);
            this.ddlPeriod = new Telerik.WinControls.UI.RadDropDownList();
            this.bindingSourceMonth = new System.Windows.Forms.BindingSource(this.components);
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.btnProcess = new Telerik.WinControls.UI.RadButton();
            this.bgwProcessImport = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maskYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.maskYear);
            this.radGroupBox1.Controls.Add(this.radLabel5);
            this.radGroupBox1.Controls.Add(this.radLabel4);
            this.radGroupBox1.Controls.Add(this.rbeFile);
            this.radGroupBox1.Controls.Add(this.radLabel3);
            this.radGroupBox1.Controls.Add(this.ddlCategory);
            this.radGroupBox1.Controls.Add(this.radLabel2);
            this.radGroupBox1.Controls.Add(this.ddlRegion);
            this.radGroupBox1.Controls.Add(this.ddlPeriod);
            this.radGroupBox1.Controls.Add(this.radLabel1);
            this.radGroupBox1.HeaderText = "File Processing Credentials";
            this.radGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Size = new System.Drawing.Size(408, 162);
            this.radGroupBox1.TabIndex = 10;
            this.radGroupBox1.Text = "File Processing Credentials";
            // 
            // maskYear
            // 
            this.maskYear.Location = new System.Drawing.Point(100, 52);
            this.maskYear.Mask = "####";
            this.maskYear.MaskType = Telerik.WinControls.UI.MaskType.Standard;
            this.maskYear.Name = "maskYear";
            this.maskYear.Size = new System.Drawing.Size(125, 20);
            this.maskYear.TabIndex = 18;
            this.maskYear.TabStop = false;
            this.maskYear.Text = "____";
            // 
            // radLabel5
            // 
            this.radLabel5.Location = new System.Drawing.Point(28, 53);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(65, 18);
            this.radLabel5.TabIndex = 17;
            this.radLabel5.Text = "Import Year";
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(33, 132);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(60, 18);
            this.radLabel4.TabIndex = 15;
            this.radLabel4.Text = "Import File";
            // 
            // rbeFile
            // 
            this.rbeFile.Location = new System.Drawing.Point(100, 131);
            this.rbeFile.Name = "rbeFile";
            this.rbeFile.Size = new System.Drawing.Size(259, 20);
            this.rbeFile.TabIndex = 14;
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(23, 105);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(71, 18);
            this.radLabel3.TabIndex = 13;
            this.radLabel3.Text = "File Category";
            // 
            // ddlCategory
            // 
            this.ddlCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            radListDataItem1.Text = "Job Claims";
            this.ddlCategory.Items.Add(radListDataItem1);
            this.ddlCategory.Location = new System.Drawing.Point(100, 105);
            this.ddlCategory.Name = "ddlCategory";
            this.ddlCategory.Size = new System.Drawing.Size(259, 20);
            this.ddlCategory.TabIndex = 12;
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(53, 79);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(41, 18);
            this.radLabel2.TabIndex = 11;
            this.radLabel2.Text = "Region";
            // 
            // ddlRegion
            // 
            this.ddlRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlRegion.DataSource = this.bindingSourceRegion;
            this.ddlRegion.DisplayMember = "RegionDescription";
            this.ddlRegion.Location = new System.Drawing.Point(100, 79);
            this.ddlRegion.Name = "ddlRegion";
            this.ddlRegion.Size = new System.Drawing.Size(259, 20);
            this.ddlRegion.TabIndex = 10;
            this.ddlRegion.ValueMember = "RegionId";
            // 
            // bindingSourceRegion
            // 
            this.bindingSourceRegion.DataSource = typeof(SMARTBillingReconciliation.RegionView);
            // 
            // ddlPeriod
            // 
            this.ddlPeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlPeriod.DataSource = this.bindingSourceMonth;
            this.ddlPeriod.DisplayMember = "MonthName";
            this.ddlPeriod.Location = new System.Drawing.Point(100, 26);
            this.ddlPeriod.Name = "ddlPeriod";
            this.ddlPeriod.Size = new System.Drawing.Size(259, 20);
            this.ddlPeriod.TabIndex = 9;
            this.ddlPeriod.ValueMember = "MonthOfTheYearId";
            // 
            // bindingSourceMonth
            // 
            this.bindingSourceMonth.DataSource = typeof(SMARTBillingReconciliation.MonthOfTheYear);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(18, 26);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(75, 18);
            this.radLabel1.TabIndex = 8;
            this.radLabel1.Text = "Import Period";
            // 
            // btnProcess
            // 
            this.btnProcess.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnProcess.Image = global::SMARTBillingReconciliation.Properties.Resources.Apps_system_software_update_icon;
            this.btnProcess.Location = new System.Drawing.Point(28, 188);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(88, 29);
            this.btnProcess.TabIndex = 11;
            this.btnProcess.Text = "Run Recon";
            this.btnProcess.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // bgwProcessImport
            // 
            this.bgwProcessImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwProcessImport_DoWork);
            this.bgwProcessImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwProcessImport_RunWorkerCompleted);
            // 
            // FrmFileImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 227);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.radGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFileImport";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "File Import";
            this.Load += new System.EventHandler(this.FrmFileImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maskYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadBrowseEditor rbeFile;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadDropDownList ddlCategory;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadDropDownList ddlRegion;
        private Telerik.WinControls.UI.RadDropDownList ddlPeriod;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton btnProcess;
        private System.Windows.Forms.BindingSource bindingSourceRegion;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private System.Windows.Forms.BindingSource bindingSourceMonth;
        private Telerik.WinControls.UI.RadMaskedEditBox maskYear;
        private System.ComponentModel.BackgroundWorker bgwProcessImport;
    }
}
