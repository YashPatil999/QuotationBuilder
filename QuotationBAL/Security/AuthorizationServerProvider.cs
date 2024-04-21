using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace QuotationBAL.Security
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (!string.IsNullOrEmpty(context.UserName) && !string.IsNullOrEmpty(context.Password))
            {
                var validateToken = new ValidateToken();
                if (validateToken.ValidateUserCredentials(context.UserName, context.Password))
                {
                    await Task.FromResult(context.Validated(identity));
                }
                else
                {
                    context.SetError("invalid_credentials", "User Credentials are expired or invalid");
                }
            }
            else
            {
                context.SetError("invalid_grant", "Provided Username and Password is incorrect");
            }
        }
    }
}