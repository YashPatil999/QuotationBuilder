using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace QuotationBAL.Security
{
    public class ValidateToken
    {
        public bool ValidateUserCredentials(string userName, string password)
        {
            var date = DateTime.Now;
            var user = EncryptDecrypt.Decrypt(userName).Split('|');
            var pass = EncryptDecrypt.Decrypt(password).Split('|');

            var userTimeStamp = (date - Convert.ToDateTime(user[1])).TotalSeconds;
            var passTimeStamp = (date - Convert.ToDateTime(pass[1])).TotalSeconds;
            var credExprInSec = Convert.ToInt32(ConfigurationManager.AppSettings["CredentialExpirationInSeconds"]);

            if (user.Length <= 1)
            {
                return false;
            }
            else if (Math.Abs(userTimeStamp) > credExprInSec)
            {
                return false;
            }
            else if (pass.Length <= 1)
            {
                return false;
            }
            else if (Math.Abs(passTimeStamp) > credExprInSec)
            {
                return false;
            }

            return true;
        }
    }
}