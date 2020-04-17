using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeLibraryForDotNetCore.OverrideNewAndVirtual.Classes
{
    public class Parent
    {

        public void NewMethod()
        {
            Console.WriteLine($"{nameof(Parent)}.{nameof(NewMethod)}");
        }

        public virtual void VirtualMethod()
        {
            Console.WriteLine($"{nameof(Parent)}.{nameof(VirtualMethod)}");
        }

        private void PrivateMethod()
        {
            //不可被子类访问
        }
    }
}
