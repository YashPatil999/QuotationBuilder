using QuotationDAL.Repositories;
using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationBAL.Services
{
    public class ProductMasterService
    {
        ProductMasterRepository repo = new ProductMasterRepository();
        public ProductMasterModelViewModel PopulateData()
        {
            return repo.PopulateData();
        }
    }
}
