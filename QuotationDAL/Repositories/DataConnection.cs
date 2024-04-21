using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace QuotationDAL.Repositories
{
    public class DataConnection
    {
        public static IDbConnection GetConnection()
        {
            IDbConnection dbConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            return dbConnection;
        }
    }
}
