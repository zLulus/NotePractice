using CoreWebsite.Castle.Windsor.Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebsite.Castle.Windsor.Demo.Classes
{
    public class Dependency2 : IDependency2
    {
        public object SomeOtherObject { get; set; }
    }
}
