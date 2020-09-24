using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;

namespace CoreWebsite.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UseJsController : Controller
    {
        private readonly INodeServices _services;

        public UseJsController(INodeServices services)
        {
            _services = services;
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            string greetingMessage = await _services.InvokeAsync<string>("./wwwroot/scripts/greeter", "zLulus");
            return greetingMessage;
        }
    }
}