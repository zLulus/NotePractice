using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.TypeConversion.Models
{
    public class Cat
    {
        public Cat(string name, int weight)
        {
            this.Name = name;
            this.Weight = weight;
        }

        public string Name { get; set; }
        public decimal Weight { get; set; }
    }
}
