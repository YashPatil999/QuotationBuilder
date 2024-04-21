using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotationBAL.Services
{
    public class QuotationNumberGenerator
    {
        public String QuotationGenerator()
        {
            StringBuilder quotationNumberBuilder = new StringBuilder();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                int index = random.Next(chars.Length);
                char randomChar = chars[index];
                quotationNumberBuilder.Append(randomChar);
            }
            return quotationNumberBuilder.ToString();
        }
    }
}
