using Microsoft.Owin;
using NotePractice.App_Start;
using NotePractice.SignalRConnections;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace NotePractice.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR<AppConnection>("/appConnection");
        }
    }
}