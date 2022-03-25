#region Unit Header
/*
{*******************************************************************}
{                                                                   }
{		Purpose : Class for Settings parameters                     }
{                                                                   }
{*******************************************************************}
*/
#endregion Unit Header

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;
using DebonoDLL.App_Code.DAL;

namespace Debono
{
    public class SetupParam
    {
        public SetupParam()
        {
        }

        /// <summary>
        /// private variables
        /// </summary>
        /// 
        #region fields

        private SqlConnection sqlconn = null;
        private string strkey;
        private string strval;
        private string strref;
        private int intty;

        private string lg_sty;
        private string lg_sstn;
        private string lg_snotes;
        private int lg_ireff;
        private int lg_iusr;

        private string r_module;

        /// <summary>
        /// sql server connection
        /// </summary>
        public SqlConnection conn
        {
            get { return sqlconn; }
            set { sqlconn = value; }
        }

        public string lg_ty
        {
            get { return lg_sty; }
            set { lg_sty = value; }
        }

        public string lg_stn
        {
            get { return lg_sstn; }
            set { lg_sstn = value; }
        }

        public string lg_notes
        {
            get { return lg_snotes; }
            set { lg_snotes = value; }
        }

        public int lg_reff
        {
            get { return lg_ireff; }
            set { lg_ireff = value; }
        }

        public int lg_usr
        {
            get { return lg_iusr; }
            set { lg_iusr = value; }
        }

        public string s_key
        {
            get { return strkey; }
            set { strkey = value; }
        }

        public string s_val
        {
            get { return strval; }
            set { strval = value; }
        }

        public string s_ref
        {
            get { return strref; }
            set { strref = value; }
        }

        public int i_ty
        {
            get { return intty; }
            set { intty = value; }
        }

        public string report_module
        {
            get { return r_module; }
            set { r_module = value; }
        }
        #endregion

        /// <summary>
        /// Set database connection before calling a function in this class
        /// </summary>
        public void SetConn()
        {
            if (!SystemVariables.IsTempConnection) sqlconn = SystemVariables.co_conn; else sqlconn = SystemVariables.co_conn_temp;
        }

        public string IsExistParamValue()
        {
            string cnt = null;

            string strSQLComm = " select vlu from cfgrt where ty = @ty and ref = @ref and ke = @key and vlu=@vlu ";


            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ty", intty);
            param[1] = new SqlParameter("@key", strkey);
            param[2] = new SqlParameter("@ref", strref);
            param[3] = new SqlParameter("@vlu", strval);

            try
            {
                Dal objDal = new Dal();
                DataTable dt = objDal.ExecuteTable(strSQLComm, param);

                foreach (DataRow dr in dt.Rows)
                {
                    cnt = dr["vlu"].ToString();
                }

                return cnt;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                return null;
            }
        }

        public string IsExistParam()
        {
            string cnt = null;

            string strSQLComm = " select vlu from cfgrt where ty = @ty and ref = @ref and ke = @key ";


            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ty", intty);
            param[1] = new SqlParameter("@key", strkey);
            param[2] = new SqlParameter("@ref", strref);


            try
            {
                Dal objDal = new Dal();
                DataTable dt = objDal.ExecuteTable(strSQLComm, param);
                foreach (DataRow dr in dt.Rows)
                {
                    cnt = dr["vlu"].ToString();
                }

                return cnt;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                return null;
            }
        }

        public string InsertData()
        {
            string strSQLComm = " insert into cfgrt( ty,ref,ke,vlu ) values ( @ty,@ref,@key,@val ) ";


            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@ty", intty);
                param[1] = new SqlParameter("@ref", strref);
                param[2] = new SqlParameter("@key", strkey);
                param[3] = new SqlParameter("@val", strval);
                Dal objDal = new Dal();
                objDal.ExecuteDataIdentity(strSQLComm, param);


                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
        }

        public string UpdateData()
        {
            string strSQLComm = " update cfgrt set vlu = @val where ty = @ty and ref = @ref and ke = @key  ";


            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@ty", intty);
                param[1] = new SqlParameter("@ref", strref);
                param[2] = new SqlParameter("@key", strkey);
                param[3] = new SqlParameter("@val", strval);
                Dal objDal = new Dal();
                int result = objDal.ExecuteDataIdentity(strSQLComm, param);

                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";

                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }

        }

        public void ResetWindowPosition()
        {
            // BK 07/02/2013 Dead. Functionality no longer required.
            // Forms / windows possitions no longer saved to table cfgrt. Instead they are all saved to a single text file.
            // see clsSettings.cs.SaveWinPosSize()

            string strSQLComm = " delete from cfgrt where ke like 'Window Position%' and ty = 2 and ref = @ref ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@ref", strref);

                Dal objDal = new Dal();
                objDal.ExecuteDataIdentity(strSQLComm, param);

                return;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";

