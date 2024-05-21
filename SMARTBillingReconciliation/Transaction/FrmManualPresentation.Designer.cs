namespace SMARTBillingReconciliation.Transaction
{
    partial class FrmManualPresentation
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
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtTicketNo = new Telerik.WinControls.UI.RadTextBox();
            this.btnUpdate = new Telerik.WinControls.UI.RadButton();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.rdBtnJobClaims = new Telerik.WinControls.UI.RadRadioButton();
            this.rdBtnSurvey123 = new Telerik.WinControls.UI.RadRadioButton();
            this.radSeparator1 = new Telerik.WinControls.UI.RadSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTicketNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdBtnJobClaims)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdBtnSurvey123)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(13, 45);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(94, 18);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Service Order No.";
            // 
            // txtTicketNo
            // 
            this.txtTicketNo.Location = new System.Drawing.Point(110, 44);
            this.txtTicketNo.Name = "txtTicketNo";
            this.txtTicketNo.Size = new System.Drawing.Size(191, 20);
            this.txtTicketNo.TabIndex = 1;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.Location = new System.Drawing.Point(112, 79);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 26);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update Status";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(18, 7);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(51, 18);
            this.radLabel2.TabIndex = 3;
            this.radLabel2.Text = "Category";
            // 
            // rdBtnJobClaims
            // 
            this.rdBtnJobClaims.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rdBtnJobClaims.Location = new System.Drawing.Point(110, 7);
            this.rdBtnJobClaims.Name = "rdBtnJobClaims";
            this.rdBtnJobClaims.Size = new System.Drawing.Size(69, 18);
            this.rdBtnJobClaims.TabIndex = 4;
            this.rdBtnJobClaims.Text = "Job Claim";
            this.rdBtnJobClaims.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // rdBtnSurvey123
            // 
            this.rdBtnSurvey123.Location = new System.Drawing.Point(196, 7);
            this.rdBtnSurvey123.Name = "rdBtnSurvey123";
            this.rdBtnSurvey123.Size = new System.Drawing.Size(75, 18);
            this.rdBtnSurvey123.TabIndex = 5;
            this.rdBtnSurvey123.TabStop = false;
            this.rdBtnSurvey123.Text = "Survey 123";
            // 
            // radSeparator1
            // 
            this.radSeparator1.Location = new System.Drawing.Point(75, 27);
            this.radSeparator1.Name = "radSeparator1";
            this.radSeparator1.Size = new System.Drawing.Size(226, 10);
            this.radSeparator1.TabIndex = 6;
            // 
            // FrmManualPresentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 116);
            this.Controls.Add(this.radSeparator1);
            this.Controls.Add(this.rdBtnSurvey123);
            this.Controls.Add(this.rdBtnJobClaims);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtTicketNo);
            this.Controls.Add(this.radLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManualPresentation";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Manual Presentation";
            this.Load += new System.EventHandler(this.FrmManualPresentation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTicketNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdBtnJobClaims)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdBtnSurvey123)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtTicketNo;
        private Telerik.WinControls.UI.RadButton btnUpdate;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadRadioButton rdBtnJobClaims;
        private Telerik.WinControls.UI.RadRadioButton rdBtnSurvey123;
        private Telerik.WinControls.UI.RadSeparator radSeparator1;
    }
}
