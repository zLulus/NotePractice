using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.OverrideTest
{
    public class ImaginaryNumber: BaseImaginaryNumber
    {
        public ImaginaryNumber(double realNumber, double imaginaryUnit) : base(realNumber, imaginaryUnit)
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is ImaginaryNumber number &&
                   RealNumber == number.RealNumber &&
                   ImaginaryUnit == number.ImaginaryUnit;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RealNumber, ImaginaryUnit);
        }
    }
}
