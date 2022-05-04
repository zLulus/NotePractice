using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.EmitTest
{
    public interface IConsole
    {
        void Say();
    }
    public static  class EmitTestDemo
    {
        public static void Run()
        {
            //https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.emit?redirectedfrom=MSDN&view=net-6.0&WT.mc_id=DT-MVP-5003010

            //https://www.jianshu.com/p/b6b86476c106
            // 在看下面的代码之前，先明白这个依赖关系，即：
            // 方法->类型->模块->程序集


            //定义程序集的名称
            AssemblyName aName = new AssemblyName("DynamicAssemblyExample");

            // 创建一个程序集构建器
            // Framework应该这样：AppDomain.CurrentDomain.DefineDynamicAssembly
            AssemblyBuilder ab =
                AssemblyBuilder.DefineDynamicAssembly(
                    aName,
                    AssemblyBuilderAccess.Run);


            // 使用程序集构建器创建一个模块构建器
            ModuleBuilder mb = ab.DefineDynamicModule(aName.Name + ".dll");

            // 使用模块构建器创建一个类型构建器
            TypeBuilder tb = mb.DefineType("DynamicConsole");

            // 使类型实现IConsole接口
            tb.AddInterfaceImplementation(typeof(IConsole));

            var attrs = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Final;

            // 使用类型构建器创建一个方法构建器
            MethodBuilder methodBuilder = tb.DefineMethod("Say", attrs, typeof(void), Type.EmptyTypes);

            // 通过方法构建器获取一个MSIL生成器
            var IL = methodBuilder.GetILGenerator();

            // 开始编写方法的执行逻辑

            // 将一个字符串压入栈顶
            IL.Emit(OpCodes.Ldstr, "I'm here.");

            // 调用Console.Writeline函数
            IL.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));

            // 退出函数
            IL.Emit(OpCodes.Ret);

            //方法结束


            // 从类型构建器中创建出类型
            var dynamicType = tb.CreateType();


            // 通过反射创建出动态类型的实例
            var console = Activator.CreateInstance(dynamicType) as IConsole;
            console.Say();

            Console.ReadLine();
        }

    }
}
