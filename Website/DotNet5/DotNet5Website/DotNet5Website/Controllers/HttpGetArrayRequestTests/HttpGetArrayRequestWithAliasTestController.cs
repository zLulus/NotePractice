using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet5Website.Controllers.HttpGetArrayRequestTests
{
    [ApiController]
    [Route("[controller]")]
    public class HttpGetArrayRequestWithAliasTestController : ControllerBase
    {
        //http://localhost:5000/HttpGetArrayRequestWithAliasTest?aaa.LongList=332&aaa.LongList=123&aaa.LongList=456
        [HttpGet]
        public IEnumerable<long> Get([FromQuery(Name = "aaa")] ArrayRequest request)
        {
            return request.LongList;
        }
    }
}
