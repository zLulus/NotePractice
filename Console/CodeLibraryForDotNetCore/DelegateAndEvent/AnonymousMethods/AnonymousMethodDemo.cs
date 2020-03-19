using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.DelegateAndEvent.AnonymousMethods
{
    public class AnonymousMethodDemo
    {
        delegate void WriteResult(string str);

        public static void Run1()
        {
            Console.WriteLine("Run1");
            // 使用匿名方法创建委托实例
            WriteResult wr = delegate (string str)
            {
                Console.WriteLine("Anonymous Method Write Result: {0}", str);
            };

            // 使用匿名方法调用委托
            wr("Test Anonymous Method");
        }

        #region Run2

        delegate void NumberChanger(int n);
        public static void Run2()
        {
            Console.WriteLine("Run2");

            // 使用命名方法实例化委托
            var nc = new NumberChanger(AddNum);

            // 使用命名方法调用委托
            nc(5);

            // 使用另一个命名方法实例化委托
            nc = new NumberChanger(MultNum);

            // 使用命名方法调用委托
            nc(2);

            //再次挂载MultNum方法，执行两次
            nc += MultNum;
            nc(2);
        }


        static int num = 10;
        public static void AddNum(int p)
        {
            num += p;
            Console.WriteLine("Named Method: {0}", num);
        }

        public static void MultNum(int q)
        {
            num *= q;
            Console.WriteLine("Named Method: {0}", num);
        }
        public static int getNum()
        {
            return num;
        }

        #endregion

    }
}
