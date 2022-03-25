using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Native;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting.Control;
using System.Reflection;
using System.Net;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Text.RegularExpressions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Media;
using DevExpress.XtraTab;
using System.Xml;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DebonoDLL.App_Code.BOL;
namespace Debono
{
    public class GeneralFunctions
    {

        public static bool bContinueCopyAllNewer = true;


        public static void ClearValidationErrorMessage(DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValid)
        {
            List<Control> c = new List<Control>();
            foreach (Control ctrl in dxValid.GetInvalidControls())
            {
                c.Add(ctrl);
            }
            foreach (Control c1 in c)
            {
                dxValid.RemoveControlError(c1);
            }
        }



        public static string GetFileName(string sTemplate)
        {
            // Generate a file name based on the customer / supplier account reference and say sales / purchase invoice no etc..
            // [conm][coname][pc][usr][name]
            // [ddd][ddt][dmt][dmth][doc][drep][dtd][dno][dref][dyy][dyyyy]
            // [date][dd][etad][hr][mn][mt][mth][time][timestamp][yy][yyyy]
            string sFlName = "";
            string sCoName = Co.Name.Replace(" ", "_").ToLower();
            string sName = gv.prName.Replace(" ", "_").ToLower();
            DateTime dtNow = System.DateTime.Now;

            // General macros
            //sFlName = App.auFileNameTemplate.ToLower().Replace("[dref]", gv.prAccountRef.ToLower()); // Cust / Supp / Stock reference
            sFlName = sTemplate.ToLower().Replace("[dref]", gv.prAccountRef.ToLower()); // Cust / Supp / Stock reference
            sFlName = sFlName.Replace("[usr]", gv.UserLogin.ToLower());                            // User login ID
            sFlName = sFlName.Replace("[pc]", gv.PCName.ToLower());                                // Workstation ID
            sFlName = sFlName.Replace("[conm]", Co.Nm.ToLower());                                  // Abbreviated company name
            sFlName = sFlName.Replace("[coname]", sCoName);                                        // Full company name
            sFlName = sFlName.Replace("[name]", sName);                                            // Customer / supplier name or stock item description

            if (gv.prDocNo >= 0)
            {
                sFlName = sFlName.Replace("[dno]", gv.prDocNo.ToString().PadLeft(App.auFileNameZeros, '0')); // Document number e.g. Inv No, SO No
            }

            // Document date related macros
            sFlName = sFlName.Replace("[doc]", gv.prDocType);                                      // Document type, si, sc, so, sq
            sFlName = sFlName.Replace("[ddt]", gv.prDocDate.ToString("ddMMyyyy"));                 // Document date in dd/mm/yyyy format
            sFlName = sFlName.Replace("[dtd]", gv.prDocDate.ToString("yyyyMMdd"));                 // Document date in reverse, yyyy/mm/dd format
            sFlName = sFlName.Replace("[dyyyy]", gv.prDocDate.ToString("yyyy"));                   // Document date 4-digit year
            sFlName = sFlName.Replace("[dyy]", gv.prDocDate.ToString("yy"));                       // Document date 2-digit year
            sFlName = sFlName.Replace("[dmt]", gv.prDocDate.ToString("MM"));                       // Document date 2-digit month
            sFlName = sFlName.Replace("[dmth]", gv.prDocDate.ToString("MMM"));                     // Document date 3-char month e.g. Jan, Feb
            sFlName = sFlName.Replace("[dda]", gv.prDocDate.ToString("dd"));                       // Document date 2-digit day
            sFlName = sFlName.Replace("[drep]", gv.prRepType);                                     // Report type

            // Current date and time related macros
            sFlName = sFlName.Replace("[date]", dtNow.Date.ToString("ddMMyyyy"));                 // Print date in dd/mm/yyyy format
            sFlName = sFlName.Replace("[da]", dtNow.ToString("dd"));                              // Current date, 2-digit day of month (01 - 31)
            sFlName = sFlName.Replace("[hr]", dtNow.ToString("HH"));                              // Print time in 2-digit hours
            sFlName = sFlName.Replace("[mn]", dtNow.ToString("mm"));                              // Current time, 2-digit minutes
            sFlName = sFlName.Replace("[mt]", dtNow.ToString("MM"));                              // Current date, 2-digit month (01 - 12)
            sFlName = sFlName.Replace("[mth]", dtNow.ToString("MMM"));                            // Document date 3-char month e.g. Jan, Feb
            sFlName = sFlName.Replace("[ss]", dtNow.ToString("ss"));                              // Current time in 2-digit seconds
            sFlName = sFlName.Replace("[yyyy]", dtNow.ToString("yyyy"));                          // Current date, 4-digit year
            sFlName = sFlName.Replace("[yy]", dtNow.ToString("yy"));                              // Current date, 2-digit year
            sFlName = sFlName.Replace("[etad]", dtNow.ToString("yyyyMMdd"));                      // Current date in reverse, yyyy/mm/dd format
            sFlName = sFlName.Replace("[time]", dtNow.Date.ToString("HHmm"));                     // Current time in HHMM format
            sFlName = sFlName.Replace("[timestamp]", dtNow.ToString("yyyyMMdd-HHmmss"));          // Current date & time

            sFlName = sFlName.Replace(" ", "");                                                   // Remove illegal characters and spaces
            sFlName = sFlName.Replace("'", "");
            //sFlName = sFlName.Replace("\\", "");                                                // Allow backslash to designate a folder
            sFlName = sFlName.Replace("/", "");

            if (sFlName == "") sFlName = "noname";

            return sFlName;
        }



