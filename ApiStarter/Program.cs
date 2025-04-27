﻿using Microsoft.Owin.Hosting;
using Owin;
using System;
using Swashbuckle.Application;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using ApiStarter;
using System.Net.Http;

namespace ApiStarter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "https://localhost:44364";

            // Start OWIN host 
            using (WebApp.Start(url: baseAddress))
            {
                var prs = new ProcessStartInfo("chrome.exe");
                prs.Arguments = "https://localhost:44364";
                //Process.Start(prs);
                Console.WriteLine("Service Listening at " + baseAddress);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                //System.Threading.Thread.Sleep(-1);
            }
        }
    }


    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            var cors = new EnableCorsAttribute("http://localhost:3000", "*", "*");
            config.EnableCors(cors);
            config
    .EnableSwagger(c =>
    {
        c.SingleApiVersion("v1", "Backend");
        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    })
    .EnableSwaggerUi();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "swagger",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler((url => url.RequestUri.ToString()), "swagger")
            );

            appBuilder.UseWebApi(config);

        }
    }
}
