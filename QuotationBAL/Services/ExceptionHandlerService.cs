using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationBAL.Services
{
    public class ExceptionHandlerService
    {
        public static ResponseStatusModel ExceptionSave(IDictionary<string, object> CB, Exception ex)
        {
            ResponseStatusModel response = new ResponseStatusModel();
            ExceptionModel em = new ExceptionModel();
            ExceptionService es = new ExceptionService();
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
            em.Esource = "Controller";
            es.SaveException(em);
            response.StatusMessage = "An Exception Occured Please Try Agian later.";
            response.n = 0;
            response.Status = "Failed";
            return response;
        }
    }
}
