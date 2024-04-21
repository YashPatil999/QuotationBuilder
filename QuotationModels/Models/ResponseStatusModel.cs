using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationModels.Models
{
    public class ResponseStatusModel
    {
        public int n { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
    }
}
