using CodeLibraryForDotNetCore.TypeConversion.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.TypeConversion
{
    public class TypeConversionDemo
    {
        public static void Run()
        {
            Cat cat = new Cat("喵喵",15);
            var animal = (Animal)cat;//显示转换
            Animal animal12 = cat;//隐式转换
        }
    }
}
