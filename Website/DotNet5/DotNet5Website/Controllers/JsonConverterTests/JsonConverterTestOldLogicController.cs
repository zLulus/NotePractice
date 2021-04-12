using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet5Website.Controllers.JsonConverterTests
{
    public class GetLongListOldLogicRequest
    {
        public IEnumerable<string> LongList { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class JsonConverterTestOldLogicController : ControllerBase
    {
        [HttpPost]
        public IEnumerable<long> Post(GetLongListOldLogicRequest request)
        {
            List<long> result = new List<long>();
            foreach (var str in request.LongList)
            {
                long l = 0;
                bool isSuccess = long.TryParse(str, out l);
                if (isSuccess)
                {
                    result.Add(l);
                }
            }
            return result;
        }
    }
}
