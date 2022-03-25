using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Helpers;
using System.IO;
using DebonoDLL;
using DebonoDLL.Helpers;
using Debono;
using Debono.Info;
using DebonoDLL.App_Code.BOL;
using DEBONO.Info;
using Debono.Detail;
using DEBONO;
using System.Diagnostics;


namespace Debono
{
    public partial class Main : XtraForm
    {

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        Boolean IsRestart = false;
        private void Main_Load(object sender, EventArgs e)
        {
            this._UserName = UserName;
            if (IsRestart)
            {
                Application.Restart();
            }
        }


        String strConnString = "";
        Boolean isCorretCon = false;
        public static string strRichMessage = @"{\rtf1\deff0{\fonttbl{\f0 }{\f1 Arial;}}{\colortbl\red0\green0\blue0 ;}{\*\listoverridetable}{\stylesheet {\ql\cf0 Normal;}{\*\cs1\cf0 Default Paragraph Font;}}\sectd\pard\plain\ql\f1\fs20\par}";
        public static Color filterRowColor = Color.White;
        String strConPath = Environment.CurrentDirectory + "\\Debono.dll";
        DBConnection objStartForm = new DBConnection();



        public Main()
        {
            try
            {
                Conversion objCon = new Conversion();

                DevExpress.Skins.SkinManager.EnableFormSkins();
                if (this.defaultLookAndFeel1 != null)
                    this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2007 Black";

                FormHelper.ShowWaitDialog();
                GetAvilableConnection();

                Application.DoEvents();
                InitializeComponent();
                Application.DoEvents();
                FormHelper.SetParentForm = this;

                String[] strD = strConnString.Split(';');
                dbName.Caption = "                         " + strD[1].Replace("Initial Catalog=", "").Replace("AttachDbFilename=", "");
                dbName.Caption = dbName.Caption + "                         " + strD[0].Replace("Data Source=", "").Replace("Server=", "");


                Banner objBan = new Banner();
                OpenForm(objBan);
                FormHelper.CloseWaitDialog();
            }
            catch (Exception ex)
            {

                ExceptionManager.LogException(ex);
            }

        }

        private void GetAvilableConnection()
        {
            Conversion objCon = new Conversion();
            strConnString = objCon.ConToStr(System.Configuration.ConfigurationSettings.AppSettings["connectionString"]);


            // Check if we have valid connection to database
            #region Check Connection
            try
            {
                System.Data.SqlClient.SqlConnection objCon1 = new System.Data.SqlClient.SqlConnection(strConnString.Replace("Connection Timeout = 0;", ""));

                objCon1.Open();
                isCorretCon = true;
                DebonoDLL.Main objMainDll = new DebonoDLL.Main();
                objMainDll.LoadConnection("");
                objCon1.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
            #endregion

            //in case if we do not get valid connection then we try to read file in which we had saved connection last time
            #region Load Connection
            if (!isCorretCon)
            {
                if (File.Exists(strConPath))
                {
                    try
                    {
                        StreamReader SR = new StreamReader(strConPath);
                        strConnString = SR.ReadToEnd();
                        SR.Close();
                        System.Data.SqlClient.SqlConnection objCon1 = new System.Data.SqlClient.SqlConnection(strConnString.Replace("Connection Timeout = 0;", ""));
                        objCon1.Open();
                        isCorretCon = true;
                        objCon1.Close();
                        FormHelper objFrmHelp = new FormHelper();
                        IsRestart = objFrmHelp.UpdateConfigData(strConnString);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.LogException(ex);
                    }
                }
            }

            #endregion

            // If we have valid connection then we save in a file for future refrence
            #region Save Connection
            if (isCorretCon)
            {
                try
                {
                    if (File.Exists(strConPath))
                    {
                        File.Delete(strConPath);
                    }
                    StreamWriter Sw = new StreamWriter(strConPath);
                    Sw.Write(strConnString);
                    Sw.Close();

                }
                catch (Exception ex)
                {
                    ExceptionManager.LogException(ex);
                }
            }
            #endregion

            // if we do not get valid connection then load DB connect form
            if (!isCorretCon)
            {
                objStartForm.ShowDialog();
            }
            else
            {

            }

        }

        private void OpenForm(Form objFrom)
        {
            FormHelper.OpenForm(objFrom, this);
        }
        private void NBI_Fees_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            StudentFees objCustnfo = new StudentFees();
            objCustnfo.UserName = this.UserName;
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }


        }
        private void NBI_Student_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            StudentList objCustnfo = new StudentList();
            objCustnfo.UserName = this.UserName;
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }


        }
        private void NBI_Staff_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            StaffList objCustnfo = new StaffList();
            objCustnfo.UserName = this._UserName;
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }


        }
        private void NBI_Salary_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            StaffList objCustnfo = new StaffList();
            objCustnfo.UserName = this.UserName;
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }


        }
        private void NBI_FeesManagement_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FeesManagement objCustnfo = new FeesManagement();
            objCustnfo.UserName = this.UserName;
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }


        }
        private void NBI_Subject_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Subject objCustnfo = new Subject();
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }
        }
        private void NBI_Publication_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Publication objCustnfo = new Publication();
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }
        }
        private void NBI_Books_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            BookList objCustnfo = new BookList();
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }
        }
        private void NBI_BookReport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DebonoMsg.MsgInformation("Sorry ! Not Authorized");
        }
        private void NBI_IssueBook_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            BookIssueForm objCustnfo = new BookIssueForm();
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }
        }
        private void NBI_IssueReport_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            BookIssueList objCustnfo = new BookIssueList();
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }
        }
        private void NBI_ReturnBook_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            BookReturnForm objCustnfo = new BookReturnForm();
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }
        }
        private void NBI_Panalty_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            PanaltyList objCustnfo = new PanaltyList();
            if (GlobleData.ManageRoleAccessView(objCustnfo.Name))
            {
                OpenForm(objCustnfo);
            }
        }
        
       
        private void nb_user_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            UserRoleInfo objForm = new UserRoleInfo();
            if (GlobleData.ManageRoleAccessView(objForm.Name))
            {
                OpenForm(objForm);
            }
        }

        private void navBarUser_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            User objForm = new User();
            if (GlobleData.ManageRoleAccessView(objForm.Name))
            {
                OpenForm(objForm);
            }
        }

        private void navBarBackUpandRestoreDB_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmDBRestoreBackup objfrmDBRestoreBackup = new frmDBRestoreBackup();
           if (GlobleData.ManageRoleAccessView(objfrmDBRestoreBackup.Name))
            {
                OpenForm(objfrmDBRestoreBackup);
            }
        }

        private void NBI_Exit_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
           /* if (DebonoMsg.MsgExit())
            {
                Application.Exit();
            }*/
            Main_FormClosing(null,null);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DebonoMsg.MsgExit())
            {
                Process.GetCurrentProcess().Kill();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}