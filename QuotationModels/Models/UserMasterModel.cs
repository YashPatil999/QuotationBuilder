using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuotationModels.Models
{
    public class UserMasterModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string EncryptedPassword { get; set;}
        public int Active { get; set; }
        public int Status { get; set; }
        public string EncryptedKey { get; set; }
        public string IV { get; set; }
        public string Token { get; set; }
        public DateTime TKExpireTime { get; set; }

    }

   /* public class UserMasterModelViewModel
    {
        public List<UserMasterModel> Master { get; set; }
        public LoginResponseStatusModel response { get; set;}
        public UserMasterModel masterModel { get; set; }
    }*/

    public class UserLoginModel
    {
        public LoginResponseStatusModel response { get; set; }
        public UserMasterModel masterModel { get; set; }
    }

    public class LoginModelViewModel
    {
        public UserMasterModel userMasterModel { get; set; }
        public string Token { get; set; }
        public LoginResponseStatusModel response { get; set; }

    }

    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }

    public class UserMasterModelViewModel
    {
        public List<UserMasterModel> Master { get; set; }
        public LoginResponseStatusModel response { get; set; }
        public UserMasterModel masterModel { get; set; }
        public TokenResponse Token { get; set; }
        public List<TokenResponse> TokenResponses { get; set; }
    }
}
