using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Optimization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PCMC.Data;
using Microsoft.EntityFrameworkCore;

namespace PCMC
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public IConfigurationRoot Config { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<CompDBContext>(options =>
                options.UseSqlServer(Config.GetConnectionString("DefaultConnection")));
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
            string err = "Error caught in Application_Error event" +
                    "\n \nError Message: " + objErr.Message.ToString() +
                    "\n \nStack Trace: " + objErr.StackTrace.ToString();

            System.Diagnostics.EventLog.WriteEntry("MYApplication", err, System.Diagnostics.EventLogEntryType.Error);
            Server.ClearError();
        }
    }
}