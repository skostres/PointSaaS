﻿using Owin;
using Microsoft.Owin;
using PCMC.Data;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalrWebService.Performance;

[assembly: OwinStartup(typeof(SignalrWebService.Startup))]
namespace SignalrWebService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            // Any connection or hub wire up and configuration should go here
            app.UseCors(CorsOptions.AllowAll);
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            app.MapSignalR(hubConfiguration);


            PerformanceEngine performanceEngine = new PerformanceEngine(2000); // , GetRequiredPerformanceMonitors()
            Task.Factory.StartNew(async () => await performanceEngine.OnPerformanceMonitor());
        }
       
    }
}