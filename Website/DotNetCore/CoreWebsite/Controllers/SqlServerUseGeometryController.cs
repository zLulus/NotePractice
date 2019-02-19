using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Controllers
{
    public class SqlServerUseGeometryController : Controller
    {
        //参考资料
        //https://docs.microsoft.com/zh-cn/ef/core/modeling/spatial
        //https://docs.microsoft.com/zh-cn/sql/t-sql/spatial-geometry/spatial-types-geometry-transact-sql?view=sql-server-2017
        public IActionResult Index()
        {
            return View();
        }
    }
}