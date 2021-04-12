using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet5Website.Controllers.HttpGetArrayRequestTests
{
    public class ArrayRequest
    {
        public IEnumerable<long> LongList { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class HttpGetArrayRequestTestController : ControllerBase
    {
        //get传送数组
        //https://forums.asp.net/t/2145420.aspx?How+to+pass+array+of+strings+as+an+input+parameter+with+HttpGet+request+
        //http://localhost:5000/HttpGetArrayRequestTest?request.LongList=123&request.LongList=321&request.LongList=555&request.LongList=332
        [HttpGet]
        public IEnumerable<long> Get([FromQuery] ArrayRequest request)
        {
            return request.LongList;
        }
    }
}
