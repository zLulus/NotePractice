using DotNet6.CodeLibrary.DesignPatterns.BuilderPatternTest.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.DesignPatterns.BuilderPatternTest
{
    public class BuilderPatternTestDemo
    {
        public static void Run()
        {
            ICarBuilder buickCarBuilder = new BuickBuilder();
            ICarBuilder aoDiCarBuilder = new AoDiBuilder();

            //别克
            Car buickCar = buickCarBuilder
                .UseCarDoor()
                .UseCarEngine()
                .UseCarWheel("雪地专用车轮")
                .BuildCar();

            Console.WriteLine();

            //奥迪
            Car aoDiCar = aoDiCarBuilder
                .UseCarDoor()
                .UseCarEngine()
                .UseCarWheel()
                .BuildCar();
        }
    }
}
