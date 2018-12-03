using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using CoreWebsite.Castle.Windsor.Demo.Classes;
using CoreWebsite.Castle.Windsor.Demo.Interfaces;
using CoreWebsite.Castle.Windsor.Demo.Tests;

namespace CoreWebsite.Controllers
{
    public class WindsorContainerTestController : Controller
    {
        private readonly IMyDependency _myDependency;
        public WindsorContainerTestController(IMyDependency myDependency)
        {
            _myDependency = myDependency;
        }

        public async Task<ActionResult> TestMyDependency()
        {
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1
            var r = await _myDependency.WriteMessage(
                "WindsorContainerTestController.TestMyDependency created this message.");
            return Json(r);
        }

        public IActionResult DoSomething()
        {
            //https://github.com/castleproject/Windsor/blob/master/docs/basic-tutorial.md

            //创建一个WINDSOR容器对象并注册接口及其具体实现
            var container = new WindsorContainer();
            container.Register(Component.For<WindsorContainerTest1>());
            container.Register(Component.For<IDependency1>().ImplementedBy<Dependency1>());
            container.Register(Component.For<IDependency2>().ImplementedBy<Dependency2>());

            //创建主要对象并根据需要调用其方法
            var mainThing = container.Resolve<WindsorContainerTest1>();
            string r= mainThing.DoSomething();
            return Json(r);
        }
    }
}