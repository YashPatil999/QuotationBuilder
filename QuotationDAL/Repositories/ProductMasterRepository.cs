using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace QuotationDAL.Repositories
{
    public class ProductMasterRepository
    {
        public ProductMasterModelViewModel PopulateData()
        {
            ProductMasterModelViewModel productMasterModel = new ProductMasterModelViewModel();
            string storepro = "ShowProductList";
            IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString);
            productMasterModel.Models = conn.Query<ProductMasterModel>(storepro, new { }, commandType: CommandType.StoredProcedure).ToList();
            //productMasterModel.ResponseStatus = conn.Query<ResponseStatusModel>(storepro, new { }, commandType: CommandType.StoredProcedure).SingleOrDefault();
            return productMasterModel;
        }
    }
}
