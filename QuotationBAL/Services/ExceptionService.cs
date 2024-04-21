using QuotationDAL.Repositories;
using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationBAL.Services
{
    public class ExceptionService
    {
        ExceptionRepository er = new ExceptionRepository();
        public void SaveException(ExceptionModel em)
        {
            er.SaveException(em);
        }
    }
}
