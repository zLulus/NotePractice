using System.Web;
using System.Web.Mvc;

namespace ArcGIS3D.JavaScriptDemo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
