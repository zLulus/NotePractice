using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Mvc.Controllers
{
    //[Route("[controller]/[action]")]
    [Route("[controller]")]
    public class CustomRouteController : Controller
    {
        [Route("[action]")]
        [Route("")]//使List()成为默认路由入口
        public string List()
        {
            return "我是CustomRoute控制器的List()操作方法 ";
        }
    }
}
