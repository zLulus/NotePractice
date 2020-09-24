using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.InterfacesAndAbstractClasses
{
    public abstract class Biological
    {
        /// <summary>
        /// 生活，生物共有方法，可继承
        /// </summary>
        public void Live()
        {
            Console.WriteLine($"生物生活");
        }
    }
}
