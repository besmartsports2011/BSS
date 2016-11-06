using BSS_WebAPI.EntityAutoMapper;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BSS_WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Added
            AutoMapperConfiguration.Configure();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //' Fires at the beginning of each request - flush for angular cors call for generating token
            if (Request.HttpMethod == "OPTIONS")
            {
                Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, ClientApplicationVersion, ClientApplicationName");
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                Context.ApplicationInstance.CompleteRequest();

                Response.Flush();
            }

            //  HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //Response.AppendHeader("Access-Control-Allow-Origin", "*");
            //if (Context.Request.HttpMethod.Equals("OPTIONS"))
            //{
            //    Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            //    Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
            //    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            //    Context.ApplicationInstance.CompleteRequest();
            //}
        }
    }
}
