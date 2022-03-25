using Debono;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DEBONO
{
    static class GlobleData
    {
        public static DataTable dtUserRoleAccess = new DataTable();

        public static bool ManageRoleAccessView(string strScreenName)
        {

            if (dtUserRoleAccess != null)
            {
                //if (strScreenName.Contains("Detail") || strScreenName.Contains("OrdrDetail"))
                //{
                //    strScreenName = strScreenName.Replace("Detail", "");
                //    strScreenName = strScreenName.Trim() + "Info";
                //}
                //if (strScreenName.Contains("ProductInfo"))
                //{
                //    strScreenName = strScreenName.Replace("Info", "nfo");
                //    // strScreenName = strScreenName.Trim() + "Info";
                //}
                if (strScreenName.Contains("Ordr"))
                {
                    strScreenName = strScreenName.Replace("Ordr", "Order");
                    // strScreenName = strScreenName.Trim() + "Info";
                }
                DataRow[] drArr = dtUserRoleAccess.Select("ScreenName='" + strScreenName + "' and ViewAccess=" + true);
                if (drArr != null && drArr.Length > 0)
                {
                    return true;
                }
            }
            DebonoMsg.MsgError("Access Denied !");
            return false;
        }

        public static bool ManageRoleAccessViewDetail(string strScreenName)
        {

            if (dtUserRoleAccess != null)
            {
                //if (strScreenName.Contains("Detail") || strScreenName.Contains("OrdrDetail"))
                //{
                //    strScreenName = strScreenName.Replace("Detail", "");
                //    strScreenName = strScreenName.Trim() + "Info";
                //}
                //if (strScreenName.Contains("ProductInfo"))
                //{
                //    strScreenName = strScreenName.Replace("Info", "nfo");
                //    // strScreenName = strScreenName.Trim() + "Info";
                //}
                if (strScreenName.Contains("Ordr"))
                {
                    strScreenName = strScreenName.Replace("Ordr", "Order");
                    // strScreenName = strScreenName.Trim() + "Info";
                }
                DataRow[] drArr = dtUserRoleAccess.Select("ScreenName='" + strScreenName + "' and ViewAccess=" + true);
                if (drArr != null && drArr.Length > 0)
                {
                    return true;
                }
            }
            DebonoMsg.MsgError("Access Denied !");
            return false;
        }
        public static bool ManageRoleAccessEdit(string strScreenName)
        {
            if (dtUserRoleAccess != null)
            {
                //if (strScreenName.Contains("Detail") || strScreenName.Contains("OrdrDetail"))
                //{
                //    strScreenName = strScreenName.Replace("Detail", "");
                //    strScreenName = strScreenName.Trim() + "Info";
                //}
                //if (strScreenName.Contains("ProductInfo"))
                //{
                //    strScreenName = strScreenName.Replace("Info", "nfo");
                //    // strScreenName = strScreenName.Trim() + "Info";
                //}
                if (strScreenName.Contains("Ordr"))
                {
                    strScreenName = strScreenName.Replace("Ordr", "Order");
                    // strScreenName = strScreenName.Trim() + "Info";
                }
                DataRow[] drArr = dtUserRoleAccess.Select("ScreenName='" + strScreenName + "' and EditAccess=" + true);
                if (drArr != null && drArr.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ManageRoleAccessEditDetail(string strScreenName)
        {
            if (dtUserRoleAccess != null)
            {
                //if (strScreenName.Contains("Detail") || strScreenName.Contains("OrdrDetail"))
                //{
                //    strScreenName = strScreenName.Replace("Detail", "");
                //    strScreenName = strScreenName.Trim() + "Info";
                //}
                //if (strScreenName.Contains("ProductInfo"))
                //{
                //    strScreenName = strScreenName.Replace("Info", "nfo");
                //    // strScreenName = strScreenName.Trim() + "Info";
                //}
                if (strScreenName.Contains("Ordr"))
                {
                    strScreenName = strScreenName.Replace("Ordr", "Order");
                    // strScreenName = strScreenName.Trim() + "Info";
                }
                DataRow[] drArr = dtUserRoleAccess.Select("ScreenName='" + strScreenName + "' and EditAccess=" + true);
                if (drArr != null && drArr.Length > 0)
                {
                    return true;
                }
            } 
            return false;
        }
        public static bool ManageRoleAccessDeleteDetail(string strScreenName)
        {
            if (dtUserRoleAccess != null)
            {
                //if (strScreenName.Contains("Detail") || strScreenName.Contains("OrdrDetail"))
                //{
                //    strScreenName = strScreenName.Replace("Detail", "");
                //    strScreenName = strScreenName.Trim() + "Info";
                //}
                //if (strScreenName.Contains("ProductInfo"))
                //{
                //    strScreenName = strScreenName.Replace("Info", "nfo");
                //    // strScreenName = strScreenName.Trim() + "Info";
                //}
                if (strScreenName.Contains("Ordr"))
                {
                    strScreenName = strScreenName.Replace("Ordr", "Order");
                    // strScreenName = strScreenName.Trim() + "Info";
                }
                //                DataRow[] drArr = dtUserRoleAccess.Select(("ScreenName='" + strScreenName + "' or ").Contains(strScreenName)+"   and DeleteAccess=" + true);
                DataRow[] drArr = dtUserRoleAccess.Select("ScreenName='" + strScreenName + "' and DeleteAccess=" + true);

                if (drArr != null && drArr.Length > 0)
                {
                    return true;
                }
            }
            return false;
        } 
        //created by ajay
        public static bool ManageRoleAccessDelete(string strScreenName)
        {
            if (dtUserRoleAccess != null)
            {
                if (strScreenName.Contains("Detail") || strScreenName.Contains("OrdrDetail"))
                {
                    strScreenName = strScreenName.Replace("Detail", "");
                    strScreenName = strScreenName.Trim() + "Info";
                }
                if (strScreenName.Contains("ProductInfo"))
                {
                    strScreenName = strScreenName.Replace("Info", "nfo");
                    // strScreenName = strScreenName.Trim() + "Info";
                }
                if (strScreenName.Contains("Ordr"))
                {
                    strScreenName = strScreenName.Replace("Ordr", "Order");
                    // strScreenName = strScreenName.Trim() + "Info";
                }
//                DataRow[] drArr = dtUserRoleAccess.Select(("ScreenName='" + strScreenName + "' or ").Contains(strScreenName)+"   and DeleteAccess=" + true);
                DataRow[] drArr = dtUserRoleAccess.Select("ScreenName='" + strScreenName + "' and DeleteAccess=" + true);
                
                if (drArr != null && drArr.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ManageRoleAccessLockEditDetail(string strScreenName)
        {
            if (dtUserRoleAccess != null)
            {
                //if (strScreenName.Contains("Detail") || strScreenName.Contains("OrdrDetail"))
                //{
                //    strScreenName = strScreenName.Replace("Detail", "");
                //    strScreenName = strScreenName.Trim() + "Info";
                //}
                //if (strScreenName.Contains("ProductInfo"))
                //{
                //    strScreenName = strScreenName.Replace("Info", "nfo");
                //    // strScreenName = strScreenName.Trim() + "Info";
                //}
                if (strScreenName.Contains("Ordr"))
                {
                    strScreenName = strScreenName.Replace("Ordr", "Order");
                    // strScreenName = strScreenName.Trim() + "Info";
                }
                //                DataRow[] drArr = dtUserRoleAccess.Select(("ScreenName='" + strScreenName + "' or ").Contains(strScreenName)+"   and DeleteAccess=" + true);
                DataRow[] drArr = dtUserRoleAccess.Select("ScreenName='" + strScreenName + "' and LockEditAccess=" + true);

                if (drArr != null && drArr.Length > 0)
                {
                    return true;
                }
            }
            return false;
        } 
        // add by islam for lock edit 
        public static bool ManageRoleAccessLockEdit(string strScreenName)
        {
            if (dtUserRoleAccess != null)
            {
                //if (strScreenName.Contains("Detail") || strScreenName.Contains("OrdrDetail"))
                //{
                //    strScreenName = strScreenName.Replace("Detail", "");
                //    strScreenName = strScreenName.Trim() + "Info";
                //}
                //if (strScreenName.Contains("ProductInfo"))
                //{
                //    strScreenName = strScreenName.Replace("Info", "nfo");
                //    // strScreenName = strScreenName.Trim() + "Info";
                //}
                if (strScreenName.Contains("Ordr"))
                {
                    strScreenName = strScreenName.Replace("Ordr", "Order");
                    // strScreenName = strScreenName.Trim() + "Info";
                }
                //                DataRow[] drArr = dtUserRoleAccess.Select(("ScreenName='" + strScreenName + "' or ").Contains(strScreenName)+"   and DeleteAccess=" + true);
                DataRow[] drArr = dtUserRoleAccess.Select("ScreenName='" + strScreenName + "' and LockEditAccess=" + true);

                if (drArr != null && drArr.Length > 0)
                {
                    return true;
                }
            }
            return false;
        }       
    }
}
