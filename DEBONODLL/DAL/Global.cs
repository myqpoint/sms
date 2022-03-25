
using DebonoDLL;
using DebonoDLL.BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Debono.DAL
{
   public class Global
    {
       UserMasterBo objuserMasterBo = new UserMasterBo();
       public static string UserId = "";
       public static string Role = "";
       public static Boolean btime = false;

      public bool setRole(string formname)
       {
           return false;
           //DataTable dt = new DataTable();
           //try
           //{
           //    bool ret=false;
           //    string Role = objuserMasterBo.GetRole(Global.UserId.ToUpper().Trim());
           //    if (Role.ToUpper() == "ADMIN")
           //    { return true; }

           //    objuserMasterBo._UId = Global.UserId.ToUpper().Trim();
           //    dt = objuserMasterBo.GetRights(Global.UserId.ToUpper());
           //    foreach (DataRow dr in dt.Rows)
           //    {
           //        string Screen = dr["FormName"].ToString().Trim().ToUpper();
           //        if (formname.ToUpper() == Screen)
           //        {
           //            ret = true;
           //        }
                   
           //    }
           //    if (ret == true)
           //    { return true; }
           //    else
           //    { return false; }
           //}
           //catch (Exception ex)
           //{              
           //    ExceptionManager.LogException(ex);
           //    return false;
           //}
           //finally
           //{
           //    dt = null;
           //}
       }

    }
}
