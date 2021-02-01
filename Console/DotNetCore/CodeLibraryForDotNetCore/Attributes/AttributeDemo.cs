using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeLibraryForDotNetCore.Attributes
{
    public class AttributeDemo
    {
        public static void Run()
        {
            Rectangle rectangle = new Rectangle(4.5, 7.5);
            rectangle.Display();
            Type type = typeof(Rectangle);
            // 遍历 Rectangle 类的属性
            //获得
            //[DeBugInfo(45, "Zara Ali", "12/8/2012", Message = "Return type mismatch")]
            //[DeBugInfo(49, "Nuha Ali", "10/10/2012", Message = "Unused variable")]
            foreach (Object attributes in type.GetCustomAttributes(false))
            {
                DeBugInfoAttribute dbi = (DeBugInfoAttribute)attributes;
                if (null != dbi)
                {
                    Console.WriteLine("Bug no: {0}", dbi.BugNo);
                    Console.WriteLine("Developer: {0}", dbi.Developer);
                    Console.WriteLine("Last Reviewed: {0}",
                         dbi.LastReview);
                    Console.WriteLine("Remarks: {0}", dbi.Message);
                }
            }

            // 遍历方法属性
            //获得GetArea/Display方法上的属性
            foreach (MethodInfo m in type.GetMethods())
            {
                foreach (Attribute a in m.GetCustomAttributes(true))
                {
                    DeBugInfoAttribute dbi = (DeBugInfoAttribute)a;
                    if (null != dbi)
                    {
                        Console.WriteLine("Bug no: {0}, for Method: {1}",
                             dbi.BugNo, m.Name);
                        Console.WriteLine("Developer: {0}", dbi.Developer);
                        Console.WriteLine("Last Reviewed: {0}",
                           dbi.LastReview);
                        Console.WriteLine("Remarks: {0}", dbi.Message);
                    }
                }
            }
            Console.ReadLine();
        }
    
    }
}
