using CoreWebsite.Castle.Windsor.Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebsite.Castle.Windsor.Demo.Classes
{
    public class Dependency1 : IDependency1
    {
        public object SomeObject { get; set; }
    }
}
