using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TimingService()
            };
            ServiceBase.Run(ServicesToRun);

            //test service
            //右键项目属性，将项目改为控制台程序
            //TimingService timingService = new TimingService();
            //timingService.TestStartupAndStop(args);
        }
    }
}
