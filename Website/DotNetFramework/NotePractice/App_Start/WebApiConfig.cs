using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Model.ODataStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NotePractice.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            //ODataModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Product>("Products");
            //configuration.MapODataServiceRoute(
            //    routeName: "ODataStudy",
            //    routePrefix: null,
            //    model: builder.GetEdmModel());
        }
    }
}