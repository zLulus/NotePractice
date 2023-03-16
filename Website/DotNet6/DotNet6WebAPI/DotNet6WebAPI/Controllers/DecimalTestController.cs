using DotNet6WebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DotNet6WebAPI.Controllers
{
    public class DecimalTestController : ControllerBase
    {
        [HttpGet(nameof(GetNormalNumber))]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<GetNormalNumberDto> GetNormalNumber()
        {
            //return 12
            return Ok(new GetNormalNumberDto() { Data = 12 });
        }

        [HttpGet(nameof(GetNumberWith2Digit))]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<GetNumberWith2Digit> GetNumberWith2Digit()
        {
            //return "12.00"
            return Ok(new GetNumberWith2Digit() { Data = 12 });
        }
    }
}
