using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotationBAL.Services
{
    public class ExceptionHandler
    {
        public static ResponseStatusModel ExceptionSave(IDictionary<string, object> CB, Exception ex)
        {
            LoginResponseStatusModel response = new LoginResponseStatusModel();
            ExceptionModel em = new ExceptionModel();
            ExceptionService es = new ExceptionService();
            ResponseStatusModel statusModel = new ResponseStatusModel();
            em.Etype = ex.GetType().ToString();
            em.Ipaddr = "";
            em.Emsg = Convert.ToString(ex.Message) + "||InnerException=" + Convert.ToString(ex.InnerException) + "||StackTrace=" + Convert.ToString(ex.StackTrace) + "||HelpLink=" + Convert.ToString(ex.HelpLink) + "||HResult=" + Convert.ToString(ex.HResult);
            if (CB.ContainsKey("action"))
            {
                em.Actionname = CB["action"].ToString();
            }
            if (CB.ContainsKey("controller"))
            {
                em.Controllername = CB["controller"].ToString();
            }
            em.Esource = "API Controller";
            es.SaveException(em);
            statusModel.StatusMessage = "An Exception Occured Please Try Agian later.";
            statusModel.n = 0;
            statusModel.Status = "Failed";
            return statusModel;
        }
    }
}