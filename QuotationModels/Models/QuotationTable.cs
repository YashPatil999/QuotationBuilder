using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationModels.Models
{
    public class QuotationProductTable
    {
        public int SrNo { get; set; }
        public string Quotation_Number { get; set; }
        public string Quotation_Date { get; set; }
        public string Company_Logo { get; set; }
        public string Company_Logo_Name { get; set; }
        public string Company_Name { get; set; }
        public string Company_Address {  get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Tenure { get; set; }
        public int Rate { get; set; }
        public int Quantity { get; set; }
        public int Total {  get; set; }
        public float SubTotal { get; set; }
        public float GSTAmount { get; set; }
        public float GrandTotal { get; set; }
        public int VersionNumber { get; set; }
        public int ValidTill {  get; set; }
    }

   /* public class CompanyLogo
    {
        public string Company_Logo { get; set; }
        public string Company_Logo_Name { get; set; }
    }*/

    public class QuotationTable
    {
        public int SrNo { get; set; }
        public string Quotation_Number { get; set; }
        public string Quotation_Date { get; set; }
        public string Company_Logo { get; set; }
        public string Company_Logo_Name { get; set; }
        public string Company_Name { get; set; }
        public string Company_Address { get; set; }
        public int VersionNumber { get; set; }
        public float SubTotal { get; set; }
        public float GSTAmount { get; set; }
        public float GrandTotal { get; set; }
        public int ValidTill {  get; set; }
    }

}
