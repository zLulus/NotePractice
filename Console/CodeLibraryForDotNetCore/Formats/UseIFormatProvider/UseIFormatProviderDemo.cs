using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.Formats.UseIFormatProvider
{
    public class UseIFormatProviderDemo
    {
        public static void Run()
        {
            Console.WriteLine($"使用IFormatProvider & ICustomFormatter");
            Temperature temp1 = new Temperature(0);
            Console.WriteLine(String.Format(new TemperatureFormatProvider(), "{0:C} (Celsius) = {0:K} (Kelvin) = {0:F} (Fahrenheit)\n", temp1));
        }
    }
}
