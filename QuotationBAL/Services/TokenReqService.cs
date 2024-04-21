using Newtonsoft.Json;
using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace QuotationBAL.Services
{
    public class TokenReqService
    {
        public static TokenResponse GetToken(UserMasterModel user)
        {
            TokenResponse TokenResp = new TokenResponse();
            string tokenurl = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/token";
            var formcont = new Dictionary<string, string>
            {
                { "UserName", user.Email },
                { "password", user.EncryptedPassword},
                { "grant_type", "password" }
            };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var content = new FormUrlEncodedContent(formcont);
            var httpResponse = client.PostAsync(tokenurl, content).Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var result = httpResponse.Content.ReadAsStringAsync().Result.ToString();
                TokenResp = JsonConvert.DeserializeObject<TokenResponse>(result);
            }
            return TokenResp;
        }
    }
}