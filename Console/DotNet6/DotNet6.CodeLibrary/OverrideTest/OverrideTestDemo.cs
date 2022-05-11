using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.OverrideTest
{
    public static class OverrideTestDemo
    {
        public static void Run()
        {
            //https://docs.microsoft.com/zh-cn/visualstudio/ide/reference/generate-equals-gethashcode-methods?view=vs-2022&WT.mc_id=DT-MVP-5003010
            BaseImaginaryNumber baseImaginaryNumber = new BaseImaginaryNumber(1,2);
            BaseImaginaryNumber baseImaginaryNumber2 = new BaseImaginaryNumber(1, 2);
            Console.WriteLine($"{nameof(BaseImaginaryNumber)}");
            Console.WriteLine(baseImaginaryNumber.Equals(baseImaginaryNumber));
            Console.WriteLine(baseImaginaryNumber.Equals(baseImaginaryNumber2));
            Console.WriteLine(baseImaginaryNumber.GetHashCode()== baseImaginaryNumber2.GetHashCode());

            ImaginaryNumber imaginaryNumber = new ImaginaryNumber(1,2);
            ImaginaryNumber imaginaryNumber2 = new ImaginaryNumber(1, 2);
            Console.WriteLine($"{nameof(ImaginaryNumber)}");
            Console.WriteLine(imaginaryNumber.Equals(imaginaryNumber));
            Console.WriteLine(imaginaryNumber.Equals(imaginaryNumber2));
            Console.WriteLine(imaginaryNumber.Equals(baseImaginaryNumber));
            Console.WriteLine(imaginaryNumber.GetHashCode() == imaginaryNumber2.GetHashCode());

        }
    }
}
