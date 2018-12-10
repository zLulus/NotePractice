using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebsite.Castle.Windsor.Demo.Interfaces
{
    public interface IMyDependency
    {
        Task<string> WriteMessage(string message);
    }
}
