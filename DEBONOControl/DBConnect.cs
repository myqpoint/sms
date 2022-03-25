using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Data.Sql;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using DebonoControl;
using DebonoDLL.Helpers;
using DebonoDLL;
using DebonoDLL.App_Code.BOL;
//using TBPDLL;
//using TBPControl;
//using TBPDLL.Helpers;
//using TBPDLL.App_Code.BOL;

namespace ReconControl
{
    public partial class DBConnect : UserControl
    {
        public DBConnect()
        {
            InitializeComponent();
        }

        private void SetDefaultProgressBarPosition()
        {
            if (progressBarSample.Properties.ProgressKind == ProgressKind.Horizontal)
            {
                progressBarSample.Height = 20;
            }
            else
            {
                progressBarSample.Width = 20;
            }
        }

        Boolean check = false;
        private void Startupformc_Load(object sender, EventArgs e)
        {
            WaitingDialog objWait = new WaitingDialog();
            try
            {
                isDatabaseList = false;
                rbnSql.Checked = true;
                cmbAuthentication.SelectedIndex = 0;

                objWait.ShowWaitingDialog("Please wait", "Application is trying to get the list of server's avilable. ");
                Application.DoEvents();
                FormHelper objFrmHelp = new FormHelper();
                DataTable dtServerinstance = objFrmHelp.GetListOfSQlServerInstance();
                Application.DoEvents();
                cmbServer.DataSource = dtServerinstance;
                cmbServer.DisplayMember = "ServerName";
                isDatabaseList = true;
                LoadConnectionString();
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
            try
            {
                objWait.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void AutomaticLoadConnection()
        {
            WaitingDialog objDai = new WaitingDialog();
            objDai.ShowWaitingDialog("Please wait...", "Getting Server List ");
            Application.DoEvents();
            Boolean result = false;
            Conversion objCon = new Conversion();
            SqlDataSourceEnumerator servers = SqlDataSourceEnumerator.Instance;
            DataTable serversTable = servers.GetDataSources();
            try
            {
                if (serversTable != null && serversTable.Rows.Count > 0)
                {
                    for (int i = 0; i < serversTable.Rows.Count; i++)
                    {
                        string strServer = objCon.ConToStr(serversTable.Rows[i]["ServerName"]);
                        objDai.ShowWaitingDialog("Please wait...", "Validating Connection with windows authentication for server  " + strServer);
                        Application.DoEvents();
                        if (!string.IsNullOrEmpty(objCon.ConToStr(serversTable.Rows[i]["InstanceName"])))
                            strServer = strServer + "\\" + objCon.ConToStr(serversTable.Rows[i]["InstanceName"]);
                        result = CheckConnection("", strServer, "", "");
                        if (result == false)
                        {
                            objDai.ShowWaitingDialog("Please wait...", "Validating Connection with Sql Server Authentication for server  " + strServer);
                            result = CheckConnection("", strServer, "sa", "comcad");
                        }
                    }
                }
                if (result == false)
                {
                    objDai.ShowWaitingDialog("Please wait...", "Validating Connection with database in Database folder  ");
                    result = CheckConnection("ReconCodeFolder/Database/Recon.MDF", "", "", "");
                }
                if (result == true)
                {
                    UpdateConfigData();
                    return;
                }
                if (result == false)
                {
                    MessageBox.Show("Could not Connect");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
            objDai.Dispose();
        }

        public Boolean CheckConnection(string strFilePath, string strSName, string strUID, String strPass)
        {
            try
            {
                string strDBName = "Recon";
                if (cmbDBName.Text != "")
                    strDBName = cmbDBName.Text;
                Application.DoEvents();

                if (strFilePath != "")
                {
                    strConnection = "Server=.\\SQLEXPRESS;AttachDbFilename=@File;Integrated Security=True;User Instance=True;";
                    strConnection = strConnection.Replace("@File", strFilePath);
                }
                else if (strUID != "")
                {
                    strConnection = @"Data Source=@Server; Initial Catalog=@DB; User Id=@UId; pwd=@PWd;";
                    strConnection = strConnection.Replace("@Server", strSName);
                    strConnection = strConnection.Replace("@DB", strDBName);
                    strConnection = strConnection.Replace("@UId", strUID);
                    strConnection = strConnection.Replace("@PWd", strPass);
                }
                else
                {
                    strConnection = "Data Source=@Server;Initial Catalog=@DB;Integrated Security=True;";
                    strConnection = strConnection.Replace("@Server", strSName);
                    strConnection = strConnection.Replace("@DB", strDBName);
                }
                Application.DoEvents();


                //string str = System.Configuration.ConfigurationSettings.AppSettings["connectionString"].ToString();
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strConnection);
                conn.Open();
                Application.DoEvents();
                conn.Close();
                return true;
                Application.DoEvents();
            }
            catch (Exception ex)
            { return false; }
            Application.DoEvents();

        }

        String StrConnectionSetting = "connectionString";
        private void UpdateConfigData()
        {
            try
            {

                StrConnectionSetting = "connectionString";
                System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
                strConnection = strConnection + "Connection Timeout = 0;Min Pool Size = 40;Max Pool Size = 1400;";
                Application.DoEvents();
                // Same code we have to change in Formhelper as well for path.
                string strConfigFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location + ".config";
                strConfigFilePath = strConfigFilePath.Replace("TBPControl.dll", "TBP App.exe");
                XmlDoc.Load(strConfigFilePath);

                foreach (XmlElement xElement in XmlDoc.DocumentElement)
                {
                    if (xElement.Name == "appSettings")
                    {
                        foreach (XmlNode xNode in xElement.ChildNodes)
                        {
                            if (xNode.Attributes[0].Value == StrConnectionSetting)
                            {
                                xNode.Attributes[1].Value = strConnection;
                                break;
                            }
                        }
                    }
                }
                Application.DoEvents();

                foreach (XmlElement xElement in XmlDoc.DocumentElement)
                {
                    if (xElement.Name == StrConnectionSetting)
                    {
                        foreach (XmlNode xNode in xElement.ChildNodes)
                        {

                            xNode.Attributes[1].Value = strConnection;
                            break;

                        }
                    }
                }

                Application.DoEvents();

                //Saving the Updated values in App.config File.Here updating the config //file in the same path.
                XmlDoc.Save(strConfigFilePath);
                XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                MessageBox.Show("Connection save successfully");
                IsRestart = true;

                Application.Restart();


            }
            catch (Exception er)
            {
                ExceptionManager.LogException(er);
            }


        }
        private Boolean IsRestart;

        public Boolean _IsRestart
        {
            get { return IsRestart; }
            set { IsRestart = value; }
        }

        private void cmbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAuthentication.SelectedIndex == 0)
            {
                txtUser.Enabled = false;
                txtPass.Enabled = false;
            }
            else
            {
                txtUser.Enabled = true;
                txtPass.Enabled = true;
            }
            GetdataBaseList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetdataBaseList();
        }

        private void cmbDBName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((System.Data.DataRowView)(cmbDBName.SelectedValue)).Row.ItemArray[0].ToString().Contains("Recon"))
                cmbDBName.Text = ((System.Data.DataRowView)(cmbDBName.SelectedValue)).Row.ItemArray[0].ToString();

        }

