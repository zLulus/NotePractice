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
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            //多个实体，不能重名
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Supplier>("Suppliers");
            //定义自定义方法
            builder.Function("GetSalesTaxRate")
                    .Returns<double>()
                    .Parameter<int>("PostalCode");
            configuration.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());
        }
    }
}