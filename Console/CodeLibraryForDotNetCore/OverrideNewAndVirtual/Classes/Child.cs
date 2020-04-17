using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeLibraryForDotNetCore.OverrideNewAndVirtual.Classes
{
    public class Child: Parent
    {
        public override void VirtualMethod()
        {
            base.VirtualMethod();
            Console.WriteLine($"{nameof(Child)}.{nameof(VirtualMethod)}");
        }

        public new void NewMethod()
        {
            //base.NewMethod();
            Console.WriteLine($"{nameof(Child)}.{nameof(NewMethod)}");
        }
    }
}
