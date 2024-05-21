namespace SMARTBillingReconciliation.Transaction
{
    partial class FrmClawBack
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
            this.btnUpdate = new Telerik.WinControls.UI.RadButton();
            this.txtTicketNo = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.ddlPeriod = new Telerik.WinControls.UI.RadDropDownList();
            this.bindingSourceMonth = new System.Windows.Forms.BindingSource(this.components);
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTicketNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.Location = new System.Drawing.Point(12, 67);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 26);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Perform Clawback";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtTicketNo
            // 
            this.txtTicketNo.Location = new System.Drawing.Point(120, 8);
            this.txtTicketNo.Name = "txtTicketNo";
            this.txtTicketNo.Size = new System.Drawing.Size(188, 20);
            this.txtTicketNo.TabIndex = 4;
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(20, 9);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(94, 18);
            this.radLabel1.TabIndex = 3;
            this.radLabel1.Text = "Service Order No.";
            // 
            // ddlPeriod
            // 
            this.ddlPeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlPeriod.DataSource = this.bindingSourceMonth;
            this.ddlPeriod.DisplayMember = "MonthName";
            this.ddlPeriod.Location = new System.Drawing.Point(120, 34);
            this.ddlPeriod.Name = "ddlPeriod";
            this.ddlPeriod.Size = new System.Drawing.Size(153, 20);
            this.ddlPeriod.TabIndex = 11;
            this.ddlPeriod.ValueMember = "MonthOfTheYearId";
            // 
            // bindingSourceMonth
            // 
            this.bindingSourceMonth.DataSource = typeof(SMARTBillingReconciliation.MonthOfTheYear);
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(76, 34);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(38, 18);
            this.radLabel2.TabIndex = 10;
            this.radLabel2.Text = "Period";
            // 
            // FrmClawBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 103);
            this.Controls.Add(this.ddlPeriod);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtTicketNo);
            this.Controls.Add(this.radLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmClawBack";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Claw Back";
            this.Load += new System.EventHandler(this.FrmClawBack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTicketNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnUpdate;
        private Telerik.WinControls.UI.RadTextBox txtTicketNo;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList ddlPeriod;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private System.Windows.Forms.BindingSource bindingSourceMonth;
    }
}
