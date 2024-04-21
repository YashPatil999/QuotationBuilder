using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotationBAL.Services
{
    public class ExpirationTimeFormat
    {
        public String TimeFormat(TokenResponse token) 
        {
            int seconds = Convert.ToInt32(token.expires_in);
            DateTime referencePoint = new DateTime(1970, 1, 1);

            // Add the seconds to the reference point
            DateTime result = referencePoint.AddSeconds(seconds);

            // Get the current date and time
            DateTime currentTime = DateTime.Now;

            // Add the time from the result to the current time
            DateTime combinedDateTime = currentTime.Add(new TimeSpan(result.Hour, result.Minute, result.Second));

            // Format the combined date and time as "yyyy-MM-dd HH:mm:ss"
            string formattedResult = combinedDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            return formattedResult;
        }
    }
}