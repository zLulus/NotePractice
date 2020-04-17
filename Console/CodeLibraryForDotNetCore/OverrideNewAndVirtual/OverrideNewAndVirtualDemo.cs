using CodeLibraryForDotNetCore.OverrideNewAndVirtual.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.OverrideNewAndVirtual
{
    public class OverrideNewAndVirtualDemo
    {
        public static void Run()
        {
            Child child = new Child();
            child.NewMethod();
            child.VirtualMethod();
        }
    }
}
