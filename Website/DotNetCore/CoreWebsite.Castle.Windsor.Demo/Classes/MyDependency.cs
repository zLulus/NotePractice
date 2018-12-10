using CoreWebsite.Castle.Windsor.Demo.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebsite.Castle.Windsor.Demo.Classes
{
    public class MyDependency : IMyDependency
    {
        private readonly ILogger<MyDependency> _logger;

        public MyDependency(ILogger<MyDependency> logger)
        {
            _logger = logger;
        }

        public async Task<string> WriteMessage(string message)
        {
            string s = $"MyDependency.WriteMessage called. Message: {message}";
            _logger.LogInformation(s);
            await Task.FromResult(0);
            return s;
        }
    }
}
