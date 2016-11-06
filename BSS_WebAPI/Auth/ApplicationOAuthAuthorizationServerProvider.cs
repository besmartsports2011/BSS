using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BSS_WebAPI.BusinessLayer;
using BSS_WebAPI.Models.DTO;
using Microsoft.Owin.Security.OAuth;
using BSS_WebAPI.Models;

namespace BSS_WebAPI.Auth
{

    public class ApplicationOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Used to make client authentication
        /// </summary>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Is not used here
            context.Validated();
        }

        /// <summary>
        /// User authentication
        /// </summary>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            string clientApplicationName, clientApplicationVersion;

            if (!ReadAuthenticationContent(context.Request.Headers, "ClientApplicationName", out clientApplicationName)
                || !ReadAuthenticationContent(context.Request.Headers, "ClientApplicationVersion", out clientApplicationVersion)
                || !CustomAuthorize.AllowedClients.Contains(clientApplicationName)
                || !CustomAuthorize.AllowedVersions.Contains(clientApplicationVersion))
            {
                context.SetError("Unsupported client version");
                return;
            }

            var login = new Login { username = context.UserName, password = context.Password };
            login = new LoginManager().Verify(login);

            if (login == null || login.errors.Count > 0)
            {
                context.SetError("invalid_grant", (login != null) ?  string.Join(", ", login.errors.ToArray()) : "Unable to get login object from datamodel" );
                return;
            }

            //if (context.UserName != System.Configuration.ConfigurationManager.AppSettings["AuthUser"] 
            //    || context.Password != System.Configuration.ConfigurationManager.AppSettings["AuthPassword"])
            //{
            //    context.SetError("invalid_grant", "The username or password is incorrect.");
            //    return;
            //}

            var identity = new ClaimsIdentity(MyStartup.OAuthBearerOptions.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("ClientApplicationName", clientApplicationName));
            identity.AddClaim(new Claim(ClaimTypes.Version, clientApplicationVersion));
            //identity.AddClaim(new Claim("ClientApplicationName", "clientApplicationName"));
            //identity.AddClaim(new Claim(ClaimTypes.Version, "clientApplicationVersion"));
            //Add more claims
            identity.AddClaim(new Claim("user_role", login.role));
            identity.AddClaim(new Claim("user_id", login.userId.ToString()));
            identity.AddClaim(new Claim("user_guid", login.userGuid.ToString()));
            identity.AddClaim(new Claim("user_email", login.email));
            identity.AddClaim(new Claim("display_name", login.email));  //login.displayname
          //  identity.AddClaim(new Claim("logo_path", login.logoPath));  //login.displayname
            
            context.Validated(identity);

        }

        protected bool ReadAuthenticationContent(IEnumerable<KeyValuePair<string, string[]>> authenticationContent, string key, out string value)
        {
            value = string.Empty;
            var content = authenticationContent.FirstOrDefault(pair => pair.Key == key);
            if (content.Value == null || content.Value.Count() != 1)
                return false;
            value = content.Value.First();
            return true;
        }

        /// <summary>
        /// Is used to add additional return values to the token response message
        /// </summary>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            foreach (var claim in context.Identity.Claims)
            {
                context.AdditionalResponseParameters.Add(claim.Type, claim.Value);
            }
            

            //var userSessionClaim = context.Identity.FindFirst(claim => claim.Type == ApiConstants.Claims.UserSessionID);
            //context.AdditionalResponseParameters.Add(userSessionClaim.Type, userSessionClaim.Value);
            return Task.FromResult<object>(null);
        }
    }
}