                strErrMsg = SQLDBException.Message;
                return;
            }
        }

        public DataTable FetchEmailToAddress()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select top 10 vlu from cfgrt where ke = @key order by id desc";

            SqlCommand objSQlComm = new SqlCommand(strSQLComm, sqlconn);
            SqlDataReader objSQLReader = null;

            try
            {


                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@key", strkey);
                Dal objDal = new Dal();
                DataTable dt = objDal.ExecuteTable(strSQLComm, param);
                foreach (DataRow dr in dt.Rows)
                {
                    dtbl.Rows.Add(new object[] { dr["vlu"].ToString() });
                }

                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                return null;
            }
        }
        /*

        public int GetDeliveryAddressTypeID()
        {
            int cnt = 0;

            string strSQLComm = " select id from lists where list = 'AdrTy' and no <> -1 and dta = 'Delivery' ";

            Dal objDal = new Dal();
            DataTable dt = objDal.ExecuteTable(strSQLComm);
            try
            {

                foreach (DataRow dr in dt.Rows)
                {
                    cnt = Functions.fnInt32(dr["id"].ToString());
                }

                return cnt;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return 0;
            }
        }


       

        #region Logs

        public bool IsExistsLogs()
        {
            int cnt = 0;

            string strSQLComm = " select count(*) as rcnt from logs where ty = @ty and id_rec = @reff ";

            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ty", lg_sty);
                param[1] = new SqlParameter("@reff", lg_ireff);
                Dal objDal = new Dal();
                DataTable dt = objDal.ExecuteTable(strSQLComm);
                foreach (DataRow dr in dt.Rows)
                {
                    cnt = Functions.fnInt32(dr["rcnt"].ToString());
                }

                return (cnt > 0);
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return false;
            }
        }

        public DataTable FetchLogs()
        {
            DataTable dtbl = new DataTable();

            string strSQLComm = " select l.*,isnull(u.usr,'Admin') as unm from logs l left outer join access u on u.id = l.usr "
                              + " where l.ty = @ty and l.id_rec = @reff and l.f_del = 0 order by l.id desc";

            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ty", lg_sty);
                param[1] = new SqlParameter("@reff", lg_ireff);
                Dal objDal = new Dal();
                DataTable dt = objDal.ExecuteTable(strSQLComm, param);

                dtbl.Columns.Add("id", System.Type.GetType("System.String"));
                dtbl.Columns.Add("dttm", System.Type.GetType("System.String"));
                dtbl.Columns.Add("usr", System.Type.GetType("System.String"));
                dtbl.Columns.Add("usrnm", System.Type.GetType("System.String"));
                dtbl.Columns.Add("stn", System.Type.GetType("System.String"));
                dtbl.Columns.Add("notes", System.Type.GetType("System.String"));
                dtbl.Columns.Add("disp", System.Type.GetType("System.String"));
                dtbl.Columns.Add("new", System.Type.GetType("System.String"));

                foreach (DataRow dr in dt.Rows)
                {
                    dtbl.Rows.Add(new object[] { dr["id"].ToString(), 
                                                 Functions.fnDate(dr["dttm"].ToString()).ToString("g"),
                                                 dr["usr"].ToString(),
                                                 dr["unm"].ToString(),
                                                 dr["stn"].ToString(),
                                                 dr["notes"].ToString(),
                                                 dr["disp"].ToString(),"N"});
                }
                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;

                return null;
            }
        }

        public int InsertLogs()
        {
            string strSQLComm = "sp_insertlog";
            try
            {
                int val = -1;
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@ty", lg_sty);
                param[1] = new SqlParameter("@usr", lg_iusr);
                param[2] = new SqlParameter("@stn", lg_sstn);
                param[3] = new SqlParameter("@notes", lg_snotes);
                param[4] = new SqlParameter("@reff", lg_ireff);
                Dal objDal = new Dal();
                objDal.ExecuteSPDataIdentity(strSQLComm, param);
                return 0;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return -1;
            }

        }

        #endregion


        #region Reports

        public DataTable FetchReports()
        {
            DataTable dtbl = new DataTable();
            DataTable dt = null;
            string strSQLComm = "";

            string sql = "";
            if (r_module != "")
            {
                sql = " and module = @m ";
            }
            strSQLComm = " select module,section,[group],report from reports where 1 = 1 " + sql
                       + " order by ord ";


            try
            {
                // if (sqlconn.State == System.Data.ConnectionState.Closed) { sqlconn.Open(); }

                if (r_module != "")
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@m", r_module);
                    Dal objDal = new Dal();
                    dt = objDal.ExecuteTable(strSQLComm, param);
                }
                else
                {
                    Dal objDal = new Dal();
                    dt = objDal.ExecuteTable(strSQLComm);
                }


                dtbl.Columns.Add("module", System.Type.GetType("System.String"));
                dtbl.Columns.Add("section", System.Type.GetType("System.String"));
                dtbl.Columns.Add("group", System.Type.GetType("System.String"));
                dtbl.Columns.Add("report", System.Type.GetType("System.String"));

                foreach (DataRow dr in dt.Rows)
                {
                    dtbl.Rows.Add(new object[] { 
                                                 dr["module"].ToString(),
                                                 dr["section"].ToString(),
                                                 dr["group"].ToString(),
                                                 dr["report"].ToString()});
                }

                return dtbl;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                return null;
            }
        }


        #endregion
         */
    }
}

