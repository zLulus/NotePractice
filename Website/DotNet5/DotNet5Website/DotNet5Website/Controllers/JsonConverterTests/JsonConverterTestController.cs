using DotNet5Website.Converters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet5Website.Controllers.JsonConverterTests
{
    public class GetLongListRequest
    {
        [System.Text.Json.Serialization.JsonConverter(typeof(ObjectIdEnumerableConverter))]
        public IEnumerable<long> LongList { get; set; }
    }

    [ApiController]
    //路由：JsonConverterTest
    [Route("[controller]")]
    public class JsonConverterTestController: ControllerBase
    {
        [HttpPost]
        public IEnumerable<long> Post(GetLongListRequest request)
        {
            return request.LongList;
        }

        //get传送数组
        //https://forums.asp.net/t/2145420.aspx?How+to+pass+array+of+strings+as+an+input+parameter+with+HttpGet+request+
        //http://localhost:5000/JsonConverterTest?request.LongList=332
        [HttpGet]
        public IEnumerable<long> Get([FromQuery]GetLongListRequest request)
        {
            return request.LongList;
        }
        //public IEnumerable<long> Get([FromQuery(Name = "aaa")]GetLongListRequest request)
        //http://localhost:5000/JsonConverterTest?aaa.LongList=332

        
    }
}
