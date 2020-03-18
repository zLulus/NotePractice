using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.TypeConversion.Models
{
    public class Animal
    {
        public Animal(string name, decimal weight)
        {
            Name = name;
            Weight = weight;
        }

        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="cat"></param>
        public static implicit operator Animal(Cat cat)
        {
            return new Animal(cat.Name, cat.Weight);
        }

        /// <summary>
        /// 显示转换:不支持隐式转换的写法
        /// </summary>
        /// <param name="cat"></param>
        //public static explicit operator Animal(Cat cat)
        //{
        //    return new Animal(cat.Name, cat.Weight);
        //}




        public string Name { get; set; }
        public decimal Weight { get; set; }
    }
}
