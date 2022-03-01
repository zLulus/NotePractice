using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.DesignPatterns.BuilderPatternTest.Builders
{
    /// <summary>
    /// 具体创建者，具体的车型的创建者，例如：别克
    /// </summary>
    internal sealed class BuickBuilder : ICarBuilder
    {
        private readonly Car buickCar = new Car();

        private void UsePartBase(string partName, string? part = null)
        {
            if (part == null)
            {
                part = $"Default Buick`s {partName}";
            }

            this.buickCar.Add(partName, part);
        }

        public ICarBuilder UseCarDoor(string? part = null)
        {
            this.UsePartBase("Door", part);
            return this;
        }

        public ICarBuilder UseCarWheel(string? part = null)
        {
            this.UsePartBase("Wheel", part);
            return this;
        }

        public ICarBuilder UseCarEngine(string? part = null)
        {
            this.UsePartBase("Engine", part);
            return this;
        }

        public ICarBuilder UseOtherPart([NotNull] string partName, string? part)
        {
            this.buickCar.Add(partName, $"Buick`s{part}");
            return this;
        }

        public Car BuildCar()
        {
            Console.WriteLine("汽车开始在组装.......");
            this.buickCar.Show();
            Console.WriteLine("汽车组装好了");
            return this.buickCar;
        }
    }
}
