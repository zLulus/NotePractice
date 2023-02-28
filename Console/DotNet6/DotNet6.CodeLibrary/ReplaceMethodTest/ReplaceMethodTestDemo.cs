using System.Reflection;
using System.Runtime.CompilerServices;

namespace DotNet6.CodeLibrary.ReplaceMethodTest
{
    public class ReplaceMethodTestDemo
    {
        public static void Run()
        {
            //return 1
            var t1 = TestClass.Test1();
            //return 2
            var t2 = TestClass.Test2();
            var state = ReplaceMethod(typeof(TestClass).GetMethod("Test1"), typeof(TestClass).GetMethod("Test2"));
            //return 2 method is replaced!
            var t1Replace = TestClass.Test1();
            //return 2
            var t2Replace = TestClass.Test2();
        }

        public static unsafe MethodReplaceState ReplaceMethod(MethodInfo methodToReplace, MethodInfo methodToInject)
        {
            RuntimeHelpers.PrepareMethod(methodToReplace.MethodHandle);
            RuntimeHelpers.PrepareMethod(methodToInject.MethodHandle);

            var tar = Calculation(methodToReplace);
            var inj = Calculation(methodToInject);

            tar = *(IntPtr*)tar + 1;
            inj = *(IntPtr*)inj + 1;
            MethodReplaceState state = new MethodReplaceState();
            state.Location = tar;
            state.OriginalLocation = new IntPtr(*(int*)tar);

            *(int*)tar = *(int*)inj + (int)(long)inj - (int)(long)tar;
            return state;
        }

        private static unsafe IntPtr Calculation(MethodInfo methodToReplace)
        {
            IntPtr tar = methodToReplace.MethodHandle.Value;
            if (!methodToReplace.IsVirtual)
            {
                tar += 8;
            }
            else
            {
                var index = (int)(((*(long*)tar) >> 32) & 0xFF);
                var classStart = *(IntPtr*)(methodToReplace.DeclaringType.TypeHandle.Value + (IntPtr.Size == 4 ? 40 : 64));
                tar = classStart + IntPtr.Size + index;
            }
            return tar;
        }
    }
}
