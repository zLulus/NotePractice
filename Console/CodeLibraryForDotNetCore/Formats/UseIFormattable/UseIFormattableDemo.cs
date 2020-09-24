using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CodeLibraryForDotNetCore.Formats.UseIFormattable
{
    public class UseIFormattableDemo
    {
        public static void Run()
        {
            Console.WriteLine($"使用IFormattable");

            // Use composite formatting with format string in the format item.
            //在格式项中使用带有格式字符串的复合格式
            Temperature temp1 = new Temperature(0);
            //带有格式化字符串C/K/F
            Console.WriteLine("{0:C} (Celsius) = {0:K} (Kelvin) = {0:F} (Fahrenheit)\n", temp1);

            // Use composite formatting with a format provider.
            //与Format Provider一起使用复合格式
            temp1 = new Temperature(-40);
            Console.WriteLine(String.Format(CultureInfo.CurrentCulture, "{0:C} (Celsius) = {0:K} (Kelvin) = {0:F} (Fahrenheit)", temp1));
            Console.WriteLine(String.Format(new CultureInfo("fr-FR"), "{0:C} (Celsius) = {0:K} (Kelvin) = {0:F} (Fahrenheit)\n", temp1));

            // Call ToString method with format string.
            //用格式字符串调用ToString方法
            temp1 = new Temperature(32);
            Console.WriteLine("{0} (Celsius) = {1} (Kelvin) = {2} (Fahrenheit)\n",
                              temp1.ToString("C"), temp1.ToString("K"), temp1.ToString("F"));

            // Call ToString with format string and format provider
            //用格式字符串、Format Provider调用ToString方法
            temp1 = new Temperature(100);
            NumberFormatInfo current = NumberFormatInfo.CurrentInfo;
            CultureInfo nl = new CultureInfo("nl-NL");
            Console.WriteLine("{0} (Celsius) = {1} (Kelvin) = {2} (Fahrenheit)",
                              temp1.ToString("C", current), temp1.ToString("K", current), temp1.ToString("F", current));
            Console.WriteLine("{0} (Celsius) = {1} (Kelvin) = {2} (Fahrenheit)",
                              temp1.ToString("C", nl), temp1.ToString("K", nl), temp1.ToString("F", nl));
        }
    }
}
