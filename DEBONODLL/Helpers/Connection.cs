using System;
using System.Collections.Generic;

using System.Text;
using System.Data.Sql;
using System.Data;

namespace DebonoDLL.App_Code.Helpers
{
    class Connection
    {
        public DataTable GetServerList()
        {
            SqlDataSourceEnumerator servers = SqlDataSourceEnumerator.Instance;
            return servers.GetDataSources();
           
        }
    }
}