        public static void SetWarningLabel(LabelControl lb)
        {
            //if (App.usWarnBG == null || App.usWarnBG == 0) App.usWarnBG = Color.Transparent.ToArgb();
            //if (App.usWarnFG == null || App.usWarnFG == 0) App.usWarnFG = Color.Blue.ToArgb();

            //lb.BackColor = Color.FromArgb(App.usWarnBG);
            //lb.ForeColor = Color.FromArgb(App.usWarnFG);
            lb.Visible = false;
        }



        public static void BeepOnError(ValidationRuleBase vrule)
        {
            bool beep = false;

            //if (App.usBeep != gv.ibeepNo)
            //{
            //    if (vrule.ErrorType == ErrorType.Critical)
            //    {
            //        if ((App.usBeep == 0) || (App.usBeep == 1) || (App.usBeep == 2)) SystemSounds.Beep.Play();
            //        beep = (App.usBeep < gv.ibeepNo);
            //    }
            //    if (vrule.ErrorType == ErrorType.Warning)
            //    {
            //        if ((App.usBeep == 1) || (App.usBeep == 2)) SystemSounds.Beep.Play();
            //        beep = (App.usBeep < gv.ibeepErrWarnMsg);
            //    }
            //}

            if (beep) SystemSounds.Beep.Play();
        }

        public static void BeepOnMessage()
        {
            //if (App.usBeep != 3)
            //{
            //    if (App.usBeep == 2) SystemSounds.Beep.Play();
            //}
        }


        public static void SetFocus(System.Windows.Forms.Control objControl)
        {
            if ((!objControl.ContainsFocus) && (objControl.Enabled))
                objControl.Focus();
        }

        public static void ShowHelp()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            string hid = "overview";

            if (gv.HelpID != null && gv.HelpID != "") hid = gv.HelpID;

            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = "http://1ta.1t-s.com/index.php?pg=" + hid;
            p.Start();
        }

        public static string SizeInBytes(long lNo)
        {
            // Return byte size as bytes, k, MB, Gb etc..

            string rtn = "0";
            long k = 1024;
            long lMB = k * k;
            long lGB = lMB * k;
            long lTB = lGB * k;

            if (lNo < k)
            {
                rtn = lNo.ToString();
            }
            else if (lNo < lMB)
            {
                rtn = (lNo / 1024).ToString("f2") + "k";
            }
            else if (lNo < lGB)
            {
                rtn = (lNo / lMB).ToString("f2") + "MB";
            }
            else if (lNo < lTB)
            {
                rtn = (lNo / lMB).ToString("f2") + "GB";
            }
            else
            {
                rtn = lNo.ToString("f0") + "bytes";
            }
            return rtn;
        }


