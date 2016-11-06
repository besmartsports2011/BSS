using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BSS_WebAPI;
using BSS_WebAPI.Auth;
using BSS_WebAPI.Models;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(MyStartup))]

namespace BSS_WebAPI
{
    public class MyStartup
    {
        public void Configuration(IAppBuilder app)
        {
            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureAuth(app);

        }

        public static OAuthAuthorizationServerOptions OAuthBearerOptions { get; private set; }

        private void ConfigureAuth(IAppBuilder app)
        {
            //app.CreatePerOwinContext(new BeSmartSportsEntities());
            // Configure the application for OAuth bearer based flow
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                //Path of the authorization server
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthAuthorizationServerProvider(),
                //Life time of the access token
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(5),
                //HTTPS is allowed only
                //AllowInsecureHttp = false,
                ApplicationCanDisplayErrors = true,
            };

//#if DEBUG
            oAuthOptions.AllowInsecureHttp = true; //Insecure HTTP is allowed conditionally
//#endif
            OAuthBearerOptions = oAuthOptions;
            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(oAuthOptions);
        }
    }
}
