using CoreWebsite.Castle.Windsor.Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebsite.Castle.Windsor.Demo.Tests
{
    public class WindsorContainerTest1
    {
        private IDependency1 object1;
        private IDependency2 object2;

        public WindsorContainerTest1(IDependency1 dependency1, IDependency2 dependency2)
        {
            object1 = dependency1;
            object2 = dependency2;
        }

        public string DoSomething()
        {
            object1.SomeObject = "Hello IDependency1";
            object2.SomeOtherObject = "Hello IDependency2";
            return $"{object1.SomeObject},{object2.SomeOtherObject}";
        }
    }
}
