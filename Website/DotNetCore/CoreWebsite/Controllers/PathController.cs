using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Controllers
{
    public class PathController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public PathController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult GetContentRootPath()
        {
            return Json(_hostingEnvironment.ContentRootPath);
        }

        public IActionResult GetWebRootPath()
        {
            return Json(_hostingEnvironment.WebRootPath);
        }
    }
}