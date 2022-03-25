using System;
using System.Collections.Generic;

using System.Text; 
using System.Configuration;
using System.Windows.Forms;
using System.Xml;
using System.Data; 
using System.Data.Sql;
using DebonoDLL.App_Code.DAL;

namespace DebonoDLL
{
    public class Main
    {
        public static Boolean isTimeOut;
        public static String strRichMessage = "";

        public void LoadConnection(String StrCon)
        {
            if (ConfigurationSettings.AppSettings["connectionString"] != null)
                Dal.strMainCon = ConfigurationSettings.AppSettings["connectionString"].ToString();
            else
                Dal.strMainCon = StrCon;

            if (ConfigurationSettings.AppSettings["connectionString2"] != null)
                Dal.strBackupCon = ConfigurationSettings.AppSettings["connectionString2"].ToString();
            else
                Dal.strBackupCon = StrCon;

            //strCon = ConfigurationSettings.AppSettings["connectionString"].ToString();
        }

       

    }
}
