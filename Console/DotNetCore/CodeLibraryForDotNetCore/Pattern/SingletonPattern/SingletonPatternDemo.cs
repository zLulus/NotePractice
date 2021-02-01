using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.Pattern.SingletonPattern
{
    public class SingletonPatternDemo
    {
        public static void Run()
        {
            var instance= SingletonPatternClass.GetInstance();
        }
    }
}
