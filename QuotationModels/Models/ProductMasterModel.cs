using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationModels.Models
{
    public class ProductMasterModel
    {
        public int PId { get; set; }
        public string PName { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
    }

    public class ProductMasterModelViewModel
    {
        public List<ProductMasterModel> Models { get; set; }
        public ResponseStatusModel ResponseStatus { get; set; }
    }

    public class ProductTable
    {
        public int SrNo { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Tenure { get; set; }
        public int Rate { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public string Quotation_Number { get; set; }
        public int VersionNumber { get; set; }

    }
}
