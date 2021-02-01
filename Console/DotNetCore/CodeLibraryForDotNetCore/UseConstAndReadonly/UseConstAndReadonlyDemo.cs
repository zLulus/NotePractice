using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseConstAndReadonly
{
    public class UseConstAndReadonlyDemo
    {
        private const int constNumber1 = 100;
        private readonly int readonlyNumber1 = 100;
        private static readonly int readonlyNumber2 = 100;
        static UseConstAndReadonlyDemo()
        {
            //constNumber1 = 200;//X
            //static readonly 可以在静态构造函数中赋值
            readonlyNumber2 = 200;
        }

        public UseConstAndReadonlyDemo()
        {
            //constNumber1 = 200;//X
            //readonly可以在构造函数中赋值
            readonlyNumber1 = 200;
            //readonlyNumber2 = 200;//X
        }
    }
}
