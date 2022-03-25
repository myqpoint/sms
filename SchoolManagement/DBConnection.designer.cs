namespace Debono
{
    partial class DBConnection
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
            this.dbConnect1 = new ReconControl.DBConnect();
            this.SuspendLayout();
            // 
            // dbConnect1
            // 
            this.dbConnect1._IsRestart = false;
            this.dbConnect1.AuthenticationName = "Windows Authentication";
            this.dbConnect1.CmbServerName = "SUSHANTW8\\SQLEXPRESS";
            this.dbConnect1.Dbname = "";
            this.dbConnect1.Location = new System.Drawing.Point(2, 2);
            this.dbConnect1.MdfFilePath = "C:\\Program Files\\Microsoft SQL Server\\MSSQL10_50.MSSQLSERVER\\MSSQL\\DATA\\Recon.mdf" +
    "";
            this.dbConnect1.Name = "dbConnect1";
            this.dbConnect1.Password = "comcad";
            this.dbConnect1.Size = new System.Drawing.Size(503, 282);
            this.dbConnect1.TabIndex = 0;
            this.dbConnect1.UserName = "sa";
            this.dbConnect1.Load += new System.EventHandler(this.dbConnect1_Load);
            // 
            // DBConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 284);
            this.Controls.Add(this.dbConnect1);
            this.Name = "DBConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DBConnection";
            this.ResumeLayout(false);

        }

        #endregion

        public  ReconControl.DBConnect dbConnect1;
    }
}