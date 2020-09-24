using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.InterfacesAndAbstractClasses
{
    public abstract class Person: Biological
    {
        /// <summary>
        /// 名字，人类都有的属性，可以继承
        /// </summary>
        protected string Name { get; set; }
        /// <summary>
        /// 生活，人类共有方法，可继承
        /// </summary>
        protected void Live()
        {
            Console.WriteLine($"人类生活");
        }
    }
}
