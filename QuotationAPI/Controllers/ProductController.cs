using Newtonsoft.Json.Linq;
using QuotationBAL.Services;
using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace QuotationAPI.Controllers
{
    public class ProductController : ApiController
    {
        ProductService ser = new ProductService();
        QuotationNumberGenerator q = new QuotationNumberGenerator();
        AesGcmExample example = new AesGcmExample();

        [Route("AddQuotation")]
        [HttpPost]
        public HttpResponseMessage AddQuotation(List<QuotationProductTable> quotation)
        {
            ResponseStatusModel rsm = new ResponseStatusModel();
            try
            {
                rsm = ser.AddQuotation(quotation);
            }
            catch (Exception ex)
            {
                rsm.n = 0;
                rsm.Status = "Exception Occurs";
                rsm.StatusMessage = "Exception : " + ex.Message;
            }
            return Request.CreateResponse(HttpStatusCode.OK, rsm);
        }

        [Route("QuotationNumberGenerator")]
        [HttpGet]
        public String QuotationNumberGenerator()
        {
            List<string> QuotationNumberList = new List<string>();
            string generatedNumber = "";
            try
            {
                QuotationNumberList = ser.ShowQuotationNumber();
                generatedNumber = q.QuotationGenerator();
                int exist = 1;
                while( exist > 0)
                {
                    bool isStringInList = QuotationNumberList.Any(s => s == generatedNumber);
                    if (isStringInList)
                    {
                        generatedNumber = q.QuotationGenerator();
                        exist = 1;
                    }
                    else
                    {
                        exist = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                /*Dictionary<string, object> values = new Dictionary<string, object>()
                {
                    {"Action","GetCommercialBuyerList"},{"Controller","CommercialBuyerController"}
                };
                b = ExceptionHandlerService.ExceptionSave(values, ex);*/
            }
            return generatedNumber;
        }

        [Route("ShowQuotation")]
        [HttpGet]
        public HttpResponseMessage ShowQuotation(string QuotationNumber)
        {
            List<QuotationProductTable> Quotation = new List<QuotationProductTable>();
            try
            {
                Quotation = ser.ShowQuotation(QuotationNumber);
            }
            catch (Exception ex)
            {
                /*Dictionary<string, object> values = new Dictionary<string, object>()
                {
                    { "Action","GetCommercialBuyerList"},{ "Controller","CommercialBuyerController"}
                };
                b = ExceptionHandlerService.ExceptionSave(values, ex); */
            }
            return Request.CreateResponse(HttpStatusCode.OK, Quotation);
        }

        [Route("updateQuotation")]
        [HttpPost]
        public HttpResponseMessage updateQuotation(List<QuotationProductTable> quotation)
        {
            ResponseStatusModel responseStatus = new ResponseStatusModel();
            try
            {
                responseStatus = ser.updateQuotation(quotation);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> values = new Dictionary<string, object>()
                {
                    {"Action","GetCommercialBuyerList"},{"Controller","CommercialBuyerController"}
                };
                responseStatus = ExceptionHandlerService.ExceptionSave(values, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, responseStatus);
        }

        [Route("deleteQuotation")]
        [HttpGet]
        public HttpResponseMessage deleteQuotation(string QuotationNumber, int VersionNumber)
        {
            ResponseStatusModel statusModel = new ResponseStatusModel();
            try
            {
                statusModel = ser.deleteQuotation(QuotationNumber, VersionNumber);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> values = new Dictionary<string, object>()
                        {
                            {"Action","GetCommercialBuyerList"},{"Controller","CommercialBuyerController"}
                        };
                statusModel = ExceptionHandlerService.ExceptionSave(values, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, statusModel);
        }

        [Route("ShowQuotationList")]
        [HttpGet]
        public HttpResponseMessage ShowQuotationList()
        {
            List<QuotationTable> quotations = new List<QuotationTable>();
            try
            {
                quotations = ser.ShowQuotationList();
            }
            catch (Exception ex)
            {
                Dictionary<string, object> values = new Dictionary<string, object>()
                        {
                            {"Action","Table"},{"Controller","Book"}
                        };
            }
            return Request.CreateResponse(HttpStatusCode.OK, quotations);
        }

        [Route("ShowQuotationsAllVersions")]
        [HttpGet]
        public HttpResponseMessage ShowQuotationsAllVersions(string QuotationNumber)
        {
            List<QuotationTable> quotations = new List<QuotationTable>();
            try
            {
                quotations = ser.ShowQuotationsAllVersions(QuotationNumber);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> values = new Dictionary<string, object>()
                        {
                            {"Action","Table"},{"Controller","Book"}
                        };
            }
            return Request.CreateResponse(HttpStatusCode.OK, quotations);
        }

        [Route("ShowSpecificQuotation")]
        [HttpGet]
        public HttpResponseMessage ShowSpecificQuotation(string QuotationNumber)
        {
            List<QuotationProductTable> quotations = new List<QuotationProductTable>();
            try
            {
                quotations = ser.ShowSpecificQuotation(QuotationNumber);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> values = new Dictionary<string, object>()
                        {
                            {"Action","Table"},{"Controller","Book"}
                        };
            }
            return Request.CreateResponse(HttpStatusCode.OK, quotations);
        }

        [Route("ShowQuotationsSpecificVersions")]
        [HttpGet]
        public HttpResponseMessage ShowQuotationsSpecificVersions(string QuotationNumber, int VersionNumber)
        {
            List<QuotationProductTable> quotations = new List<QuotationProductTable>();
            try
            {
                quotations = ser.ShowQuotationsSpecificVersions(QuotationNumber,VersionNumber);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> values = new Dictionary<string, object>()
                        {
                            {"Action","Table"},{"Controller","Book"}
                        };
            }
            return Request.CreateResponse(HttpStatusCode.OK, quotations);
        }
    }
}
