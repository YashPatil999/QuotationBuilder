using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Collections;
using System.Xml;
using Newtonsoft.Json;

namespace QuotationDAL.Repositories
{
    public class ProductRepository
    {
        public ResponseStatusModel AddQuotation(QuotationTable quote)
        {
            ResponseStatusModel responseStatus = new ResponseStatusModel();
            string storepro = "AddNewQuotation";

            using (IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString))
            {
                    var input = conn.QueryMultiple(storepro, new
                    {
                        Quotation_Number = quote.Quotation_Number,
                        Quotation_Date = quote.Quotation_Date,
                        Company_Logo = quote.Company_Logo,
                        Company_Logo_Name = quote.Company_Logo_Name,
                        Company_Name = quote.Company_Name,
                        Company_Address = quote.Company_Address,
                        SubTotal = quote.SubTotal,
                        GSTAmount = quote.GSTAmount,
                        GrandTotal = quote.GrandTotal,
                        ValidTill = quote.ValidTill,
                    },commandType: CommandType.StoredProcedure);
                responseStatus= input.Read<ResponseStatusModel>().SingleOrDefault();
            }
            return responseStatus;
        }

        public ResponseStatusModel AddQuotationProducts(List<ProductTable> products)
        {
            ResponseStatusModel responseStatus = new ResponseStatusModel();
            string storepro = "AddQuotationProducts";
            using (IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString))
            {
                foreach (var prod in products)
                {
                    var parameters = new
                    {
                        Quotation_Number = prod.Quotation_Number,
                        ProductName = prod.ProductName,
                        Description = prod.Description,
                        Tenure = prod.Tenure,
                        Rate = prod.Rate,
                        Quantity = prod.Quantity,
                        Total = prod.Total,
                    };

                    var result = conn.Query<ResponseStatusModel>(storepro, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();

                    if (result != null)
                    {
                        responseStatus = result;
                    }
                }
            }
            return responseStatus;
        }

        public List<string> ShowQuotationNumber()
        {
            List<string> qnumber = new List<string>();
            string storepro = "ShowQuotationNumbers";
            IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString);
            qnumber = conn.Query<string>(storepro, new { }, commandType: CommandType.StoredProcedure).ToList();
            return qnumber;
        }

        public List<QuotationProductTable> ShowQuotation(string QuotationNumber)
        {
            List<QuotationProductTable> quotation = new List<QuotationProductTable>();
            string storepro = "ShowQuotationByID";
            IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString);
            quotation = conn.Query<QuotationProductTable>(storepro, new { Quotation_Number = QuotationNumber }, commandType: CommandType.StoredProcedure).ToList();
            return quotation;
        }

        public ResponseStatusModel updateQuotation(QuotationTable quote)
        {
            ResponseStatusModel responseStatus = new ResponseStatusModel();
            string storepro = "UpdateQuotation";
            using (IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString))
            {
                var input = conn.QueryMultiple(storepro, new
                {
                    Quotation_Number = quote.Quotation_Number,
                    Quotation_Date = quote.Quotation_Date,
                    Company_Logo = quote.Company_Logo,
                    Company_Logo_Name = quote.Company_Logo_Name,
                    Company_Name = quote.Company_Name,
                    Company_Address = quote.Company_Address,
                    SubTotal = quote.SubTotal,
                    GSTAmount = quote.GSTAmount,
                    GrandTotal = quote.GrandTotal,
                    VersionNumber = quote.VersionNumber,
                    ValidTill = quote.ValidTill,

                }, commandType: CommandType.StoredProcedure);
                responseStatus = input.Read<ResponseStatusModel>().SingleOrDefault();
            }
            return responseStatus;
        }

        public ResponseStatusModel UpdateQuotationProducts(List<ProductTable> products)
        {
            ResponseStatusModel responseStatus = new ResponseStatusModel();
            string storepro = "UpdateQuotationProducts";
            using (IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString))
            {
                foreach (var prod in products)
                {
                    var parameters = new
                    {
                        Quotation_Number = prod.Quotation_Number,
                        ProductName = prod.ProductName,
                        Description = prod.Description,
                        Tenure = prod.Tenure,
                        Rate = prod.Rate,
                        Quantity = prod.Quantity,
                        Total = prod.Total,
                        VersionNumber = prod.VersionNumber
                    };

                    var result = conn.Query<ResponseStatusModel>(storepro, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();

                    if (result != null)
                    {
                        responseStatus = result;
                    }
                }
            }
            return responseStatus;
        }

        public ResponseStatusModel deleteQuotation(string QuotationNumber, int VersionNumber)
        {
            ResponseStatusModel statusModel = new ResponseStatusModel();
            string storepro = "DeleteQuotation";
            using (IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString))
            {
                var input = conn.QueryMultiple(storepro, new
                {
                    Quotation_Number = QuotationNumber,
                    VersionNumber = VersionNumber,

                }, commandType: CommandType.StoredProcedure);
                statusModel = input.Read<ResponseStatusModel>().SingleOrDefault();
            }
            return statusModel;
        }

        public List<QuotationTable> ShowQuotationList()
        {
            List<QuotationTable> quotation = new List<QuotationTable>();
            string storepro = "ShowQuotations";
            IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString);
            quotation = conn.Query<QuotationTable>(storepro, new { }, commandType: CommandType.StoredProcedure).ToList();
            return quotation;
        }

        public List<QuotationTable> ShowQuotationsAllVersions(string QuotationNumber)
        {
            List<QuotationTable> quotation = new List<QuotationTable>();
            string storepro = "ShowQuotationsAllVersions";
            IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString);
            quotation = conn.Query<QuotationTable>(storepro, new { Quotation_Number = QuotationNumber }, commandType: CommandType.StoredProcedure).ToList();
            return quotation;
        }

        public List<QuotationProductTable> ShowSpecificQuotation(string QuotationNumber)
        {
            List<QuotationProductTable> quotation = new List<QuotationProductTable>();
            string storepro = "ShowSpecificQuotation";
            IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString);
            quotation = conn.Query<QuotationProductTable>(storepro, new { Quotation_Number = QuotationNumber }, commandType: CommandType.StoredProcedure).ToList();
            return quotation;
        }
        public List<QuotationProductTable> ShowQuotationsSpecificVersions(string QuotationNumber, int VersionNumber)
        {
            List<QuotationProductTable> quotation = new List<QuotationProductTable>();
            string storepro = "ShowQuotationsSpecificVersions";
            IDbConnection conn = new SqlConnection(DataConnection.GetConnection().ConnectionString);
            quotation = conn.Query<QuotationProductTable>(storepro, new { Quotation_Number = QuotationNumber, VersionNumber = VersionNumber }, commandType: CommandType.StoredProcedure).ToList();
            return quotation;
        }
    }
}
