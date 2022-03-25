using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Data.SqlClient;
using DebonoDLL.App_Code.BOL;
using DebonoDLL;
using DebonoDLL.Helpers;
using Debono.DAL;
using DebonoDLL.BOL;
using DEBONO;
namespace Debono
{

    public partial class Login : DevExpress.XtraEditors.XtraForm
    {

        #region "Variables and object declaration"
        String strConnString = "";
        Boolean isCorretCon = false;
        Boolean IsRestart = false;
        Conversion objCon = new Conversion();
        UserMasterBo objUser = new UserMasterBo();
        String strConPath = Environment.CurrentDirectory + "\\DebonoApp.dll";
        DBConnection objStartForm = new DBConnection();
        #endregion
        public Login()
        {
            InitializeComponent();
            GetAvilableConnection();
        }
        private void GetAvilableConnection()
        {

            strConnString = objCon.ConToStr(System.Configuration.ConfigurationSettings.AppSettings["connectionString"]);

            // Check if we have valid connection to database
            #region Check Connection
            try
            {
                SqlConnection objCon1 = new SqlConnection(strConnString);
                //.Replace("Connection Timeout = 0;", ""));

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
                        SqlConnection objCon1 = new SqlConnection(strConnString);
                        //.Replace("Connection Timeout = 0;", ""));
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
                //AdminLogin objLogin = new AdminLogin();
                //objLogin.ShowDialog();
                //GetRole = objLogin.GetRole;

                //if (!objLogin._isLoginSuccess)
                //{
                //    IsExist = true;
                //}
            }
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtLoginName.Text.Trim()))
                {
                    Debono.DebonoMsg.MsgError("Please fill login Id");
                    txtLoginName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(TxtPassword.Text.Trim()))
                {
                    Debono.DebonoMsg.MsgError("Please fill password");
                    TxtPassword.Focus();
                    return;
                }


                if (!ValidateUser())
                {
                    MessageBox.Show("UserName or Password is invalid !");
                    return;
                }
                else
                {
                    Main objMain = new Main();
                    objMain.UserName = txtLoginName.Text.Trim().ToUpper();
                    this.Hide();
                    objMain.Show(); 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private bool ValidateUser()
        {
            UserMasterBo objUser = new UserMasterBo();
            objUser._UserName = txtLoginName.Text.Trim().ToUpper();
            objUser._UserPassword = TxtPassword.Text.Trim();
            bool result = objUser.ValidateUser();
            if (result)
            {
                ManageRoleAccess(objUser._RoleId);
            }
            return result;
        }

        private void ManageRoleAccess(Int64 RoleId)
        {
            RoleAccessBo objRoleAccess = new RoleAccessBo();
            objRoleAccess._RoleId = RoleId;
            DataTable dtAccess = objRoleAccess.GetAccessForRole();
            if (dtAccess != null)
                GlobleData.dtUserRoleAccess = dtAccess.Copy();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

            /*  try
              {
                  lblVer.Text = "App. Version : " + Application.ProductVersion.ToString();
                  Dal objDal = new Dal();
                  if (String.IsNullOrEmpty(objDal.strCon1))
                  {
                      DBConnection objStartForm = new DBConnection();
                      objStartForm.ShowDialog();
                  }
              }
              catch (Exception ex)
              {
                  PointOfSaleMsg.MsgError("Problem during Connection");
                  ExceptionManager.LogException(ex);
              }*/
        }
        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == 13)
                {
                    BtnLogin_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                Debono.DebonoMsg.MsgError("Problem during login");
                ExceptionManager.LogException(ex);
            }
        }

        private void txtLoginName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                TxtPassword.Focus();
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}