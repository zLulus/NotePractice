using System.Reflection;

namespace DotNet6.CodeLibrary.HigherOrderFunctionTest
{
    public class HigherOrderFunctionTestDemo
    {
        public static void Run()
        {
            //高阶函数使用1
            var t1 = ShowInfoFromClassType(typeof(Class1));
            var t2 = t1(nameof(Class1.Class1Method));
            var t3 = t2(typeof(Class2));
            t3();

            //高阶函数使用2
            ShowInfoFromClassType(typeof(Class1))
                (nameof(Class1.Class1Method))
                (typeof(Class2))
                ();
        }

        public static Func<Type, Func<string, Func<Type, Action>>> ShowInfoFromClassType =
           //第一层：Func<Type, Func<string, Func<Type, Action>>>
           classType =>
           {
               Console.WriteLine($"类型:{classType.Name}");

               //第二层：Func<string, Func<Type, Action>>
               return methodName =>
               {
                   MethodInfo? methodInfo = classType.GetMethod(methodName);
                   if (methodInfo != null)
                   {
                       Console.WriteLine($"方法名签名:{methodInfo.Name}");
                       //第三层：Func<Type, Action>
                       return attrType => () =>
                       {
                           Console.WriteLine($"类型:{attrType.Name}");
                       };
                   }
                   else
                   {
                       //第三层：Func<Type, Action>
                       return attrType => () => { };
                   }
               };
           };
    }

    public class Class1
    {
        public void Class1Method()
        {
        }
    }

    public class Class2
    {

    }
}
