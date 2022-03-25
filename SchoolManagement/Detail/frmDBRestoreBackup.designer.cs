namespace Debono
{
    partial class frmDBRestoreBackup
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnRestore = new DevExpress.XtraEditors.SimpleButton();
            this.btnBackup = new DevExpress.XtraEditors.SimpleButton();
            this.lblFile = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtSrvLocalDir = new DevExpress.XtraEditors.TextEdit();
            this.FileDlg = new System.Windows.Forms.OpenFileDialog();
            this.xtBackup = new DevExpress.XtraTab.XtraTabControl();
            this.xtpBackup = new DevExpress.XtraTab.XtraTabPage();
            this.txtBackupHist = new DevExpress.XtraEditors.MemoEdit();
            this.xtpRestore = new DevExpress.XtraTab.XtraTabPage();
            this.txtRestoreHist = new DevExpress.XtraEditors.MemoEdit();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.fdBackupFile = new System.Windows.Forms.SaveFileDialog();
            this.fdZipFiles = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.txtSrvLocalDir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtBackup)).BeginInit();
            this.xtBackup.SuspendLayout();
            this.xtpBackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupHist.Properties)).BeginInit();
            this.xtpRestore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRestoreHist.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.Location = new System.Drawing.Point(516, 126);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(70, 32);
            this.btnRestore.TabIndex = 14;
            this.btnRestore.Text = "&Restore";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackup.Location = new System.Drawing.Point(612, 126);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(70, 32);
            this.btnBackup.TabIndex = 12;
            this.btnBackup.Text = "&Backup";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // lblFile
            // 
            this.lblFile.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFile.Location = new System.Drawing.Point(13, 66);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(145, 16);
            this.lblFile.TabIndex = 207;
            this.lblFile.Text = "Restore File (*.bak files )";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(13, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(130, 16);
            this.labelControl1.TabIndex = 209;
            this.labelControl1.Text = "DB Server Local Folder";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(708, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 32);
            this.btnCancel.TabIndex = 210;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtSrvLocalDir
            // 
            this.txtSrvLocalDir.Location = new System.Drawing.Point(12, 87);
            this.txtSrvLocalDir.Name = "txtSrvLocalDir";
            this.txtSrvLocalDir.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSrvLocalDir.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtSrvLocalDir.Properties.AutoHeight = false;
            this.txtSrvLocalDir.Properties.ReadOnly = true;
            this.txtSrvLocalDir.Size = new System.Drawing.Size(355, 22);
            this.txtSrvLocalDir.TabIndex = 213;
            this.txtSrvLocalDir.EditValueChanged += new System.EventHandler(this.txtSrvLocalDir_EditValueChanged);
            // 
            // FileDlg
            // 
            this.FileDlg.FileName = "openFileDialog1";
            // 
            // xtBackup
            // 
            this.xtBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtBackup.Location = new System.Drawing.Point(12, 144);
            this.xtBackup.Name = "xtBackup";
            this.xtBackup.SelectedTabPage = this.xtpBackup;
            this.xtBackup.Size = new System.Drawing.Size(771, 326);
            this.xtBackup.TabIndex = 215;
            this.xtBackup.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpBackup,
            this.xtpRestore});
            // 
            // xtpBackup
            // 
            this.xtpBackup.Controls.Add(this.txtBackupHist);
            this.xtpBackup.Name = "xtpBackup";
            this.xtpBackup.Size = new System.Drawing.Size(765, 298);
            this.xtpBackup.Text = "Backup";
            // 
            // txtBackupHist
            // 
            this.txtBackupHist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBackupHist.Location = new System.Drawing.Point(0, 0);
            this.txtBackupHist.Name = "txtBackupHist";
            this.txtBackupHist.Properties.Appearance.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackupHist.Properties.Appearance.Options.UseFont = true;
            this.txtBackupHist.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtBackupHist.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtBackupHist.Properties.ReadOnly = true;
            this.txtBackupHist.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBackupHist.Properties.WordWrap = false;
            this.txtBackupHist.Size = new System.Drawing.Size(765, 298);
            this.txtBackupHist.TabIndex = 213;
            // 
            // xtpRestore
            // 
            this.xtpRestore.Controls.Add(this.txtRestoreHist);
            this.xtpRestore.Name = "xtpRestore";
            this.xtpRestore.Size = new System.Drawing.Size(765, 298);
            this.xtpRestore.Text = "Restore";
            // 
            // txtRestoreHist
            // 
            this.txtRestoreHist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRestoreHist.Location = new System.Drawing.Point(0, 0);
            this.txtRestoreHist.Name = "txtRestoreHist";
            this.txtRestoreHist.Properties.Appearance.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRestoreHist.Properties.Appearance.Options.UseFont = true;
            this.txtRestoreHist.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtRestoreHist.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtRestoreHist.Properties.ReadOnly = true;
            this.txtRestoreHist.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRestoreHist.Properties.WordWrap = false;
            this.txtRestoreHist.Size = new System.Drawing.Size(765, 298);
            this.txtRestoreHist.TabIndex = 213;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(12, 44);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(147, 21);
            this.txtFileName.TabIndex = 216;
            this.txtFileName.Text = "D:\\";
            this.txtFileName.TextChanged += new System.EventHandler(this.txtFileName_TextChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(388, 86);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(27, 23);
            this.simpleButton1.TabIndex = 218;
            this.simpleButton1.Text = "...";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // fdBackupFile
            // 
            this.fdBackupFile.FileOk += new System.ComponentModel.CancelEventHandler(this.fdBackupFile_FileOk);
            // 
            // fdZipFiles
            // 
           // this.fdZipFiles.FileOk += new System.ComponentModel.CancelEventHandler(this.fdZipFiles_FileOk);
            // 
            // frmDBRestoreBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 482);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.xtBackup);
            this.Controls.Add(this.txtSrvLocalDir);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lblFile);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDBRestoreBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Backup/Restore";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.frmDBRestoreBackup_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDBRestoreBackup_FormClosing);
            this.Load += new System.EventHandler(this.frmDBRestoreBackup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSrvLocalDir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtBackup)).EndInit();
            this.xtBackup.ResumeLayout(false);
            this.xtpBackup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupHist.Properties)).EndInit();
            this.xtpRestore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRestoreHist.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private DevExpress.XtraEditors.SimpleButton btnRestore;
        private DevExpress.XtraEditors.SimpleButton btnBackup;
        private DevExpress.XtraEditors.LabelControl lblFile;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtSrvLocalDir;
        private System.Windows.Forms.OpenFileDialog FileDlg;
        private DevExpress.XtraTab.XtraTabControl xtBackup;
        private DevExpress.XtraTab.XtraTabPage xtpBackup;
        private DevExpress.XtraEditors.MemoEdit txtBackupHist;
        private DevExpress.XtraTab.XtraTabPage xtpRestore;
        private DevExpress.XtraEditors.MemoEdit txtRestoreHist;
        private System.Windows.Forms.TextBox txtFileName;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.SaveFileDialog fdBackupFile;
        private System.Windows.Forms.SaveFileDialog fdZipFiles;
    }
}