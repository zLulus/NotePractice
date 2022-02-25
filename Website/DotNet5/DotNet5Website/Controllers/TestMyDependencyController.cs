using DotNet5Website.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet5Website.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestMyDependencyController : ControllerBase
    {
        private readonly IMyDependency _myDependency;
        public TestMyDependencyController(IMyDependency myDependency)
        {
            _myDependency = myDependency;
        }

        [HttpGet]
        public string Get()
        {
            return _myDependency.WriteMessage("msg");
        }
    }
}
