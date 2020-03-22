using CodeLibraryForDotNetCore.TypeConversion.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.TypeConversion
{
    /// <summary>
    /// 基类、子类之间的类型转换
    /// </summary>
    public class BaseAndSubClassTypeConversionDemo
    {
        public static void Run()
        {
            PositivePlant positivePlant = new PositivePlant() { Name = "阳性植物", MinimumSurvivalTemperature = 10 };
            //子转基：隐式
            Plant plant = positivePlant;
            //Plant plant = (Plant)positivePlant;//正确的写法
            //基转子：显式
            PositivePlant convertFromPlant = (PositivePlant)plant;
            //PositivePlant convertFromPlant = plant;//错误的写法
            Console.WriteLine($"positivePlant == plant:{positivePlant == plant}");//true
            Console.WriteLine($"positivePlant == convertFromPlant:{positivePlant == convertFromPlant}");//true

            //as运算符
            Plant plant2 = positivePlant as Plant;
            PositivePlant convertFromPlant2 = plant2 as PositivePlant;
            Console.WriteLine($"positivePlant == plant2:{positivePlant == plant2}");//true
            Console.WriteLine($"positivePlant == convertFromPlant2:{positivePlant == convertFromPlant2}");//true
        }
    }
}
