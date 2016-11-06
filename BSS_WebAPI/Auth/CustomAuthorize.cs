using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BSS_WebAPI.Auth
{

    public class CustomAuthorize : AuthorizeAttribute
    {
        public static readonly string[] AllowedClients = { "GmWebClient", "GmAndroidClient" };
        public static readonly string[] AllowedVersions = { "1.0.0" };

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string client, version;
            if (!ReadAuthenticationContent(actionContext.Request.Headers, "ClientApplicationName", out client)
                || !ReadAuthenticationContent(actionContext.Request.Headers, "ClientApplicationVersion", out version)
                || !AllowedVersions.Contains(version)
                || !AllowedClients.Contains(client))
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = "Unsupported client version"
                };
            else
            {
                base.OnAuthorization(actionContext);
            }
        }

        protected bool ReadAuthenticationContent(HttpRequestHeaders authenticationContent, string key, out string value)
        {
            value = string.Empty;
            var content = authenticationContent.FirstOrDefault(pair => pair.Key == key);
            if (content.Value == null || content.Value.Count() != 1)
                return false;
            value = content.Value.First();
            return true;
        }
    }
}