using DEBONO.Helper;
using DebonoDLL;
using DebonoDLL.App_Code.BOL;
using DebonoDLL.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Debono
{
    public partial class ExternalLink : DevExpress.XtraEditors.XtraForm
    {
        public static string serverPath;
        public static string serverusername;
        public static string serverpwd;
        String strConnString = "";
        static String sourcefilepath = "";

        Conversion objCon = new Conversion();

        public bool isFromOrder = false;
        public bool isDrawing { get; set; }
        public ExternalLink()
        {
            strConnString = objCon.ConToStr(System.Configuration.ConfigurationSettings.AppSettings["connectionString"]);
            InitializeComponent();
        }

        private DataTable dtExternalLink;
        public DataTable _dtExternalLink
        {
            get { return dtExternalLink; }
            set { dtExternalLink = value; }
        }

        public String[] strToDeleteExternalLink = new String[250];

        private void ExternalLink_Load(object sender, EventArgs e)
        {
            try
            {

                DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("Arial Unicode MS", 11.25f);
                Conversion objCon = new Conversion();
                if (dtExternalLink != null)
                {
                    //if (!dtExternalLink.Columns.Contains("icon")) dtExternalLink.Columns.Add("icon", typeof(String));
                    if (!dtExternalLink.Columns.Contains("icon")) dtExternalLink.Columns.Add("icon", typeof(Image));

                    foreach (DataRow dr in dtExternalLink.Rows)
                    {
                        if (!string.IsNullOrEmpty(objCon.ConToStr(dr["LinkFile"])))
                        {
                            string ext = Path.GetExtension(objCon.ConToStr(dr["LinkFileType"]));

                            string base_Path1 = Application.StartupPath;

                            string base_Path = objCon.ConToStr(dr["Path"]);
                            if(!base_Path.Contains(":"))
                            base_Path = base_Path1 + base_Path;
                            dr["Path"] = base_Path;
                            if (ext == ".pdf")
                            {
                                dr["icon"] = DEBONO.Properties.Resources.pdf;
                            }
                            if (ext == ".txt")
                            {

                                dr["icon"] = DEBONO.Properties.Resources.txt;
                            }
                            if (ext == ".zip" || ext == ".rar")
                            {

                                dr["icon"] = DEBONO.Properties.Resources.zip;
                            }
                            if (ext == ".doc" || ext == ".docx")
                            {

                                dr["icon"] = DEBONO.Properties.Resources.word;
                            }
                            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                            {
                                dr["icon"] = DEBONO.Properties.Resources.image;
                            }
                        }
                    }
                    gridExternLink.DataSource = dtExternalLink;
                }
                btnOk.Enabled = !isFromOrder;
                gridColumn4.OptionsColumn.AllowEdit = !isFromOrder;
                gridColumn5.OptionsColumn.AllowEdit = !isFromOrder;

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {


                dtExternalLink = gridExternLink.DataSource as DataTable;
                // during ftp server this code will be run
                if (dtExternalLink != null)
                {
                    //AppSettingBo objappsettingbo = new AppSettingBo();
                    //serverPath = objappsettingbo.getServerData("serverpath");
                    //serverusername = objappsettingbo.getServerData("serverusername");
                    //serverpwd = objappsettingbo.getServerData("serverpwd");
                    string base_Path1 = Application.StartupPath;
                  string serverPath = ConfigurationManager.AppSettings["ServerImageFolder"];
                    //MessageBox.Show(serverPath);
                  serverPath = base_Path1+serverPath;
                    for (int n = 0; n < dtExternalLink.Rows.Count; n++)
                    {
                        Conversion objCon = new Conversion();
                        sourcefilepath = objCon.ConToStr(dtExternalLink.Rows[n]["Path"]);
                        //UploadFileToFTP(sourcefilepath);
                        UploadFileToFolder(sourcefilepath);

                        dtExternalLink.Rows[n]["Path"] = serverPath + Path.GetFileName(sourcefilepath);
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "2");
            }
        }
       
        private static void UploadFileToFolder(string Path1)
        {
            try
            {
                NetworkCredential nw = new NetworkCredential();
                nw.UserName = ConfigurationManager.AppSettings["serveruser"];
                nw.Password = ConfigurationManager.AppSettings["serverpass"];

               

                string[] str = Path1.Split('\\');
                int count = str.Length;
                string iName = str[count - 1];
                string base_Path1 = Application.StartupPath;
                string serverPath = ConfigurationManager.AppSettings["ServerImageFolder"];
                serverPath = base_Path1 + serverPath;
                string serverfullfilename = serverPath + iName;
                // if (File.Exists(serverPath + iName))
                // {
                //     string locpath = Path;
                //    System.IO.File.Delete(serverPath + iName);

                // }
                //// else
                // {
                //     System.IO.File.Copy(Path, serverPath + iName);
                // }
                string mappedPath = Path.Combine(serverPath, iName);
               
                if (!Path1.Contains(serverPath))
                {
                    
                    if (File.Exists(serverfullfilename))
                        System.IO.File.Delete(serverfullfilename);
                    
                   
                    if(File.Exists(serverfullfilename))
                    {
                        File.SetAttributes(serverfullfilename, FileAttributes.Normal);
                    }
                   
                    File.Copy(Path1, serverfullfilename,true);
                    
                   
                   
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "1");
            }

        }

        private static void UploadFileToFTP(string source)
        {
            try
            {
               
                // AppSettingBo objAppsettingbo = new AppSettingBo(); 
                string filename = Path.GetFileName(source);
                string ftpfullpath = serverPath;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath + filename);
                ftp.Credentials = new NetworkCredential(serverusername, serverpwd);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                FileStream fs = File.OpenRead(source);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void deletetoolStripMenuItem_Click(object sender, EventArgs e)
        {

            CommonLoadFunction.DeleteItemFromGrid(gvExternalLink, "ExternalLinkId", ref strToDeleteExternalLink);
            dtExternalLink.AcceptChanges();
            gridExternLink.DataSource = dtExternalLink;


            // DeleteFocusedRow();

        }

        void DeleteFocusedRow()
        {
            if (Debono.DebonoMsg.MsgDelete())
            {
                if (gvExternalLink == null || gvExternalLink.SelectedRowsCount == 0) return;

                /*
                DataRow[] rows = new DataRow[gvExternalLink.SelectedRowsCount];

                for (int i = 0; i < gvExternalLink.SelectedRowsCount; i++)
                
 rows[i] = gvExternalLink.GetDataRow(gvExternalLink.GetSelectedRows()[i]);
gvExternalLink.BeginSort();
try
{
    foreach (DataRow row in rows)
        row.Delete();
}
finally
{ gvExternalLink.BeginSort(); }
                */

                int a = gvExternalLink.FocusedRowHandle;
                DataTable dt = gridExternLink.DataSource as DataTable;

                dt.Rows.RemoveAt(a);
                dt.AcceptChanges();
                gridExternLink.DataSource = dt;

            }
        }


        private void repItmBtnExtLink_Click(object sender, EventArgs e)
        {

        }

        private void repItmBtnExtLink_DoubleClick(object sender, EventArgs e)
        {
            Conversion objCon = new Conversion();
            DataTable dt = gridExternLink.DataSource as DataTable;
            string Destination = objCon.ConToStr(gvExternalLink.GetFocusedRowCellValue("Path"));
            try
            {
                if (!string.IsNullOrEmpty(Destination) && File.Exists(Destination))
                    Process.Start(Destination);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void repItmBtnExtLink_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                Conversion objCon = new Conversion();

                ColumnView viewExternLink = gridExternLink.FocusedView as ColumnView;
                viewExternLink.CloseEditor();
                viewExternLink.UpdateCurrentRow();
                int gridFocusedRow = objCon.ConToInt(gvExternalLink.FocusedRowHandle);

                OpenFileDialog objDlg = new OpenFileDialog();
                objDlg.Multiselect = true;
                objDlg.ShowDialog();

                foreach (string filename in objDlg.FileNames)
                {
                    dtExternalLink = gridExternLink.DataSource as DataTable;

                    DataRow row = dtExternalLink.NewRow();

                    dtExternalLink.Rows.Add(row);
                    gridFocusedRow = objCon.ConToInt(dtExternalLink.Rows.Count - 1);

                    //modify by ajay
                    sourcefilepath = objDlg.FileName;
                    //end ajay modification
                    if (File.Exists(objDlg.FileName))
                    {
                        string FileName = System.IO.Path.GetFileNameWithoutExtension(objDlg.FileName);

                        gvExternalLink.SetRowCellValue(gridFocusedRow, "LinkFile", FileName);
                        gvExternalLink.SetRowCellValue(gridFocusedRow, "Path", objDlg.FileName);
                        gvExternalLink.SetRowCellValue(gridFocusedRow, "LastUpdate", DateTime.Now);
                        string ext = Path.GetExtension(objCon.ConToStr(objDlg.FileName));
                        gvExternalLink.SetRowCellValue(gridFocusedRow, "LinkFileType", ext);
                        string base_Path = Application.StartupPath;
                        if (ext == ".pdf")
                        {
                            gvExternalLink.SetRowCellValue(gridFocusedRow, "icon", DEBONO.Properties.Resources.pdf);
                        }
                        if (ext == ".txt")
                        {
                            gvExternalLink.SetRowCellValue(gridFocusedRow, "icon", DEBONO.Properties.Resources.txt);
                        }
                        if (ext == ".zip" || ext == ".rar")
                        {
                            gvExternalLink.SetRowCellValue(gridFocusedRow, "icon", DEBONO.Properties.Resources.zip);
                        }
                        if (ext == ".doc" || ext == ".docx")
                        {
                            gvExternalLink.SetRowCellValue(gridFocusedRow, "icon", DEBONO.Properties.Resources.word);
                        }
                        if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                        {
                            gvExternalLink.SetRowCellValue(gridFocusedRow, "icon", DEBONO.Properties.Resources.image);
                        }

                        dtExternalLink.AcceptChanges();
                    }
                }
                gvExternalLink.FocusedRowHandle = gridFocusedRow;
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }


        private void LoadOrSaveLayout(CommonFunction.custLayoutOptions enLayoutOptions)
        {
            CommonFunction objcmnFun = new CommonFunction();
            objcmnFun.LoadORSaveLayout(this.Name + "&", ref gvExternalLink, enLayoutOptions);
        }

        private void ExternalLink_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoadOrSaveLayout(CommonFunction.custLayoutOptions.Save);
        }

        private void ExternalLink_Activated(object sender, EventArgs e)
        {
            LoadOrSaveLayout(CommonFunction.custLayoutOptions.Load);
        }

        private void gridExternLink_Click(object sender, EventArgs e)
        {

        }

        private void repIcon_DoubleClick(object sender, EventArgs e)
        {
           
            Conversion objCon = new Conversion();
            string Destination = objCon.ConToStr(gvExternalLink.GetFocusedRowCellValue("Path"));
            
            try
            {
                if (!string.IsNullOrEmpty(Destination) && File.Exists(Destination))
                    Process.Start(Destination);
                else
                    MessageBox.Show(Destination);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Destination + "   " + ex.Message);
                ExceptionManager.LogException(ex);
            }
        }

        private void repIcon_Click(object sender, EventArgs e)
        {

        }

        private void repIcon_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        private void gvExternalLink_DoubleClick(object sender, EventArgs e)
        {

        }

    }
}