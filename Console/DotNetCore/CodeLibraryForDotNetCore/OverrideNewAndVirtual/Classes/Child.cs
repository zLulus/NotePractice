using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeLibraryForDotNetCore.OverrideNewAndVirtual.Classes
{
    public class Child: Parent
    {
        /// <summary>
        /// 重写虚方法
        /// </summary>
        public override void VirtualMethod()
        {
            base.VirtualMethod();
            Console.WriteLine($"{nameof(Child)}.{nameof(VirtualMethod)}");
        }

        /// <summary>
        /// 隐藏、new关键词重新实现
        /// </summary>
        public new void NewMethod()
        {
            //base.NewMethod();
            Console.WriteLine($"{nameof(Child)}.{nameof(NewMethod)}");
        }

        #region 重载
        public void OverloadMethod()
        {
            Console.WriteLine($"This is OverloadMethod");
        }

        public void OverloadMethod(string input)
        {
            Console.WriteLine($"This is OverloadMethod:{input}");
        }

        public void OverloadMethod(int input)
        {
            Console.WriteLine($"This is OverloadMethod:{input}");
        }

        public void OverloadMethod(bool input)
        {
            Console.WriteLine($"This is OverloadMethod:{input}");
        }
        #endregion
    }
}
