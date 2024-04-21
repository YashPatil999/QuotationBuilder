using QuotationDAL.Repositories;
using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationBAL.Services
{
    public class ProductService
    {
        ProductRepository repo = new ProductRepository();
        public ResponseStatusModel AddQuotation(List<QuotationProductTable> quotation)
        {
            ResponseStatusModel response = new ResponseStatusModel();
            ResponseStatusModel response1 = new ResponseStatusModel();
            ResponseStatusModel responseStatusModel = new ResponseStatusModel();
            QuotationTable quotationTable = new QuotationTable()
            {
                Quotation_Number = quotation[0].Quotation_Number,
                Quotation_Date = quotation[0].Quotation_Date,
                Company_Logo = quotation[0].Company_Logo,
                Company_Logo_Name = quotation[0].Company_Logo_Name,
                Company_Name = quotation[0].Company_Name,
                Company_Address = quotation[0].Company_Address,
                VersionNumber = quotation[0].VersionNumber,
                SubTotal = quotation[0].SubTotal,
                GSTAmount = quotation[0].GSTAmount,
                GrandTotal = quotation[0].GrandTotal,
                ValidTill = quotation[0].ValidTill,
            };
            List<ProductTable> productTables = new List<ProductTable>();

            foreach (var quotationProduct in quotation)
            {
                var productTable = new ProductTable
                {
                    SrNo = quotationProduct.SrNo,
                    ProductName = quotationProduct.ProductName,
                    Description = quotationProduct.Description,
                    Tenure = quotationProduct.Tenure,
                    Rate = quotationProduct.Rate,
                    Quantity = quotationProduct.Quantity,
                    Total = quotationProduct.Total,
                    Quotation_Number = quotationProduct.Quotation_Number,
                    VersionNumber = quotationProduct.VersionNumber
                };
                productTables.Add(productTable);
            }
             response= repo.AddQuotation(quotationTable);
             response1 = repo.AddQuotationProducts(productTables);

            if(response1.n == 1 && response.n == 1 ) 
            {
                responseStatusModel.n = 1;
                responseStatusModel.Status = "Success";
                responseStatusModel.StatusMessage = "Quotation Added successfully!!!";
            }
            else
            {
                responseStatusModel.n = 0;
                responseStatusModel.Status = "Failed";
                responseStatusModel.StatusMessage = "Quotation Not Added";
            }
            return response;
        }

        public List<string> ShowQuotationNumber()
        {
            return repo.ShowQuotationNumber();
        }

        public List<QuotationProductTable> ShowQuotation(string QuotationNumber)
        {
            return repo.ShowQuotation(QuotationNumber);
        }

        public ResponseStatusModel updateQuotation(List<QuotationProductTable> quotation)
        {
            ResponseStatusModel response = new ResponseStatusModel();
            ResponseStatusModel response1 = new ResponseStatusModel();
            ResponseStatusModel responseStatusModel = new ResponseStatusModel();
            QuotationTable quotationTable = new QuotationTable()
            {
                Quotation_Number = quotation[0].Quotation_Number,
                Quotation_Date = quotation[0].Quotation_Date,
                Company_Logo = quotation[0].Company_Logo,
                Company_Logo_Name = quotation[0].Company_Logo_Name,
                Company_Name = quotation[0].Company_Name,
                Company_Address = quotation[0].Company_Address,
                VersionNumber = quotation[0].VersionNumber,
                SubTotal = quotation[0].SubTotal,
                GSTAmount = quotation[0].GSTAmount,
                GrandTotal = quotation[0].GrandTotal,
                ValidTill = quotation[0].ValidTill,

            };
            List<ProductTable> productTables = new List<ProductTable>();

            foreach (var quotationProduct in quotation)
            {
                var productTable = new ProductTable
                {
                    SrNo = quotationProduct.SrNo,
                    ProductName = quotationProduct.ProductName,
                    Description = quotationProduct.Description,
                    Tenure = quotationProduct.Tenure,
                    Rate = quotationProduct.Rate,
                    Quantity = quotationProduct.Quantity,
                    Total = quotationProduct.Total,
                    Quotation_Number = quotationProduct.Quotation_Number,
                    VersionNumber = quotationProduct.VersionNumber
                };
                productTables.Add(productTable);
            }
            response = repo.updateQuotation(quotationTable);
            response1 = repo.UpdateQuotationProducts(productTables);

            if (response1.n == 1 && response.n == 1)
            {
                responseStatusModel.n = 1;
                responseStatusModel.Status = "Success";
                responseStatusModel.StatusMessage = "Quotation Updated successfully!!!";
            }
            else
            {
                responseStatusModel.n = 0;
                responseStatusModel.Status = "Failed";
                responseStatusModel.StatusMessage = "Quotation Not Updated";
            }
            return response;
        }

        public ResponseStatusModel deleteQuotation(string QuotationNumber, int VersionNumber)
        {
            return repo.deleteQuotation(QuotationNumber, VersionNumber);
        }

        public List<QuotationTable> ShowQuotationList()
        {
            return repo.ShowQuotationList();
        }

        public List<QuotationTable> ShowQuotationsAllVersions(string QuotationNumber)
        {
            return repo.ShowQuotationsAllVersions(QuotationNumber);
        }

        public List<QuotationProductTable> ShowSpecificQuotation(string QuotationNumber)
        {
            return repo.ShowSpecificQuotation(QuotationNumber);
        }

        public List<QuotationProductTable> ShowQuotationsSpecificVersions(string QuotationNumber, int VersionNumber)
        {
            return repo.ShowQuotationsSpecificVersions(QuotationNumber, VersionNumber);
        }
    }

}
