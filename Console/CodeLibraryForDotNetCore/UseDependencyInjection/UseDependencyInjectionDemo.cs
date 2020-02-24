using CodeLibraryForDotNetCore.UseDependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseDependencyInjection
{
    public class UseDependencyInjectionDemo
    {
        public void Run(string context)
        {
            var serviceProvider = new ServiceCollection()
                                    //单例模式，只有一个实例
                                    .AddSingleton<ILogService, ConsoleLogService>()
                                    //每次请求都是同一个实例，比如EntityFramework.Context
                                    //.AddScoped<ILogService, ConsoleLogService>()
                                    //每次调用都是不同的实例
                                    //.AddTransient<ILogService, ConsoleLogService>()
                                    .BuildServiceProvider();
            var _logService = serviceProvider.GetService<ILogService>();
            _logService.Log(context);
        }
    }
}
