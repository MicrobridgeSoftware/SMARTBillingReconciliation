namespace SMARTBillingReconciliation
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.mnuPerformRecon = new Telerik.WinControls.UI.RadMenuItem();
            this.mnuJobPayment = new Telerik.WinControls.UI.RadMenuItem();
            this.mnuClawback = new Telerik.WinControls.UI.RadMenuItem();
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            this.mnuException = new Telerik.WinControls.UI.RadMenuItem();
            this.mnuFound = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenu = new Telerik.WinControls.UI.RadMenu();
            this.mnuManualPresentation = new Telerik.WinControls.UI.RadMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuPerformRecon
            // 
            this.mnuPerformRecon.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.mnuJobPayment,
            this.mnuClawback});
            this.mnuPerformRecon.Name = "mnuPerformRecon";
            this.mnuPerformRecon.Text = "Perform Reconciliation";
            this.mnuPerformRecon.Click += new System.EventHandler(this.mnuPerformRecon_Click);
            // 
            // mnuJobPayment
            // 
            this.mnuJobPayment.Name = "mnuJobPayment";
            this.mnuJobPayment.Text = "Job Payment";
            this.mnuJobPayment.Click += new System.EventHandler(this.mnuJobPayment_Click);
            // 
            // mnuClawback
            // 
            this.mnuClawback.Name = "mnuClawback";
            this.mnuClawback.Text = "Claw Back";
            this.mnuClawback.Click += new System.EventHandler(this.mnuClawback_Click);
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 244);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Size = new System.Drawing.Size(529, 26);
            this.radStatusStrip1.TabIndex = 1;
            // 
            // mnuException
            // 
            this.mnuException.Name = "mnuException";
            this.mnuException.Text = "Reconciliation Exceptions";
            this.mnuException.Click += new System.EventHandler(this.mnuException_Click);
            // 
            // mnuFound
            // 
            this.mnuFound.Name = "mnuFound";
            this.mnuFound.Text = "Transaction Status";
            this.mnuFound.Click += new System.EventHandler(this.mnuFound_Click);
            // 
            // radMenu
            // 
            this.radMenu.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.mnuPerformRecon,
            this.mnuException,
            this.mnuFound,
            this.mnuManualPresentation});
            this.radMenu.Location = new System.Drawing.Point(0, 0);
            this.radMenu.Name = "radMenu";
            this.radMenu.Size = new System.Drawing.Size(529, 40);
            this.radMenu.TabIndex = 0;
            // 
            // mnuManualPresentation
            // 
            this.mnuManualPresentation.Name = "mnuManualPresentation";
            this.mnuManualPresentation.Text = "Manual Presentation";
            this.mnuManualPresentation.Click += new System.EventHandler(this.mnuManualPresentation_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 270);
            this.Controls.Add(this.radStatusStrip1);
            this.Controls.Add(this.radMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainWindow";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "SMART Billing Reconciliation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Telerik.WinControls.UI.RadMenuItem mnuPerformRecon;
        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        private Telerik.WinControls.UI.RadMenuItem mnuException;
        private Telerik.WinControls.UI.RadMenu radMenu;
        private Telerik.WinControls.UI.RadMenuItem mnuFound;
        private Telerik.WinControls.UI.RadMenuItem mnuManualPresentation;
        private Telerik.WinControls.UI.RadMenuItem mnuJobPayment;
        private Telerik.WinControls.UI.RadMenuItem mnuClawback;
    }
}
