using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeLibraryForDotNetCore.Reflections
{
    public class ReflectionDemo
    {
        public static void Run()
        {
            // Using GetType to obtain type information:
            int i = 42;
            Type type = i.GetType();
            Console.WriteLine(type);

            // Using Reflection to get information of an Assembly:
            Assembly info = typeof(int).Assembly;
            Console.WriteLine(info);

            //获取程序集的 Assembly 对象和模块所必需的语法
            // Gets the mscorlib assembly in which the object is defined.
            Assembly a = typeof(object).Module.Assembly;

            //从已加载的程序集获取 Type 对象
            // Loads an assembly using its file name.
            Assembly a2 = Assembly.LoadFrom("DotNetCoreConsole.dll");
            // Gets the type names from the assembly.
            Type[] types2 = a2.GetTypes();
            foreach (Type t in types2)
            {
                Console.WriteLine(t.FullName);
            }
        }
    }
}
