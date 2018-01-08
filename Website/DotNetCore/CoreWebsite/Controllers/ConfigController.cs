using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.Models.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CoreWebsite.Controllers
{
    public class ConfigController : Controller
    {
        private ConfigTest _configTestByOptions;
        private readonly IConfiguration _configuration;
        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            //GetSection先读节点，必须
            //ConfigTest实现接口IOptions<ConfigTest>
            _configTestByOptions = _configuration.GetSection("MyData").Get<ConfigTest>();
            return View();
        }
    }
}