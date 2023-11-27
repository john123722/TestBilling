using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FormPractise
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            

            routes.MapRoute(
                name: "DisplayIndividualReport",
                url: "PatientAddTest/DisplayIndividualReport/{id}",
                defaults: new { controller = "PatientAddTest", action = "DisplayIndividualReport", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
           name: "Details",
           url: "Home/Details/{id}",
           defaults: new { controller = "Home", action = "Details", id = UrlParameter.Optional }
           );

            routes.MapRoute(
            name: "DisplayTest",
            url: "PatientAddTest/DisplayTest",
            defaults: new { controller = "PatientAddTest", action = "DisplayTest" }
            );
            routes.MapRoute(
            name: "PatientDetails",
            url: "PatientTest/Details/{id}",
            defaults: new { controller = "PatientTest", action = "Details", id = UrlParameter.Optional }
            );
           
            routes.MapRoute(
            name: "Display",
            url: "PatientAddTest/Display/{id}",
            defaults: new { controller = "PatientAddTest", action = "Display", id = UrlParameter.Optional }
            );
            
        }
    }
}
