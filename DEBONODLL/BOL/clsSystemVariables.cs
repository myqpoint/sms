#region Unit Header
/*
{*******************************************************************}
{                                                                   }
{	 Purpose : Class for Database Connection Static Variables	    }
{                                                                   }
{*******************************************************************}
*/
#endregion Unit Header


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Debono
{
    public class SystemVariables
    {
        /// <summary>
        /// private variables
        /// </summary>
        /// 
        private static SqlConnection gconn = null;
        private static string _Mainconnstring = "";
        private static SqlConnection sqlco_conn = null;
        private static string sqlco_connstring = "";

        private static SqlConnection sqlco_conn_temp = null;
        private static string sqlco_connstring_temp = "";

        private static string strco_active = "";
        private static string strco_activenm = "";

        private static string gdbserver = "";
        private static string gdbuserid = "";
        private static string gdbpswd = "";

        private static bool blIsTempConnection = false;


        /// <summary>
        /// Flag responsible for temporary connection 
        /// </summary>
        public static bool IsTempConnection
        {
            get { return blIsTempConnection; }
            set { blIsTempConnection = value; }
        }

        /// <summary>
        /// sql server connection
        /// </summary>
        public static SqlConnection conn
        {
            get { return gconn; }
            set { gconn = value; }
        }

        /// <summary>
        /// company sql server connection
        /// </summary>
        public static SqlConnection co_conn
        {
            get { return sqlco_conn; }
            set { sqlco_conn = value; }
        }

        /// <summary>
        /// temporary company sql server connection 
        /// </summary>
        public static SqlConnection co_conn_temp
        {
            get { return sqlco_conn_temp; }
            set { sqlco_conn_temp = value; }
        }

        /// <summary>
        /// sql server connection string
        /// </summary>
        public static string Mainconnstring
        {
            get { return _Mainconnstring; }
            set { _Mainconnstring = value; }
        }

        /// <summary>
        /// company sql server connection string
        /// </summary>
        public static string co_connstring
        {
            get { return sqlco_connstring; }
            set { sqlco_connstring = value; }
        }

         

        /// <summary>
        /// active company code
        /// </summary>
        public static string co_active
        {
            get { return strco_active; }
            set { strco_active = value; }
        }

        /// <summary>
        /// active company name
        /// </summary>
        public static string co_activenm
        {
            get { return strco_activenm; }
            set { strco_activenm = value; }
        }

        /// <summary>
        /// Server Name
        /// </summary>
        public static string dbserver
        {
            get { return gdbserver; }
            set { gdbserver = value; }
        }

        /// <summary>
        /// Database User ID
        /// </summary>
        public static string dbuserid
        {
            get { return gdbuserid; }
            set { gdbuserid = value; }
        }

        /// <summary>
        /// Database Password
        /// </summary>
        public static string dbpswd
        {
            get { return gdbpswd; }
            set { gdbpswd = value; }
        }
    }
}
