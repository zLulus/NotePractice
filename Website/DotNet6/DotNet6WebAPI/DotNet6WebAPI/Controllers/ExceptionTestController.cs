using DotNet6WebAPI.Common;
using Microsoft.AspNetCore.Mvc;

namespace DotNet6WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExceptionTestController : ControllerBase
    {
        [HttpGet(nameof(Return404))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Return404()
        {
            return new NotFoundResult();
        }

        [HttpGet(nameof(ThrowCustomException))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult ThrowCustomException()
        {
            throw new CustomException("test throw exception");
        }

        [HttpGet(nameof(Return403))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Return403()
        {
            return new ForbidResult("test");
        }
    }
}