        static Boolean isDatabaseList = false;
        private void GetdataBaseList()
        {
            if (!isDatabaseList)
                return;
            String strConn = "";
            if (rbnSql.Checked == true)
            {
                if (cmbAuthentication.SelectedIndex == 1)
                {
                    strConn = @"Data Source=@Server; Initial Catalog=master; User Id=@UId; pwd=@PWd;";
                    strConn = strConn.Replace("@Server", cmbServer.Text.Trim());
                    strConn = strConn.Replace("@UId", txtUser.Text.Trim());
                    strConn = strConn.Replace("@PWd", txtPass.Text.Trim());
                }
                else if (cmbAuthentication.SelectedIndex == 0)
                {
                    strConn = "Data Source=@Server;Initial Catalog=master;Integrated Security=True";
                    strConn = strConn.Replace("@Server", cmbServer.Text.Trim());
                }
            }
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(strConn);
                cmd = new SqlCommand("select Name from sys.databases order by Name", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmbDBName.DataSource = ds.Tables[0];
                cmbDBName.DisplayMember = "Name";
            }
            catch
            { }
            finally
            {
                cmd = null;
                con.Close();
                con.Dispose();
            }
        }

        Form objForm;
        private void btnSaveConnection_Click(object sender, EventArgs e)
        {
            objForm = this.FindForm();
            Boolean result = CheckConnection();
            if (!result)
            {
                MessageBox.Show("Could not connect");
            }
            else
            {
                UpdateConfigData();
            }
        }

        string strConnection = "";
        public Boolean CheckConnection()
        {
            try
            {
                strConnection = "";
                if (rbnMDF.Checked == true)
                {
                    strConnection = "Server=.\\SQLEXPRESS;AttachDbFilename=@File;Integrated Security=True;User Instance=True;";
                    strConnection = strConnection.Replace("@File", txtMdfPath.Text.Trim());
                }
                else if (rbnSql.Checked == true)
                {
                    if (cmbAuthentication.SelectedIndex == 1)
                    {
                        strConnection = @"Data Source=@Server; Initial Catalog=@DB; User Id=@UId; pwd=@PWd;";
                        strConnection = strConnection.Replace("@Server", cmbServer.Text.Trim());
                        strConnection = strConnection.Replace("@DB", cmbDBName.Text.Trim());
                        strConnection = strConnection.Replace("@UId", txtUser.Text.Trim());
                        strConnection = strConnection.Replace("@PWd", txtPass.Text.Trim());
                    }
                    else if (cmbAuthentication.SelectedIndex == 0)
                    {
                        strConnection = "Data Source=@Server;Initial Catalog=@DB;Integrated Security=True;";
                        strConnection = strConnection.Replace("@Server", cmbServer.Text.Trim());
                        strConnection = strConnection.Replace("@DB", cmbDBName.Text.Trim());
                    }
                }
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(strConnection);
                conn.Open();
                return true;
            }
            catch
            { return false; }
        }

