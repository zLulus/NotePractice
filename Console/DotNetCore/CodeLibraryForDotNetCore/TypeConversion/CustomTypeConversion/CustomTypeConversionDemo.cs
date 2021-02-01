using CodeLibraryForDotNetCore.TypeConversion.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.TypeConversion
{
    /// <summary>
    /// 自定义显隐式类型转换
    /// </summary>
    public class CustomTypeConversionDemo
    {
        public static void Run()
        {
            Cat cat = new Cat("喵喵",15);
            var animal = (Animal)cat;//显示转换
            Animal animal12 = cat;//隐式转换(explicit写法不支持)
        }
    }
}
