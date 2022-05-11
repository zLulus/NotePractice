using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.OverrideTest
{
    public class BaseImaginaryNumber
    {
        public double RealNumber { get; set; }
        public double ImaginaryUnit { get; set; }

        public BaseImaginaryNumber(double realNumber,double imaginaryUnit)
        {
            RealNumber = realNumber;
            ImaginaryUnit = imaginaryUnit;
        }
    }
}
