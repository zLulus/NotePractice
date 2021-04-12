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
    }
}