        public static string GetFieldList(DataTable table, bool bCopyToClipboard = false)
        {
            string sTxt = "";
            int i = 0;
            while (i < table.Columns.Count)
            {
                sTxt += table.Columns[i].ColumnName + ", ";
                i++;
            }

            if (bCopyToClipboard)
            {
                try
                {
                    Clipboard.SetText(sTxt);
                }
                catch (Exception ex)
                {
                   
                }
            }

            //System.Windows.Forms.Clipboard.SetText(sTxt);

            //MessageBox.Show(sTxt, "Fields List", System.Windows.Forms.MessageBoxButtons.OK);

            return sTxt;
        }

        # region Find Path

        public static XmlDocument LoadXMLFile1(string fl)
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(fl);
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No xml file found to process. Please check.", e);
            }
        }

        public static XmlDocument LoadXMLFile(string filetype, string fileinfo)
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                if (filetype == "Nominal Template") doc.Load(GetNominalTemplateFile(fileinfo));
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No xml file found to process. Please check.", e);
            }
        }

        public static string GetNominalTemplateFile(string fl)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string dirName = Path.GetDirectoryName(asm.Location);
            return Path.Combine(dirName + "\\Templates\\NL\\", fl + ".xml");
        }

        public static string DefaultFileName(string LayoutName)
        {
            // Return a full pathname of the file that the default settings should be saved to / loaded from
            string filepath = Path.Combine(System.IO.Directory.GetCurrentDirectory().ToString(), "Layouts", "_" + LayoutName.Replace(' ', '_') + ".xml");
            return filepath;
        }

        public static string LayoutFileName(string GridName)
        {
            // Return a full pathname of the file that the grid layout should be saved to / loaded from
            string filepath = "";// Path.Combine(Acct.App.wsSharedDir, "Layouts\\", Acct.gv.UserLogin + "_" + GridName.Replace(' ', '_') + ".xml");
            return filepath;
        }

        public static string LogFileName(string FileName)
        {
            // Return a full pathname of a logfile to be created in the shared folder with user login & workstation ID prefix
            // FileName = filename and extension
            string filepath = "";//Path.Combine(Acct.App.wsSharedDir, Acct.gv.UserLogin + Acct.gv.PCName + "_" + FileName);
            return filepath;
        }

        public static string TempFileName(string sName)
        {
            // Return a full pathname of a file based on supplied name, user login ID and temp folder
            string tmp = Environment.GetEnvironmentVariable("TEMP");
            if (tmp == "") tmp = Environment.GetEnvironmentVariable("TMP");

            // If we can not find the temp folder defined, just use the application folder
            if (tmp == "") tmp = gv.AppDir;

            // Return a full pathname of the file 
            string filepath = "";//Path.Combine(tmp, Acct.gv.UserLogin + "_" + sName);
            return filepath;
        }

        


        public static string GetLookupLayoutPath(string browsename)
        {
            // Use LayoutFileName() instead of GetLookupLayoutPath()
            //    string filepath = "";
            //    filepath = GetUserFolderPath("\\Layouts\\Lookups\\");
            //    if (browsename == "Country") filepath = filepath + "cntry.xml";
            //    return filepath;
            return browsename;
        }


        public static string IncludesLeadingTrails(string param)
        {
            if (param.EndsWith("\\")) return param; else return param + "\\";
        }


        public static string GetUserFolderPath(string Subfolders)
        {
            string p = "";
            p = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            p = IncludesLeadingTrails(p) + "1T Accounting" + Subfolders;
            if (!Directory.Exists(p)) Directory.CreateDirectory(p);
            return p;
        }

        public static string GetReportFilePath(string FolderName, string repxFile)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string dirName = Path.GetDirectoryName(asm.Location);
            if (!File.Exists(Path.Combine(dirName + "\\Reports\\" + FolderName)))
                Directory.CreateDirectory(Path.Combine(dirName + "\\Reports\\" + FolderName));
            return Path.Combine(dirName + "\\Reports\\" + FolderName, repxFile);
        }

        public static string GetReportPath(string FolderName, DevExpress.XtraReports.UI.XtraReport fReport, string ext)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string repName = fReport.Name;
            if (repName.Length == 0)
                repName = fReport.GetType().Name;
            string dirName = Path.GetDirectoryName(asm.Location);
            return Path.Combine(dirName + "\\Reports\\" + FolderName, repName + "." + ext);
        }

        public static void ResetGridCustomization()
        {
            string filepath = "";
            //filepath = GetUserFolderPath("\\Layouts\\BrowseGrid\\");
            filepath = System.IO.Directory.GetCurrentDirectory().ToString() + "\\GridLayouts\\"; ;
            foreach (string f in Directory.GetFiles(filepath))
            {
                FileInfo fi = new FileInfo(f);
                fi.Delete();
            }
        }

        public static string GetEmailTemplateDefaultFolder()
        {
            string dirName = "";
            if ((App.wsTemplatesDir == "") || (App.wsTemplatesDir == null))
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                dirName = Path.GetDirectoryName(asm.Location);
            }
            else dirName = App.wsTemplatesDir;
            return Path.Combine(dirName + "\\Templates\\Default\\Emails\\");
        }

        public static string GetEmailTemplateDefaultPath(string doctype, string subtype)
        {
            string rootpath = GetEmailTemplateDefaultFolder();
            return Path.Combine(rootpath, doctype + "\\" + subtype + "\\" + "template.xml");
        }

        public static string GetTemplateDefaultFolder()
        {
            string dirName = "";
            if ((App.wsTemplatesDir == "") || (App.wsTemplatesDir == null))
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                dirName = Path.GetDirectoryName(asm.Location);
            }
            else dirName = App.wsTemplatesDir;
            return Path.Combine(dirName + "\\Templates\\Default\\");
        }

        public static string GetTemplateDefaultPath(string doctype, string subtype)
        {
            string rootpath = GetTemplateDefaultFolder();
            return Path.Combine(rootpath, doctype + "\\" + subtype + "\\" + "template.repx");
        }

        public static string GetTemplateFolder()
        {
            string dirName = "";
            if ((App.wsTemplatesDir == "") || (App.wsTemplatesDir == null))
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                dirName = Path.GetDirectoryName(asm.Location);
            }
            else dirName = App.wsTemplatesDir;
            return Path.Combine(dirName + "\\Templates\\");
        }

        public static string GetEmailTemplateFolder()
        {
            string dirName = "";
            if ((App.wsTemplatesDir == "") || (App.wsTemplatesDir == null))
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                dirName = Path.GetDirectoryName(asm.Location);
            }
            else dirName = App.wsTemplatesDir;
            return Path.Combine(dirName + "\\Templates\\Emails\\");
        }

        public static string GetTemplateRoot(string doctype)
        {
            string rootpath = GetTemplateFolder();
            return Path.Combine(rootpath, doctype + "\\");
        }

        public static string GetEmailTemplateRoot(string doctype)
        {
            string rootpath = GetEmailTemplateFolder();
            return Path.Combine(rootpath, doctype + "\\");
        }

        public static string GetEmailTemplatePath(string doctype, string subtype)
        {
            string rootpath = GetEmailTemplateRoot(doctype);
            return Path.Combine(rootpath, subtype + "\\");
        }

        public static string GetTemplatePath(string doctype, string subtype)
        {
            string rootpath = GetTemplateRoot(doctype);
            return Path.Combine(rootpath, subtype + "\\");
        }

        public static string GetDefaultFileNameInFolder(string Folder, string ftxt)
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("fname", System.Type.GetType("System.String"));


            int lcnt = 0;
            foreach (string f in Directory.GetFiles(Folder))
            {
                FileInfo fi = new FileInfo(f);
                dtbl.Rows.Add(new object[] { fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length) });
            }
            DataTable dtmp = dtbl;

            string ftxttemp = ftxt;
            int i = 0;
            bool unique = false;
            while (!unique)
            {
                i++;
                bool findfile = false;
                foreach (DataRow dr in dtbl.Rows)
                {
                    if (dr["fname"].ToString() == ftxttemp)
                    {
                        ftxttemp = ftxt + i.ToString();
                        findfile = true;
                        break;
                    }
                }
                if (!findfile)
                {
                    findfile = true;
                    unique = true;
                }
            }

            return ftxttemp;
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            // Make sure both source and target Dir end with \

            if (source.FullName.ToLower() == target.FullName.ToLower()) return;

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        public static int CopyAllNewer(DirectoryInfo source, DirectoryInfo target)
        {
            // Returns number of files copied
            // This routine makes recursive calls to itself. If there is an error within nested calls, we need a way to quit and abort the copy process
            // So, if bContinue = false, we are aborting and should not attempt to copy any more files
            bool bError = false;
            int intCount = 0;

            if (bContinueCopyAllNewer)
            {
                // Make sure both source and target Dir end with \
                if (source.FullName.ToLower() == target.FullName.ToLower())
                {
                    XtraMessageBox.Show("Source and destination folders are the same, please select a different destination folder.", "Copy Files", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    bContinueCopyAllNewer = false;
                }

                if (Directory.Exists(source.FullName) == false)
                {
                    XtraMessageBox.Show("Source folder ( " + source + " ) does not exist !.", "Copy Files", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    bContinueCopyAllNewer = false;
                }
            }

            if (bContinueCopyAllNewer)
            {
                try
                {
                    // Check if the target directory exists, if not, create it.
                    if (Directory.Exists(target.FullName) == false)
                    {
                        Directory.CreateDirectory(target.FullName);
                    }

                    // Copy each file into it's new directory.
                    string strMsg = "";
                    string sourceFile = "";
                    string destFile = "";
                    DialogResult dlrAnswer = DialogResult.Yes;

                    DateTime dtSourceFile = System.DateTime.Now;
                    DateTime dtDestFile = System.DateTime.Now;

                    foreach (FileInfo fi in source.GetFiles())
                    {
                        sourceFile = fi.FullName;
                        destFile = Path.Combine(target.ToString(), fi.Name);

                        if (File.Exists(destFile))
                        {
                            dlrAnswer = DialogResult.Yes;
                            dtSourceFile = fi.CreationTime;
                            dtDestFile = File.GetCreationTime(destFile);

                            if (dtDestFile > dtSourceFile)
                            {
                                strMsg = "Destination File is Newer, Overwrite ?\r\n" + "     Source: " + sourceFile + " " + dtSourceFile.ToString("dd/MM/yyyy hh:mm:ss") + "\r\n" +
                                    "Destination: " + destFile + " " + dtDestFile.ToString("dd/MM/yyyy hh:mm:ss");

                                dlrAnswer = XtraMessageBox.Show(strMsg, "Overwrite File ?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (dlrAnswer == DialogResult.Cancel)
                                {
                                    bContinueCopyAllNewer = false;
                                    break;
                                }
                            }
                        }

                        if (bContinueCopyAllNewer && dlrAnswer == DialogResult.Yes)
                        {
                            fi.CopyTo(destFile, true);
                            intCount += 1;
                        }
                    }

                    if (bContinueCopyAllNewer)
                    {
                        // Copy each subdirectory using recursion.
                        foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                        {
                            try
                            {
                                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                                intCount += CopyAllNewer(diSourceSubDir, nextTargetSubDir);
                            }
                            catch (Exception ex)
                            {
                                //AcctMsg.MsgExceptionErr(ex, "00378", 0, "Error Copying Files");
                                bContinueCopyAllNewer = false;
                                break;
                            }
                        }
                    }

                }

                catch (Exception ex)
                {
                    //AcctMsg.MsgExceptionErr(ex, "00377", 0, "Error copying files");
                    bContinueCopyAllNewer = false;
                }
            }
            return intCount;
        }

        #endregion



       


    }
}
