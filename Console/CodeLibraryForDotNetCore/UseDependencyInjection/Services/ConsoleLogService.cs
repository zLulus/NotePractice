using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseDependencyInjection.Services
{
    public class ConsoleLogService: ILogService
    {
        public void Log(string context)
        {
            Console.WriteLine(context);
        }
    }
}
