using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using AcctControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Security.AccessControl;
using System.Security.Principal;
using Debono;
using DebonoDLL;
using DebonoDLL.App_Code.DAL;
using DebonoDLL.App_Code.BOL;
using DEBONO;
using System.Net;
using System.Net.Sockets;
using Ionic.Zip;
using DebonoDLL.BOL;


namespace Debono
{
    public partial class frmDBRestoreBackup : DevExpress.XtraEditors.XtraForm
    {
        private long lFilesize = 0;
        private bool bBackup = true;
        String strConnString = "";
        Conversion objCon = new Conversion();
        public frmDBRestoreBackup(bool bBak = true)
        {            
            InitializeComponent();
            strConnString = objCon.ConToStr(System.Configuration.ConfigurationSettings.AppSettings["connectionString"]);

            bBackup = bBak;
            // btnBackup.Visible = bBackup;
            //btnRestore.Visible = !bBackup;

            this.Text = (bBackup ? "Backup Data" : "Restore Data");
            lblFile.Text = (bBackup ? "Backup File to be Created" : "Backup File to Restore Data From");

            if (!bBackup) txtFileName.Text = "";

            txtBackupHist.EditValue = Bak_Hist();
            txtRestoreHist.EditValue = Rest_Hist();
        }


        private void btnBackup_Click(object sender, EventArgs e)
        {
            int iErr = 0;
            try
            {
                CommonFunction.DefaultCursor();
                if (true)
                {
                    CommonFunction.WaitCursor();
                    int i = 0;

                    iErr = BackupDataBase();

                    /*
                    if (iErr == 0)
                    {
                        MessageBox.Show("Saved.");

                    }
                    else
                    {

                        MessageBox.Show("Not Saved.");

                    }
                    */
                    /*

            if (iErr == 0)
            {
                try
                {

                    #region Log backup details to local station backup history


                    sFileSize = GeneralFunctions.SizeInBytes(lFilesize);
                    // Backup complated, update backup history.
                    // First, load previously saved backup history
                    string sParam = Settings.LoadParam("Backup", "W") + ";;;;;;;;;;;;";
                    string[] sBits = sParam.Split(';');

                    // Now move history entries down by 1, loosing the oldest entry

                    for (i = (sBits.Length - 1); i > 0; i--)
                    {
                        sBits[i] = sBits[i - 1];
                    }

                    sBits[0] = Path.GetDirectoryName(txtFileName.Text);                 // Last backup folder

                    // Save latest entry in following format
                    // Timestamp ~ Workstation ~ UserLogin ~ FileSize ~ BackupFileName
                    string sNewLog = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "~";
                    sNewLog += gv.PCName + "~";
                    sNewLog += gv.UserLogin + "~";
                    sNewLog += sFileSize + "~";
                    sNewLog += txtFileName.Text + "~";
                    sBits[1] = sNewLog;

                    // Now recombine history entries into a single string for saving
                    string sTxt = "";

                    for (i = 0; i < 11; i++)
                    {
                        sTxt += sBits[i] + ";";
                    }

                    SaveParam("Backup", "W", sTxt);                     // Save backup history - specific to current work station

                    #endregion

                    #region Also log backup details to Global Backup History Log
                    // Now repeat the process and save to the Global (all work stations) history

                    sParam = Settings.LoadParam("Backup", "G") + ";;;;;;;;;;;;";
                    sBits = sParam.Split(';');

                    // Now move history entries down by 1, loosing the oldest entry

                    for (i = (sBits.Length - 1); i > 0; i--)
                    {
                        sBits[i] = sBits[i - 1];
                    }

                    sBits[0] = Path.GetDirectoryName(txtFileName.Text);                 // Last backup folder

                    // Save latest entry in following format
                    // Timestamp ~ Workstation ~ UserLogin ~ BackupFileName
                    sBits[1] = sNewLog;

                    // Now recombine history entries into a single string for saving
                    sTxt = "";

                    for (i = 0; i < 11; i++)
                    {
                        sTxt += sBits[i] + ";";
                    }

                    SaveParam("Backup", "G", sTxt);                     // Save backup history - Global - all work stations
                    #endregion
                             
                }
                catch (Exception ex)
                {
                    DebonoMsg.MsgError("Saving backup history !");
                }
                            
            }  */
                }
                else
                {
                    DebonoMsg.MsgError("No Write Permission on Database Server !");
                    return;
                }

            }
            catch (Exception ex)
            {
                DebonoMsg.MsgError("Data Backup Failed !");
                ExceptionManager.LogException(ex);
                iErr = -1;
            }         
            if (iErr == 0) DebonoMsg.MsgInformation("Database backup completed .");
        }