        private void btnMdfPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.ShowDialog();
            txtMdfPath.Text = openFileDialog2.FileName;
        }
        public delegate void CloseHostFormEventHandler(Object sender, EventArgs e);
        public event CloseHostFormEventHandler Close;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //EventArgs myargs = new EventArgs();
                //Close(this, myargs);
                XtraForm objXtraForm = (XtraForm)this.Parent;
                objXtraForm.Close();
              
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Auto Search can take time . Are you sure you want to continue with auto search ???? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                AutomaticLoadConnection();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetdataBaseList();
        }

        private void cmbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetdataBaseList();
        }


        private void rbnSql_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnMDF.Checked)
            {
                txtMdfPath.Enabled = true;
                btnMdfPath.Enabled = true;
                cmbServer.Enabled = false;
                txtPass.Enabled = false;
                txtUser.Enabled = false;
                cmbDBName.Enabled = false;
                cmbAuthentication.Enabled = false;
            }
            else if (rbnSql.Checked)
            {
                txtMdfPath.Enabled = false;
                btnMdfPath.Enabled = false;
                cmbServer.Enabled = true;

                txtPass.Enabled = false;
                txtUser.Enabled = false;
                cmbDBName.Enabled = true;
                cmbAuthentication.Enabled = true;
            }
        }

        private void rbnMDF_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnMDF.Checked)
            {
                txtMdfPath.Enabled = true;
                btnMdfPath.Enabled = true;
                cmbServer.Enabled = false;
                txtPass.Enabled = false;
                txtUser.Enabled = false;
                cmbDBName.Enabled = false;
                cmbAuthentication.Enabled = false;
            }
            else if (rbnSql.Checked)
            {
                txtMdfPath.Enabled = false;
                btnMdfPath.Enabled = false;
                cmbServer.Enabled = true;
                txtPass.Enabled = true;
                txtUser.Enabled = true;
                cmbDBName.Enabled = true;
                cmbAuthentication.Enabled = true;
            }
        }

        public object SetVariables()
        {
            CmbServerName = cmbServer.Text.Trim();
            return CmbServerName;
        }

        public string MdfFilePath
        {
            get { return txtMdfPath.Text.Trim(); }
            set { txtMdfPath.Text = value; }
        }
        public string CmbServerName
        {
            get { return cmbServer.Text.Trim(); }
            set { cmbServer.Text = value; }
        }
        public string AuthenticationName
        {
            get { return cmbAuthentication.Text.Trim(); }
            set { cmbAuthentication.Text = value; }
        }
        public string UserName
        {
            get { return txtUser.Text.Trim(); }
            set { txtUser.Text = value; }
        }
        public string Password
        {
            get { return txtPass.Text.Trim(); }
            set { txtPass.Text = value; }
        }
        public string Dbname
        {
            get { return cmbDBName.Text.Trim(); }
            set { cmbDBName.Text = value; }
        }

        private void LoadConnectionString()
        {
            try
            {
                objForm = this.FindForm();
                if (objForm.Name == "Syncronization")
                {
                    StrConnectionSetting = "connectionString2";
                }
                String strConnection = "";
                Boolean isCorretCon = false;
                try
                {
                    strConnection = System.Configuration.ConfigurationSettings.AppSettings[StrConnectionSetting].ToString();
                    System.Data.SqlClient.SqlConnection objCon = new System.Data.SqlClient.SqlConnection(strConnection.Replace("Connection Timeout = 0;", ""));
                    objCon.Open();
                    objCon.Close();
                    isCorretCon = true;
                }
                catch (Exception ex)
                {
                    isCorretCon = false;
                    ExceptionManager.LogException(ex);
                }
                if (isCorretCon)
                {
                    if (strConnection.Contains("AttachDbFilename"))
                    {
                        rbnMDF.Checked = true;
                        String[] StrCon = strConnection.Split(';');
                        String StrDB = StrCon[1].Replace("AttachDbFilename=", "");
                        txtMdfPath.Text = StrDB;
                    }
                    else if (strConnection.Contains("Integrated"))
                    {
                        rbnSql.Checked = true;
                        cmbAuthentication.SelectedIndex = 0;
                        String[] StrCon = strConnection.Split(';');
                        cmbServer.Text = StrCon[0].Replace("Data Source=", ""); ;
                        String StrDB = StrCon[1].Replace("Initial Catalog=", "");
                        cmbDBName.Text = StrDB;
                    }
                    else if (strConnection.Contains("pwd"))
                    {
                        rbnSql.Checked = true;
                        cmbAuthentication.SelectedIndex = 1;
                        String[] StrCon = strConnection.Split(';');
                        cmbServer.Text = StrCon[0].Replace("Data Source=", ""); ;
                        String StrDB = StrCon[1].Replace("Initial Catalog=", "");
                        cmbDBName.Text = StrDB;
                        txtUser.Text = StrCon[2].Replace("User Id=", "").Trim();
                        txtPass.Text = StrCon[3].Replace("pwd=", "").Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }

        }

    }
}
