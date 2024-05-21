namespace SMARTBillingReconciliation.Exception
{
    partial class FrmExceptionList
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
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.maskYear = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.rdbtnRegion = new Telerik.WinControls.UI.RadRadioButton();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.rdbtnValue = new Telerik.WinControls.UI.RadRadioButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.rdbtnNotFound = new Telerik.WinControls.UI.RadRadioButton();
            this.ddlPeriod = new Telerik.WinControls.UI.RadDropDownList();
            this.bindingSourcePeriod = new System.Windows.Forms.BindingSource(this.components);
            this.btnPreview = new Telerik.WinControls.UI.RadButton();
            this.rdBtnFound = new Telerik.WinControls.UI.RadRadioButton();
            this.radSeparator1 = new Telerik.WinControls.UI.RadSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maskYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdbtnRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdbtnValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdbtnNotFound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdBtnFound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.radSeparator1);
            this.radGroupBox1.Controls.Add(this.rdBtnFound);
            this.radGroupBox1.Controls.Add(this.maskYear);
            this.radGroupBox1.Controls.Add(this.rdbtnRegion);
            this.radGroupBox1.Controls.Add(this.radLabel5);
            this.radGroupBox1.Controls.Add(this.rdbtnValue);
            this.radGroupBox1.Controls.Add(this.radLabel1);
            this.radGroupBox1.Controls.Add(this.rdbtnNotFound);
            this.radGroupBox1.Controls.Add(this.ddlPeriod);
            this.radGroupBox1.HeaderText = "Exception Category";
            this.radGroupBox1.Location = new System.Drawing.Point(13, 4);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Size = new System.Drawing.Size(417, 140);
            this.radGroupBox1.TabIndex = 0;
            this.radGroupBox1.Text = "Exception Category";
            // 
            // maskYear
            // 
            this.maskYear.Location = new System.Drawing.Point(270, 101);
            this.maskYear.Mask = "####";
            this.maskYear.MaskType = Telerik.WinControls.UI.MaskType.Standard;
            this.maskYear.Name = "maskYear";
            this.maskYear.Size = new System.Drawing.Size(125, 20);
            this.maskYear.TabIndex = 20;
            this.maskYear.TabStop = false;
            this.maskYear.Text = "____";
            this.maskYear.Visible = false;
            // 
            // rdbtnRegion
            // 
            this.rdbtnRegion.Location = new System.Drawing.Point(10, 111);
            this.rdbtnRegion.Name = "rdbtnRegion";
            this.rdbtnRegion.Size = new System.Drawing.Size(128, 18);
            this.rdbtnRegion.TabIndex = 2;
            this.rdbtnRegion.TabStop = false;
            this.rdbtnRegion.Text = "Found - Region Differ";
            // 
            // radLabel5
            // 
            this.radLabel5.Location = new System.Drawing.Point(236, 102);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(28, 18);
            this.radLabel5.TabIndex = 19;
            this.radLabel5.Text = "Year";
            this.radLabel5.Visible = false;
            // 
            // rdbtnValue
            // 
            this.rdbtnValue.Location = new System.Drawing.Point(10, 84);
            this.rdbtnValue.Name = "rdbtnValue";
            this.rdbtnValue.Size = new System.Drawing.Size(161, 18);
            this.rdbtnValue.TabIndex = 1;
            this.rdbtnValue.TabStop = false;
            this.rdbtnValue.Text = "Found - Duplicated Ticket(s)";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(226, 77);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(38, 18);
            this.radLabel1.TabIndex = 4;
            this.radLabel1.Text = "Period";
            this.radLabel1.Visible = false;
            // 
            // rdbtnNotFound
            // 
            this.rdbtnNotFound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rdbtnNotFound.Location = new System.Drawing.Point(9, 57);
            this.rdbtnNotFound.Name = "rdbtnNotFound";
            this.rdbtnNotFound.Size = new System.Drawing.Size(74, 18);
            this.rdbtnNotFound.TabIndex = 0;
            this.rdbtnNotFound.Text = "Not Found";
            this.rdbtnNotFound.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // ddlPeriod
            // 
            this.ddlPeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlPeriod.DataSource = this.bindingSourcePeriod;
            this.ddlPeriod.DisplayMember = "MonthName";
            this.ddlPeriod.Location = new System.Drawing.Point(270, 77);
            this.ddlPeriod.Name = "ddlPeriod";
            this.ddlPeriod.Size = new System.Drawing.Size(125, 20);
            this.ddlPeriod.TabIndex = 3;
            this.ddlPeriod.ValueMember = "MonthOfTheYearId";
            this.ddlPeriod.Visible = false;
            // 
            // bindingSourcePeriod
            // 
            this.bindingSourcePeriod.DataSource = typeof(SMARTBillingReconciliation.MonthOfTheYear);
            // 
            // btnPreview
            // 
            this.btnPreview.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnPreview.Image = global::SMARTBillingReconciliation.Properties.Resources.file_manager;
            this.btnPreview.Location = new System.Drawing.Point(30, 153);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(93, 30);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "Preview";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // rdBtnFound
            // 
            this.rdBtnFound.Location = new System.Drawing.Point(10, 24);
            this.rdBtnFound.Name = "rdBtnFound";
            this.rdBtnFound.Size = new System.Drawing.Size(124, 18);
            this.rdBtnFound.TabIndex = 21;
            this.rdBtnFound.Text = "Found Transaction(s)";
            // 
            // radSeparator1
            // 
            this.radSeparator1.Location = new System.Drawing.Point(11, 44);
            this.radSeparator1.Name = "radSeparator1";
            this.radSeparator1.Size = new System.Drawing.Size(253, 10);
            this.radSeparator1.TabIndex = 22;
            // 
            // FrmExceptionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 191);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.radGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExceptionList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Exception List";
            this.Load += new System.EventHandler(this.FrmExceptionList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maskYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdbtnRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdbtnValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdbtnNotFound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdBtnFound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadRadioButton rdbtnRegion;
        private Telerik.WinControls.UI.RadRadioButton rdbtnValue;
        private Telerik.WinControls.UI.RadRadioButton rdbtnNotFound;
        private Telerik.WinControls.UI.RadButton btnPreview;
        private Telerik.WinControls.UI.RadDropDownList ddlPeriod;
        private System.Windows.Forms.BindingSource bindingSourcePeriod;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadMaskedEditBox maskYear;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadSeparator radSeparator1;
        private Telerik.WinControls.UI.RadRadioButton rdBtnFound;
    }
}