        private void RestoreDataBase(string FilePath)
        {
            string ConnectionString = SystemVariables.co_connstring;
            string[] atrributes = ConnectionString.Split(';');
            string sqlQuery = "Use Master \n ";
            sqlQuery += "Alter Database [" + atrributes[1].Split('=')[1] + "] SET SINGLE_USER With ROLLBACK IMMEDIATE \n";
            sqlQuery += "RESTORE DATABASE [" + atrributes[1].Split('=')[1] + "]  FROM DISK = '" + FilePath + "'";
            Dal obj = new Dal();
            obj.ExecuteData(sqlQuery);
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (true)
                {
                    if (String.IsNullOrEmpty(txtSrvLocalDir.Text.Trim()))
                    {
                        DebonoMsg.MsgInformation("Please select backup file first.");
                        return;
                    }

                    CommonFunction.WaitCursor();
                    int resule = RestoreDataBase();
                    CommonFunction.DefaultCursor();
                    if (resule > 0)
                    {
                        DebonoMsg.MsgInformation("Database restored successfully.");
                    }
                }
                else
                {
                    DebonoMsg.MsgInformation("No Write Permission on Database Server !");
                    return;
                }
            }
            catch (Exception ex)
            {
                DebonoMsg.MsgError(ex.Message);
            }

        }

        private void btnLocalFileName_Click(object sender, EventArgs e)
        {
            if (bBackup)
            {
                SaveFileDialog objSave = new SaveFileDialog();
                objSave.Title = "Save Backup Files";
                objSave.DefaultExt = ".bak";
                objSave.Filter = "Back-up files (*.bak)|*.bak|All files (*.*)|*.*";
                objSave.FilterIndex = 1;
                //objSave.CheckFileExists = true;
                objSave.CheckPathExists = true;
                objSave.RestoreDirectory = true;
                objSave.ShowDialog();
                if (objSave.FileName != "")
                    txtFileName.Text = objSave.FileName;
            }
            else
            {

                OpenFileDialog objOpen = new OpenFileDialog();
                objOpen.Title = "Browse Backup Files";
                objOpen.CheckFileExists = true;
                objOpen.CheckPathExists = true;
                objOpen.DefaultExt = "bak";
                objOpen.Filter = "Back-up files (*.bak)|*.bak|All files (*.*)|*.*";
                objOpen.FilterIndex = 2;
                objOpen.RestoreDirectory = true;
                objOpen.ReadOnlyChecked = true;
                objOpen.ShowReadOnly = true;
                objOpen.ShowDialog();
                if (objOpen.FileName != "")
                    txtFileName.Text = objOpen.FileName;
            }
        }

        private void CopyFile(Boolean FromServer)
        {
            String DestinationFileName = "";
            String SourceFileName = "";
            String FileName = Path.GetFileName(txtFileName.Text);
            if (FromServer)
            {
                DestinationFileName = txtFileName.Text;
                SourceFileName = txtSrvLocalDir.Text + "//" + FileName;
            }
            else
            {
                DestinationFileName = txtSrvLocalDir.Text + "//" + FileName;
                SourceFileName = txtFileName.Text;
            }

            File.Copy(SourceFileName, DestinationFileName, true);
        }

       

