using QuotationBAL.Services;
using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Web.Http;

namespace QuotationAPI.Controllers
{
    public class ProductMasterController : ApiController
    {
        ProductMasterService ser = new ProductMasterService();

        [Route("PopulateData")]
        [HttpGet]
        public ProductMasterModelViewModel PopulateData()
        {
            ProductMasterModelViewModel rsm = new ProductMasterModelViewModel();
            try
            {
                rsm = ser.PopulateData();
            }
            catch (Exception ex)
            {
                rsm.ResponseStatus.n = 0;
                rsm.ResponseStatus.Status = "Exception Occurs";
                rsm.ResponseStatus.StatusMessage = "Exception : " + ex.Message;
            }
            return rsm;
        }
    }
}

