using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CodeLibraryForDotNetCore.Formats.UseIFormatProvider
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/dotnet/api/system.iformatprovider?view=netframework-4.8&WT.mc_id=DT-MVP-5003010
    /// </summary>
    public class TemperatureFormatProvider : IFormatProvider, ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider provider)
        {
            if (arg.GetType() != typeof(Temperature))
                throw new FormatException(String.Format("类型 '{0}' 是无效的.", arg.GetType().ToString()));

            decimal temp = (arg as Temperature).temp;

            if (String.IsNullOrEmpty(format)) format = "G";
            if (provider == null) provider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "G":
                case "C":
                    return temp.ToString("F2", provider) + " °C";
                case "F":
                    var Fahrenheit= temp * 9 / 5 + 32;
                    return Fahrenheit.ToString("F2", provider) + " °F";
                case "K":
                    var Kelvin= temp + 273.15m;
                    return Kelvin.ToString("F2", provider) + " K";
                default:
                    throw new FormatException(String.Format("{0}格式不被支持.", format));
            }
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }
    }
}
