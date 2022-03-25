using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Xml;
using DebonoDLL.App_Code.BOL;
using DebonoDLL.App_Code.DAL;
using System.Diagnostics;
//using DebonoDLL.App_Code.BOL;
using DevExpress.XtraSplashScreen;

namespace DebonoDLL.Helpers
{

    public class FormHelper
    {

        #region Encrypt Function
        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns></returns>
        public string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = System.Text.UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new System.Configuration.AppSettingsReader();
            // Get the key from config file
            string key = "ISO";// (string)settingsReader.GetValue("SecurityKey", typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            if (useHashing)
            {
                System.Security.Cryptography.MD5CryptoServiceProvider hashmd5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(key);

            System.Security.Cryptography.TripleDESCryptoServiceProvider tdes = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = System.Security.Cryptography.CipherMode.ECB;
            tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            System.Security.Cryptography.ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        #endregion

        #region Descript Function
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new System.Configuration.AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = "ISO";// (string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                System.Security.Cryptography.MD5CryptoServiceProvider hashmd5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(key);

            System.Security.Cryptography.TripleDESCryptoServiceProvider tdes = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = System.Security.Cryptography.CipherMode.ECB;
            tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            System.Security.Cryptography.ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return System.Text.UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion


        public Boolean UpdateConfigData(String strConnection)
        {
            Boolean IsRestart = false;
            System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
            Application.DoEvents();
            string strConfigFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location + ".config";
            strConfigFilePath = strConfigFilePath.Replace("DebonoDLL.dll", "Debono App.exe");
            XmlDoc.Load(strConfigFilePath);
            foreach (XmlElement xElement in XmlDoc.DocumentElement)
            {
                if (xElement.Name == "appSettings")
                {
                    foreach (XmlNode xNode in xElement.ChildNodes)
                    {
                        if (xNode.Attributes[0].Value == "connectionString")
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
                if (xElement.Name == "connectionStrings")
                {
                    foreach (XmlNode xNode in xElement.ChildNodes)
                    {

                        xNode.Attributes[1].Value = strConnection;
                        break;

                    }
                }
            }

            Application.DoEvents();
            XmlDoc.Save(strConfigFilePath);
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            IsRestart = true;
            return IsRestart;
        }


        public DataTable GetListOfSQlServerInstance()
        {
            DataTable dtServerinstance = new DataTable();
            try
            {
                Conversion objCon1 = new Conversion();
                //SqlDataSourceEnumerator servers = SqlDataSourceEnumerator.Instance;
                //DataTable serversTable = servers.GetDataSources();
                DataTable serversTable = SqlDataSourceEnumerator.Instance.GetDataSources();
                dtServerinstance.Columns.Add("ServerName");
                for (int i = 0; i < serversTable.Rows.Count; i++)
                {
                    DataRow dr = dtServerinstance.NewRow();
                    dr["ServerName"] = objCon1.ConToStr(serversTable.Rows[i]["ServerName"]) + "\\" + objCon1.ConToStr(serversTable.Rows[i]["InstanceName"]);
                    dtServerinstance.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
            return dtServerinstance;
        }

        private static Form _objMain = null;
        public static Form SetParentForm
        {
            set
            {
                _objMain = value;
            }
        }


        /// <summary>
        /// This funtion will be used to Open a form as Children of the form. 
        /// </summary>
        /// <param name="objFormToOpen">Form which is needed to be open</param>
        /// <param name="objParentForm">Parent form of the Form.</param>
      
     
        public static void OpenForm(Form objFormToOpen, Form objParentForm)
        {
            FormHelper o = new FormHelper();
            ShowWaitDialog();
            try
            {

                int frmIndex = o.IsFormAlreadyOpened(objFormToOpen, objParentForm);
                if (frmIndex > -1)
                {
                    // objParentForm.MdiChildren["sd"].Close();
                    //    objParentForm.MdiChildren[frmIndex].Close();
                    
                    objFormToOpen.MdiParent = objParentForm;
                    objFormToOpen.Show();
                 // objParentForm.MdiChildren[frmIndex].Focus();
                }
                else
                {
                    objFormToOpen.MdiParent = objParentForm;
                    objFormToOpen.Show();
                }
            }
            catch (Exception ex) { ExceptionManager.LogException(ex); }

            CloseWaitDialog();
        }
        



        /// <summary>
        /// This funtion will check if form that is needed to be opened is already open.
        /// </summary>
        /// <param name="objFormToCheck">Form which is needed to be open.</param>
        /// <param name="objParentForm">Parent form of the Form which is needed to be open.</param>
        /// <returns>true if already opened, false otherwise.</returns>
        public int IsFormAlreadyOpened(Form objFormToCheck, Form objParentForm)
        {
            try
            {
                if (objParentForm != null)
                {
                    for (int i = 0; i < objParentForm.MdiChildren.Length; i++)
                    {
                        if (objParentForm.MdiChildren[i].Name == objFormToCheck.Name)
                            return i;
                    }
                }
                return -1;
            }
            catch (Exception ex) { ExceptionManager.LogException(ex); return 0; }
        }

        public static Int64 MovePreviousRecord(String strTable, String strUniqueColumn, String strColumnValue, String strPrimaryColumn, Int64 nPrimaryValue)
        {
            DataTable dtPreviousRecord = new DataTable();
            Conversion objCon = new Conversion();
            Int64 nPreviousRecordId = 0;
            Dal objDal = new Dal();
            dtPreviousRecord = objDal.ExecuteTable("select top 1 " + strPrimaryColumn + " from [" + strTable + "] where " + strUniqueColumn + " < '" + strColumnValue + "' order by " + strUniqueColumn + " desc");
            if (dtPreviousRecord != null && dtPreviousRecord.Rows.Count > 0)
            {
                nPreviousRecordId = objCon.ConToInt64(dtPreviousRecord.Rows[0][strPrimaryColumn]);
            }
            if (nPreviousRecordId == 0)
                nPreviousRecordId = nPrimaryValue;
            return nPreviousRecordId;
        }

        public static Int64 MoveNextRecord(String strTable, String strUniqueColumn, String strColumnValue, String strPrimaryColumn, Int64 nPrimaryValue)
        {
            DataTable dtNextRecord = new DataTable();
            Conversion objCon = new Conversion();
            Int64 nNextRecordId = 0;
            Dal objDal = new Dal();
            dtNextRecord = objDal.ExecuteTable("select top 1 " + strPrimaryColumn + " from [" + strTable + "] where " + strUniqueColumn + " > '" + strColumnValue + "' order by " + strUniqueColumn + " asc");
            if (dtNextRecord != null && dtNextRecord.Rows.Count > 0)
            {
                nNextRecordId = objCon.ConToInt64(dtNextRecord.Rows[0][strPrimaryColumn]);
            }
            if (nNextRecordId == 0)
                nNextRecordId = nPrimaryValue;
            return nNextRecordId;
        }


        public DataTable GetCountryList()
        {
            Dal objDal = new Dal();
            DataTable dtCountry = objDal.ExecuteTable("Select distinct CountryId,Country from CountryList");
            return dtCountry;
        }

        public DataTable GetTarifList()
        {
            Dal objDal = new Dal();
            DataTable dttarif = objDal.ExecuteTable("Select TarifId,Tarif from Tarif");
            return dttarif;
        }

        public static String GetScreenNumbering(String ScreenName)
        {
            //NumberingBo objNum = new NumberingBo();
            //objNum._ScreenName = ScreenName;
            //objNum.LoadNumberbyScreenName();
            return "";
        }

        public static void SetScreenNumbering(String ScreenName)
        {
            //try
            //{


            //    NumberingBo objNum = new NumberingBo();
            //    objNum._ScreenName = ScreenName;
            //    objNum.LoadNumberbyScreenName();

            //    Conversion objCon = new Conversion();

            //    Int64 NewNo = 0;
            //    int iIncrementedValue = objNum._Increase;
            //    string strValue = objNum._Number;
            //    int i = objNum._Step;
            //    int count = strValue.Length;
            //    int temp = count - i;
            //    string strFirst = strValue.Substring(0, temp);
            //    string strLast = strValue.Substring(temp, count - temp);

            //    NewNo = objCon.ConToInt64(strLast) + iIncrementedValue;
            //    string strNewNo = NewNo.ToString();
            //    if (strLast.Length > strNewNo.Length)
            //        strNewNo = objCon.ConToStr("0000000000").Substring(0, strLast.Length - strNewNo.Length) + strNewNo;

            //    strValue = strFirst + strNewNo;

            //    objNum._Number = strValue;
            //    objNum.UpdateNumberingByScreenName();
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.LogException(ex);
            //}
        }


        public static void OpenDialog(Form objForm)
        {
            objForm.ShowDialog();
        }

        public void SendEmail(String StrTo, String strSubject, String strBody)
        {
            //Process p = new Process();
            //p.StartInfo.FileName = "mailto:" + StrTo + "?subject= " + strSubject + " &body=" + strBody;
            //p.Start();
            SendMail(null, StrTo, strBody, strSubject);

        }


        /// <summary>
        /// This method prepares the mail to send with the attachments as files
        /// send as arguments to this function.
        /// </summary>
        /// <param name="arrAttachments">Array of files to be sent as attachments</param>
        public void SendMail(string[] arrAttachments, string strAddressTO, string strMailBody, String strSubject)
        {
            //Outlook.ApplicationClass outlook = new Outlook.ApplicationClass();
            //Outlook.MailItem mail = (Outlook.MailItem)outlook.CreateItem(Outlook.OlItemType.olMailItem);
            //int iposition = 1;
            //if (strMailBody != string.Empty)
            //{
            //    mail.Body = strMailBody;
            //    iposition = mail.Body.Length + 1;
            //}
            //mail.To = strAddressTO;
            //mail.Subject = strSubject;
            //if (arrAttachments != null)
            //{
            //    for (int i = 0; i < arrAttachments.Length; i++)
            //    {
            //        mail.Attachments.Add(arrAttachments[i], (int)Outlook.OlAttachmentType.olByValue, iposition, System.IO.Path.GetFileNameWithoutExtension(arrAttachments[i]));
            //        iposition++;
            //    }
            //}
            //mail.Display(true);
        }


        public void SendEmail(String StrTo, String strSubject)
        {
            SendEmail(StrTo, strSubject, "");
        }

        public string SaveFileDialog()
        {
            SaveFileDialog objDialog = new SaveFileDialog();
            objDialog.InitialDirectory = "Reports";
            objDialog.DefaultExt = ".repx";
            objDialog.Filter = "Report file (*.repx)|*.repx|All files (*.*)|*.*";
            objDialog.ShowDialog();
            return objDialog.FileName;
        }


        public string OpenFileDialog()
        {
            OpenFileDialog objDialog = new OpenFileDialog();
            objDialog.InitialDirectory = "Reports";
            objDialog.DefaultExt = ".repx";
            objDialog.Filter = "Report file (*.repx)|*.repx|All files (*.*)|*.*";
            objDialog.ShowDialog();
            return objDialog.FileName;
        }

        public void SaveReport(string strForm, String strReport, String strPath, string strType)
        {

            DataSet ds = new DataSet();
            string strReportList = "Select path from ReportList";
            Dal objDAL = null;
            try
            {
                objDAL = new Dal();
                ds = objDAL.ExecuteDataset(strReportList);
                bool exists = false;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (strPath == ds.Tables[0].Rows[i][0].ToString())
                    {
                        exists = true;
                        break;
                    }
                }
                string strCommandText = string.Empty;
                if (exists)
                    strCommandText = "Update ReportList set FormName='" + strForm + "',ReportName='" + strReport + "',ReportType='" + strType + "',Enabled=1 where Path ='" + strPath + "'";
                else
                    strCommandText = "insert into ReportList(FormName,ReportName,Path,ReportType,Enabled) values('" + strForm + "','" + strReport + "','" + strPath + "','" + strType + "',1)";
                objDAL.ExecuteData(strCommandText);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
            finally
            {
                ds = null;
            }
        }


        public static void ShowWaitDialog()
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(WaitDialog));
            }
            catch (Exception ex)
            {

            }

        }


        public static void CloseWaitDialog()
        {
            try
            {
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
