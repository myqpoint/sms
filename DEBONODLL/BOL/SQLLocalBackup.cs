using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace DebonoDLL.BOL
{
    public class SQLLocalBackup
    {
        protected SqlConnection _conn;
        protected string _address;
        protected string _user;
        protected string _pass;
        protected string _dbname;
        public SQLLocalBackup(string Aaddress, string Auser, string Apass, string Adatabasename,string connection)
        {
            try
            {
                _conn = new SqlConnection(connection);
                _conn.Open();
                _address = Aaddress;
                _dbname = Adatabasename;
            }
            catch (Exception ex)
            {
                //	MessageBox.Show(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// /// This function checks for temporary tables since we don't want to interfere with other programs/functions
        /// </summary>
        /// <returns></returns>
        protected string findUniqueTemporaryTableName()
        {
            string name = "afpTempBackup";
            int counter = 0;
            string sql = "";
            SqlCommand _mycommand = new SqlCommand();
            _mycommand.Connection = _conn;
            while (true)
            {
                ++counter;
                sql = String.Format("SELECT OBJECT_ID('tempdb..##{0}') as xyz", name + counter.ToString());
                _mycommand.CommandText = sql;
                if (_mycommand.ExecuteScalar().ToString() == "")
                {
                    return name + counter.ToString();
                    break;
                }
            }

            return name;
        }
        /// <summary>
        /// Main backuping function
        /// </summary>
        /// <param name="AremoteTempPath">You can specify what folder do you wish to be set for your backup</param>
        /// <param name="AlocalPath">Local path where copy of your backup file</param>
        /// <param name="AtempTableName">Specify temp table name so you wont collide with other programs</param>
        public void DoLocalBackup(string AremoteTempPath, string AlocalPath)
        {
            try
            {
                if (_conn == null)
                    return;
                SqlCommand _command = new SqlCommand();
                _command.Connection = _conn;
                // nice filename on local side, so we know when backup was done
                //string fileName = _dbname + ".bak";
                //+ DateTime.Now.Year.ToString() + "-" +
                //DateTime.Now.Month.ToString() + "-" +
                //DateTime.Now.Day.ToString() + "-" + 
                //DateTime.Now.Millisecond.ToString() + ".bak";
                // we invoke this method to ensure we didnt mess up with other programs
                string temporaryTableName = findUniqueTemporaryTableName();

                string _sql;

                _sql = String.Format("BACKUP DATABASE {0} TO DISK = N'{1}\\{0}.bak' WITH FORMAT, COPY_ONLY, INIT, NAME = N'{0} - Full Database Backup', SKIP ", _dbname, AremoteTempPath, _dbname);
                _command.CommandText = _sql;
                _command.ExecuteNonQuery();
                _sql = String.Format("IF OBJECT_ID('tempdb..##{0}') IS NOT NULL DROP TABLE ##{0}", temporaryTableName);
                _command.CommandText = _sql;
                _command.ExecuteNonQuery();
                _sql = String.Format("CREATE TABLE ##{0} (bck VARBINARY(MAX))", temporaryTableName);
                _command.CommandText = _sql;
                _command.ExecuteNonQuery();
                _sql = String.Format("INSERT INTO ##{0} SELECT bck.* FROM OPENROWSET(BULK '{1}\\{2}.bak',SINGLE_BLOB) bck", temporaryTableName, AremoteTempPath, _dbname);
                _command.CommandText = _sql;
                _command.ExecuteNonQuery();
                _sql = String.Format("SELECT bck FROM ##{0}", temporaryTableName);
                SqlDataAdapter da = new SqlDataAdapter(_sql, _conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataRow dr = ds.Tables[0].Rows[0];
                byte[] backupFromServer = new byte[0];
                backupFromServer = (byte[])dr["bck"];
                int aSize = new int();
                aSize = backupFromServer.GetUpperBound(0) + 1;

                FileStream fs = new FileStream(String.Format("{0}", AlocalPath), FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(backupFromServer, 0, aSize);
                fs.Close();

                _sql = String.Format("DROP TABLE ##{0}", temporaryTableName);
                _command.CommandText = _sql;
                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //s	MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}