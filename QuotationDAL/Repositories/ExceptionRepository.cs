using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using QuotationModels.Models;

namespace QuotationDAL.Repositories
{
    public class ExceptionRepository
    {
        public void SaveException(ExceptionModel em)
        {
            ResponseStatusModel response = new ResponseStatusModel();

            using (IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString))
            {
                string sql = "[SaveExceptions]";
                response = conn.Query<ResponseStatusModel>(sql, new
                {
                    UserId = em.Userid,
                    ExceptionType = em.Etype,
                    ExceptionSource = em.Esource,
                    ExceptionMgs = em.Emsg,
                    ExceptionUrl = em.Eurl,
                    ActionName = em.Actionname,
                    IpAddress = em.Ipaddress,
                    ControllerName = em.Controllername
                }, null, true, null, CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
