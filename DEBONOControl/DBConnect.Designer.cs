namespace ReconControl
{
    partial class DBConnect
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBConnect));
            this.label6 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.rbnSql = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.rbnMDF = new System.Windows.Forms.RadioButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnSaveConnection = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDBName = new System.Windows.Forms.ComboBox();
            this.cmbAuthentication = new System.Windows.Forms.ComboBox();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.progressBarSample = new DevExpress.XtraEditors.ProgressBarControl();
            this.btnMdfPath = new System.Windows.Forms.Button();
            this.timerInternal = new System.Windows.Forms.Timer(this.components);
            this.txtMdfPath = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarSample.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 58;
            this.label6.Text = "Authentication :";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(439, 199);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(35, 35);
            this.btnRefresh.TabIndex = 68;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // rbnSql
            // 
            this.rbnSql.AutoSize = true;
            this.rbnSql.Location = new System.Drawing.Point(254, 34);
            this.rbnSql.Name = "rbnSql";
            this.rbnSql.Size = new System.Drawing.Size(74, 17);
            this.rbnSql.TabIndex = 62;
            this.rbnSql.TabStop = true;
            this.rbnSql.Text = "Sql Server";
            this.rbnSql.UseVisualStyleBackColor = true;
            this.rbnSql.CheckedChanged += new System.EventHandler(this.rbnSql_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Database:";
            // 
            // rbnMDF
            // 
            this.rbnMDF.AutoSize = true;
            this.rbnMDF.Location = new System.Drawing.Point(89, 34);
            this.rbnMDF.Name = "rbnMDF";
            this.rbnMDF.Size = new System.Drawing.Size(67, 17);
            this.rbnMDF.TabIndex = 61;
            this.rbnMDF.TabStop = true;
            this.rbnMDF.Text = "MDF File";
            this.rbnMDF.UseVisualStyleBackColor = true;
            this.rbnMDF.CheckedChanged += new System.EventHandler(this.rbnMDF_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(31, 239);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(125, 35);
            this.btnSearch.TabIndex = 67;
            this.btnSearch.Text = "Auto Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Password :";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(387, 239);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(85, 35);
            this.simpleButton1.TabIndex = 66;
            this.simpleButton1.Text = "Cancel";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(172, 179);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(265, 20);
            this.txtPass.TabIndex = 55;
            this.txtPass.Text = "comcad";
            // 
            // btnSaveConnection
            // 
            this.btnSaveConnection.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveConnection.Image")));
            this.btnSaveConnection.Location = new System.Drawing.Point(181, 239);
            this.btnSaveConnection.Name = "btnSaveConnection";
            this.btnSaveConnection.Size = new System.Drawing.Size(190, 35);
            this.btnSaveConnection.TabIndex = 65;
            this.btnSaveConnection.Text = "Save Connection";
            this.btnSaveConnection.Click += new System.EventHandler(this.btnSaveConnection_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "User Name :";
            // 
            // cmbDBName
            // 
            this.cmbDBName.FormattingEnabled = true;
            this.cmbDBName.Location = new System.Drawing.Point(172, 206);
            this.cmbDBName.Name = "cmbDBName";
            this.cmbDBName.Size = new System.Drawing.Size(265, 21);
            this.cmbDBName.TabIndex = 64;
            this.cmbDBName.SelectedIndexChanged += new System.EventHandler(this.cmbDBName_SelectedIndexChanged);
            // 
            // cmbAuthentication
            // 
            this.cmbAuthentication.FormattingEnabled = true;
            this.cmbAuthentication.Items.AddRange(new object[] {
            "Windows Authentication",
            "Sql Server Authentication"});
            this.cmbAuthentication.Location = new System.Drawing.Point(172, 125);
            this.cmbAuthentication.Name = "cmbAuthentication";
            this.cmbAuthentication.Size = new System.Drawing.Size(265, 21);
            this.cmbAuthentication.TabIndex = 59;
            this.cmbAuthentication.SelectedIndexChanged += new System.EventHandler(this.cmbAuthentication_SelectedIndexChanged);
            // 
            // cmbServer
            // 
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Location = new System.Drawing.Point(172, 96);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(265, 21);
            this.cmbServer.TabIndex = 63;
            this.cmbServer.SelectedIndexChanged += new System.EventHandler(this.cmbServer_SelectedIndexChanged);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(172, 152);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(265, 20);
            this.txtUser.TabIndex = 52;
            //this.txtUser.Text = "sa";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "MDF File Path :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Server :";
            // 
            // timer
            // 
            this.timer.Interval = 50;
            // 
            // progressBarSample
            // 
            this.progressBarSample.EditValue = 100;
            this.progressBarSample.Location = new System.Drawing.Point(144, 242);
            this.progressBarSample.Name = "progressBarSample";
            this.progressBarSample.Properties.Maximum = 200;
            this.progressBarSample.Size = new System.Drawing.Size(10, 20);
            this.progressBarSample.TabIndex = 49;
            // 
            // btnMdfPath
            // 
            this.btnMdfPath.Location = new System.Drawing.Point(440, 68);
            this.btnMdfPath.Name = "btnMdfPath";
            this.btnMdfPath.Size = new System.Drawing.Size(26, 23);
            this.btnMdfPath.TabIndex = 53;
            this.btnMdfPath.Text = "...";
            this.btnMdfPath.UseVisualStyleBackColor = true;
            this.btnMdfPath.Click += new System.EventHandler(this.btnMdfPath_Click);
            // 
            // timerInternal
            // 
            this.timerInternal.Enabled = true;
            this.timerInternal.Interval = 25;
            // 
            // txtMdfPath
            // 
            this.txtMdfPath.Location = new System.Drawing.Point(172, 68);
            this.txtMdfPath.Name = "txtMdfPath";
            this.txtMdfPath.Size = new System.Drawing.Size(265, 20);
            this.txtMdfPath.TabIndex = 57;
            this.txtMdfPath.Text = "C:\\Program Files\\Microsoft SQL Server\\MSSQL10_50.MSSQLSERVER\\MSSQL\\DATA\\Recon.mdf" +
                "";
            // 
            // DBConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.rbnSql);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbnMDF);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.btnSaveConnection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDBName);
            this.Controls.Add(this.cmbAuthentication);
            this.Controls.Add(this.cmbServer);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBarSample);
            this.Controls.Add(this.btnMdfPath);
            this.Controls.Add(this.txtMdfPath);
            this.Name = "DBConnect";
            this.Size = new System.Drawing.Size(505, 308);
            this.Load += new System.EventHandler(this.Startupformc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarSample.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RadioButton rbnSql;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbnMDF;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.TextBox txtPass;
        private DevExpress.XtraEditors.SimpleButton btnSaveConnection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDBName;
        private System.Windows.Forms.ComboBox cmbAuthentication;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer;
        private DevExpress.XtraEditors.ProgressBarControl progressBarSample;
        private System.Windows.Forms.Button btnMdfPath;
        private System.Windows.Forms.Timer timerInternal;
        private System.Windows.Forms.TextBox txtMdfPath;
    }
}
