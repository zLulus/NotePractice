using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}