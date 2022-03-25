using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using DebonoDLL.App_Code.BOL;
using DebonoDLL;

namespace DebonoDLL.App_Code.DAL
{
    public class Dal
    {
        //public static string strCon = ConfigurationSettings.AppSettings["connectionString"].ToString();
        public Dal(Boolean isBackDB)
        {
            if (isBackDB)
                strCon = strBackupCon;
            else
                strCon = strMainCon;
        }

        public Dal()
        {
            strCon = strMainCon;
        }

        string strCon = "";
        public static string strMainCon = "";

        public static string strBackupCon = "";


        public int ExecuteDataIdentity(string strQuery)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        return SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strQuery);
                    }
                    else
                        return 0;
                }
                catch (Exception ex)
                {
                    ExceptionManager.LogException(ex);
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        return SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strQuery);
                    }
                    else
                        return 0;

                    throw ex;
                }
            }
            else
                return 0;

        }

        

        public int ExecuteDataIdentityShowError(string strQuery)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        return SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strQuery);
                    }
                    else
                        return 0;
                }
                catch (Exception ex)
                {
                    ExceptionManager.LogException(ex);
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        return SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strQuery);
                    }
                    else
                        throw ex;
                }
            }
            else
                return 0;

        }

        public int ExecuteDataIdentity(string strQuery, SqlParameter[] objparam)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        return SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strQuery, objparam);
                    }
                    else
                        return 0;
                }
                catch (SqlException ex)
                {
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    ExceptionManager.LogException(ex);
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        return SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strQuery, objparam);
                    }
                    //else
                    //    return 0;

                    throw ex;
                }
            }
            return 0;
        }

        public int ExecuteSPDataIdentity(string strQuery, SqlParameter[] objparam)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        return SqlHelper.ExecuteNonQuery(strCon, CommandType.StoredProcedure, strQuery, objparam);
                    }
                    else
                        return 0;
                }
                catch (Exception ex)
                {
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    ExceptionManager.LogException(ex);
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        return SqlHelper.ExecuteNonQuery(strCon, CommandType.StoredProcedure, strQuery, objparam);
                    }
                    else
                        return 0;

                    throw ex;
                }
            }
            else
                return 0;
        }

        public void ExecuteData(string strQuery)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strQuery);
                    }
                }
                catch (Exception ex)
                {
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    ExceptionManager.LogException(ex);
                    if (ex.Message.Contains("Timeout") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strQuery);
                    }


                    throw ex;
                }
            }
        }

        public void ExecuteData(string strQuery, SqlParameter[] objparam)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strQuery, objparam);
                    }
                }
                catch (Exception ex)
                {
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    ExceptionManager.LogException(ex);
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        SqlHelper.ExecuteNonQuery(strCon, CommandType.Text, strQuery, objparam);
                    }


                    throw ex;
                }
            }
        }

        public DataSet ExecuteDataset(String strQuery)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        return SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery);
                    }
                    else
                        return null;

                }
                catch (Exception ex)
                {
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    ExceptionManager.LogException(ex);
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        return SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery);
                    }
                    else
                        return null;

                    throw ex;
                }
            }
            return null;
        }

        public DataSet ExecuteDatasetShowError(String strQuery)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        return SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery);
                    }
                    else
                        return null;

                }
                catch (Exception ex)
                {
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    ExceptionManager.LogException(ex);
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        return SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery);
                    }
                    else
                    {
                        throw ex;
                    }

                }
            }
            else
                return null;
        }

        public DataSet ExecuteDataset(String strQuery, SqlParameter[] objparam)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        return SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery, objparam);
                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    ExceptionManager.LogException(ex);
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        return SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery, objparam);
                    }
                    else
                        return null;

                    throw ex;
                }
            }
            else
                return null;
        }      

        public DataTable ExecuteTable(string strQuery)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        DataTable ds = SqlHelper.ExecuteDataTable(strCon, CommandType.Text, strQuery);
                        if (ds != null)
                            return ds;
                        else
                            return new DataTable();
                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    ExceptionManager.LogException(ex);
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        DataTable ds = SqlHelper.ExecuteDataTable(strCon, CommandType.Text, strQuery);
                        if (ds != null)
                            return ds;
                        else
                            return new DataTable();
                    }
                    else
                        return null;

                    throw ex;
                }
            }
            else
                return null;
        }

        public DataTable ExecuteTable(string strQuery, SqlParameter[] objparam)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        DataTable ds = SqlHelper.ExecuteDataTable(strCon, CommandType.Text, strQuery, objparam);
                        if (ds != null)
                            return ds;
                        else
                            return new DataTable();

                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    ExceptionManager.LogException(ex);
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        DataTable ds = SqlHelper.ExecuteDataTable(strCon, CommandType.Text, strQuery, objparam);
                        if (ds != null)
                            return ds;
                        else
                            return new DataTable();
                    }
                    else
                        return null;

                    throw ex;
                }
            }
            else
                return null;
        }

        public DataTable GetTableFromSP(string strQuery, SqlParameter[] objparam)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        DataTable ds = SqlHelper.ExecuteDataTable(strCon, strQuery, objparam);
                        if (ds != null)
                            return ds;
                        else
                            return new DataTable();
                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    ExceptionManager.LogException(ex);
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        DataTable ds = SqlHelper.ExecuteDataTable(strCon, strQuery, objparam);
                        if (ds != null)
                            return ds;
                        else
                            return new DataTable();
                    }
                    else
                        return null;

                    throw ex;
                }
            }
            else
                return null;
        }
        Conversion objCon = new Conversion();
        public Int64 ExecuteForID(string strQuery)
        {
            if (DateTime.Now.Month <= 12 && DateTime.Now.Year <= 2022)
            {
                try
                {
                    if (strCon != null & strCon != "" & strQuery != "")
                    {
                        return objCon.ConToInt64(SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery).Tables[0].Rows[0][0]);
                    }
                    else
                        return 0;
                }
                catch (Exception ex)
                {
                    if (Main.isTimeOut)
                        Main.isTimeOut = false;
                    else
                        Main.isTimeOut = true;
                    ExceptionManager.LogException(ex);
                    if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                    {
                        return objCon.ConToInt64(SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery).Tables[0].Rows[0][0]);
                    }
                    else
                        return 0;

                    throw ex;
                }
            }
            else
                return 0;
        }

        public object GetSingleValue(string strQuery)
        {
            try
            {
                if (strCon != null & strCon != "" & strQuery != "")
                {
                    DataSet dsData = SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery); ;
                    if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                        return dsData.Tables[0].Rows[0][0];
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (Main.isTimeOut)
                    Main.isTimeOut = false;
                else
                    Main.isTimeOut = true;
                ExceptionManager.LogException(ex);
                if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                {
                    DataSet dsData = SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery); ;
                    if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                        return dsData.Tables[0].Rows[0][0];
                    else
                        return null;
                }
                else
                    return null;

                throw ex;
            }
        }

        public object GetSingleValue(string strQuery, SqlParameter[] objparam)
        {
            try
            {
                if (strCon != null & strCon != "" & strQuery != "")
                {
                    DataSet dsData = SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery, objparam); ;
                    if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                        return dsData.Tables[0].Rows[0][0];
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (Main.isTimeOut)
                    Main.isTimeOut = false;
                else
                    Main.isTimeOut = true;
                ExceptionManager.LogException(ex);
                if (ex.Message.Contains("Timeout expired") || ex.Message.Contains("Zeitüberschreitung"))
                {
                    DataSet dsData = SqlHelper.ExecuteDataset(strCon, CommandType.Text, strQuery, objparam); ;
                    if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                        return dsData.Tables[0].Rows[0][0];
                    else
                        return null;
                }
                else
                    return null;

                throw ex;
            }
        }
        private void handleTimeOut()
        {

        }

    }
}
