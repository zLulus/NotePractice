using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseDependencyInjection.Services
{
    public interface ILogService
    {
        void Log(string context);
    }
}