        protected void MakeZipFile(string sourPath,String destpath)
        {

            string ZipFileToCreate = destpath;

            try
            {

                CommonFunction.WaitCursor();
                string uploadPath = sourPath;

                // DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath(uploadPath));
                if (Directory.Exists(uploadPath))
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        ZipEntry e = zip.AddDirectory(uploadPath, "");
                        // e.Comment = "CreatedFiles";
                        zip.Comment = String.Format("This file contain backup of '{0}' and created on '{1}' ", DateTime.UtcNow.ToLongDateString(), System.Net.Dns.GetHostName());
                        zip.Save(destpath);

                    }


                }
                CommonFunction.DefaultCursor();
            }
            catch (Exception exx)
            {
                DebonoMsg.MsgError("Data Backup Failed !");
                ExceptionManager.LogException(exx);

            }

        }

        private int BackupDataBase()
        {
            int iErr = 0;
            lFilesize = 0;

            try
            {
                if (string.IsNullOrEmpty(txtFileName.Text.Trim()))
                {
                    DebonoMsg.MsgError("Backup file name not defined !");
                    return 1;
                }
                
                var connectionString = strConnString;
                var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);
                SqlConnection sqlCon = new SqlConnection(connectionString);

                #region create backupfile
                string[] atrributes = connectionString.Split(';');
                // var backupFileName = Path.GetFileName(txtBackupFileName.Text.Trim());//   sqlConStrBuilder.InitialCatalog + "-" + DateTime.Now.ToString("dd-MM-yy") + ".bak";
                var backupFileName = Path.Combine(txtFileName.Text, "Debono.bak");

                // var localFileName = Path.Combine(txtSrvLocalDir.Text, Co.Nm + "_Backup.Bak");

                //if (File.Exists(backupFileName))
                //{
                //    File.Delete(backupFileName);
                //}


                using (var connection = new SqlConnection(connectionString))
                {
                    var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}' WITH INIT", sqlConStrBuilder.InitialCatalog, backupFileName);

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                #endregion

                #region copy backup file to local path
                fdBackupFile.Filter = "SQL Server database backup files|*.bak";
                fdBackupFile.Title = "Create Database Backup";
                fdBackupFile.ShowDialog();

                #endregion
            }
            catch (Exception ex)
            {
                iErr = 3;
                ExceptionManager.LogException(ex.Message);
            }

            return iErr;
        }

        private void fdBackupFile_FileOk(object sender, CancelEventArgs e)
        {
            var connectionString = strConnString;
            var distFile = fdBackupFile.FileName;
            string[] atrributes = connectionString.Split(';');

            SQLLocalBackup _backupClass = new SQLLocalBackup(atrributes[0].Split('=')[1], atrributes[2].Split('=')[1], atrributes[3].Split('=')[1], atrributes[1].Split('=')[1], connectionString);
            _backupClass.DoLocalBackup(txtFileName.Text.Trim(), distFile);

        }



        public bool HasWritePermissionOnDir()
        {
            try
            {

                //string directory = App.wsSharedDir;
                //DirectoryInfo di = new DirectoryInfo(directory);


                string directory = txtSrvLocalDir.Text;
                DirectoryInfo di = new DirectoryInfo(directory);


                DirectorySecurity ds = di.GetAccessControl();
                bool Permission = false;
                foreach (AccessRule rule in ds.GetAccessRules(true, true, typeof(NTAccount)))
                {
                    Console.WriteLine("Identity = {0}; Access = {1}",
                                  rule.IdentityReference.Value, rule.AccessControlType);
                    Permission = true;
                }
                return Permission;
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex.Message);
                return false;
            }
        }

        private int RestoreDataBase()
        {
            try
            {
                var connectionString = SystemVariables.co_connstring;
                string sFileSize = "";
                string sFileName = txtFileName.Text.Trim();

                var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);
                SqlConnection sqlCon = new SqlConnection(connectionString);
                string RestorePath = Path.GetFileName(sFileName);// sqlConStrBuilder.InitialCatalog + "-" + DateTime.Now.ToString("dd-MM-yy") + ".bak";

                #region copy to server the selected bak file

                // Backup file created on the server is always of the same name, CoName_Backup.Bak
                // When restoring, do not delete this file, instead create a new file, CoName_Restore.Bak
                string sDestFile = Co.Nm + "_Restore.Bak";
                string sFullPathName = Path.Combine(App.wsDBSrvSharedDir, sDestFile);

                DirectoryInfo dirInfoLocal = new DirectoryInfo(txtFileName.Text.Replace(Path.GetFileName(txtFileName.Text.Trim()), ""));
                FileInfo[] Localfiles = dirInfoLocal.GetFiles("*.Bak");

                foreach (FileInfo Localtempfile in Localfiles)
                {
                    if (Localtempfile.Name == RestorePath)
                    {
                        //if (File.Exists(Path.Combine(App.wsDBSrvSharedDir + Localtempfile)))
                        //    File.Delete(App.wsDBSrvSharedDir + Localtempfile.Name);
                        //Localtempfile.CopyTo(Path.Combine(App.wsDBSrvSharedDir, RestorePath));

                        if (File.Exists(sFullPathName))
                        {
                            File.Delete(sFullPathName);
                        }


                        long lFileSize = Localtempfile.Length;
                        sFileSize = GeneralFunctions.SizeInBytes(lFilesize);

                        Localtempfile.CopyTo(Path.Combine(sFullPathName));
                        break;
                    }
                }

                #endregion

                #region Restore database
                //var path = App.wsSharedDir + RestorePath;

                var path = Path.Combine(App.gnSrvLocalDir, sDestFile);

                using (var connection = new SqlConnection(connectionString))
                {
                    int i = 0;
                    string sqlQuery = "Use Master \n ";

                    //previous code
                    // sqlQuery += String.Format("Alter Database {0} SET SINGLE_USER With ROLLBACK IMMEDIATE \n", sqlConStrBuilder.InitialCatalog);
                    // sqlQuery += String.Format("RESTORE DATABASE {0}  FROM DISK = '{1}'", sqlConStrBuilder.InitialCatalog, path);
                    //end previous code
                    sqlQuery += String.Format("RESTORE DATABASE {0}  FROM DISK = '{1}'", sqlConStrBuilder.InitialCatalog + "2", path);
                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    connection.Close();

                    /*

                    #region Log restore details
                    // No exception errors generated so far - all must have gone ok
                    // Log restore details

                    // But first read existing log
                    string sParam = Settings.LoadParam("Restore", "G") + ";;;;;;;;;;;;";
                    string[] sBits = sParam.Split(';');

                    // Now move history entries down by 1, loosing the oldest entry

                    for (i = (sBits.Length - 1); i > 0; i--)
                    {
                        sBits[i] = sBits[i - 1];
                    }

                    string sNewLog = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "~";
                    sNewLog += gv.PCName + "~";
                    sNewLog += gv.UserLogin + "~";
                    sNewLog += sFileSize + "~";
                    sNewLog += sFileName + "~";
                    
                    // Add latest details to top of array
                    sBits[0] = sNewLog;

                    // Now recombine history entries into a single string for saving
                    string sTxt = "";

                    for (i = 0; i < 11; i++)
                    {
                        sTxt += sBits[i] + ";";
                    }

                    SaveParam("Restore", "G", sTxt);                 // Save restore history - Global - all work stations
                }
                    #endregion


               
                */
                }
                #endregion
                return 1;
            }
            catch (Exception ex)
            {
                DebonoMsg.MsgError(ex.Message);
                return -1;
            }

        }

        private string SaveParam(string keyname, string paramcat, string value)
        { // Save parameter to system runtime configuration settings for next session
            string ret = "";
            try
            {

                string usr = "";
                string wrkstn = "";
                bool findparam = false;
                string val = null;
                if (paramcat.Contains("U"))
                {
                    findparam = false;
                    usr = gv.UserID.ToString();
                    SetupParam setp = new SetupParam();
                    setp.SetConn();
                    setp.i_ty = 2;
                    setp.s_key = keyname;
                    setp.s_ref = usr;
                    val = setp.IsExistParam();
                    if (val != null)
                    {
                        findparam = true;
                    }
                    ret = SaveData(findparam, keyname, value, usr, 2);
                }
                if (paramcat.Contains("W") && (ret == ""))
                {
                    findparam = false;
                    wrkstn = gv.PCName;
                    SetupParam setp1 = new SetupParam();
                    setp1.SetConn();
                    setp1.i_ty = 1;
                    setp1.s_key = keyname;
                    setp1.s_ref = wrkstn;
                    val = setp1.IsExistParam();
                    if (val != null)
                    {
                        findparam = true;
                    }
                    ret = SaveData(findparam, keyname, value, wrkstn, 1);
                }

                if (paramcat.Contains("G") && (ret == ""))
                {
                    findparam = false;
                    SetupParam setp2 = new SetupParam();
                    setp2.SetConn();
                    setp2.i_ty = 0;
                    setp2.s_key = keyname;
                    setp2.s_ref = "";
                    val = setp2.IsExistParam();
                    if (val != null)
                    {
                        findparam = true;
                    }
                    ret = SaveData(findparam, keyname, value, "", 0);
                }
            }
            catch (Exception ex)
            {

            }
            return ret;
        }

        private string SaveData(bool isexist, string keyname, string value, string reff, int ty)
        {
            SetupParam setp = new SetupParam();
            setp.SetConn();
            setp.i_ty = ty;
            setp.s_key = keyname;
            setp.s_ref = reff;
            setp.s_val = value;
            if (isexist) return setp.UpdateData();
            else return setp.InsertData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBackupFileName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                FileDlg.Filter = "Backup Files|*.bak|All|*.*";
                FileDlg.FileName = "";
                if (FileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) txtFileName.Text = FileDlg.FileName;
            }
            finally
            {
                FileDlg.Dispose();
            }

            // btnRestore.Enabled = (!bBackup && txtFileName.EditValue != "");
        }

        private string Bak_Hist()
        {
            // Get the folder name for the last backup on this work station
            string[] sBits;
            string[] sBits2;
            string sBakDir = gv.AppDir;
            string sHist = "";
            string sTxt = "";

            string sParam = Settings.LoadParam("Backup", "W");

            if (sParam != "")
            {
                sBits = sParam.Split(';');
                sBakDir = (sBits[0] == "" ? gv.AppDir : sBits[0]);

                string sFN = GeneralFunctions.GetFileName(App.auBackupTemplate);
                txtFileName.Text = Path.Combine(sBakDir, sFN);

                int iNo = sBits.Length - 1;

                sHist = "      Date     Time  Workstation     User ID   File Size  Backup File\r\n";
                int iHist = 0;

                for (int i = 1; i < 11; i++)
                {
                    // Each line entry has ~ delimited data:
                    // Timestamp ~ Workstation ~ UserLogin ~ FileSize ~ BackupFileName
                    sBits2 = (sBits[i] + "~~~~").Split('~');
                    sTxt = (sBits2[0].PadRight(16) + " " + sBits2[1].PadRight(15) + " " + sBits2[2].PadRight(10) + sBits2[3].PadRight(10) + " " + sBits2[4]).Trim();

                    if (sTxt != "")
                    {
                        iHist++;
                        sHist += iHist.ToString().PadLeft(2) + ". " + sTxt + "\r\n";
                    }
                }

            }

            sParam = Settings.LoadParam("Backup", "G");

            if (sParam != "")
            {
                sBits = sParam.Split(';');

                int iNo = sBits.Length - 1;

                int iHist = 0;

                for (int i = 1; i < 11; i++)
                {
                    // Each line entry has ~ delimited data:
                    // Timestamp ~ Workstation ~ UserLogin ~ FileSize ~ BackupFileName
                    sBits2 = (sBits[i] + "~~~~").Split('~');
                    sTxt = (sBits2[0].PadRight(16) + " " + sBits2[1].PadRight(15) + " " + sBits2[2].PadRight(10) + sBits2[3].PadRight(10) + " " + sBits2[4]).Trim();

                    if (sTxt != "" && sBits2[1] != gv.PCName)
                    {
                        iHist++;

                        if (iHist == 1)
                        {
                            sHist += "\r\n     --------------------------------------------------------\r\n\r\n";
                            sHist += "      Date     Time  Workstation     User ID   File Size  Backup File\r\n";
                        }

                        sHist += iHist.ToString().PadLeft(2) + ". " + sTxt + "\r\n";
                    }
                }
            }


            return sHist;
        }

        private string Rest_Hist()
        {
            // Get the folder name for the last restore file location on this work station
            string[] sBits;
            string[] sBits2;
            string sBakDir = gv.AppDir;
            string sHist = "";
            string sTxt = "";

            string sParam = Settings.LoadParam("Restore", "G");

            if (sParam != "")
            {
                sBits = sParam.Split(';');

                int iNo = sBits.Length - 1;

                int iHist = 0;

                for (int i = 1; i < 11; i++)
                {
                    // Each line entry has ~ delimited data:
                    // Timestamp ~ Workstation ~ UserLogin ~ FileSize ~ BackupFileName
                    sBits2 = (sBits[i] + "~~~~").Split('~');
                    sTxt = (sBits2[0].PadRight(16) + " " + sBits2[1].PadRight(15) + " " + sBits2[2].PadRight(10) + sBits2[3].PadRight(10) + " " + sBits2[4]).Trim();

                    if (sTxt != "")
                    {
                        iHist++;

                        if (iHist == 1)
                        {
                            //sHist += "\r\n     --------------------------------------------------------\r\n\r\n";
                            sHist += "      Date     Time  Workstation     User ID   File Size  Restore File\r\n";
                        }

                        sHist += iHist.ToString().PadLeft(2) + ". " + sTxt + "\r\n";
                    }
                }
            }

            return sHist;
        }

        private void frmDBRestoreBackup_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            string oHelpID = gv.HelpID;
            gv.HelpID = (bBackup ? "h_backup" : "h_restore");
            GeneralFunctions.ShowHelp();
            gv.HelpID = oHelpID;
        }

        private void frmDBRestoreBackup_Load(object sender, EventArgs e)
        {
            txtFileName.Text = ConfigurationManager.AppSettings["ServerBackupFolder"];
            
            //txtServerImageFolder.Text = ConfigurationManager.AppSettings["ServerImageFolder"];
            if (!GlobleData.ManageRoleAccessEdit(this.Name))
            {
                btnBackup.Enabled = false;
                btnRestore.Enabled = false;
                btnCancel.Enabled = false;
            }
            Settings.SetWinPosSize(21, 520, 800, this);

        }

        private void frmDBRestoreBackup_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.SaveWinPosSize(21, "BackupRestore", this);
        }

        private void txtFileName_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fd.SelectedPath;

            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtSrvLocalDir.Text = fd.SelectedPath;

            }
        }

        private void txtSrvLocalDir_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

      




    }
}