using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CodeLibraryForDotNetCore.Formats.UseIFormattable
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/dotnet/api/system.iformattable?view=netframework-4.8&WT.mc_id=DT-MVP-5003010
    /// </summary>
    public class Temperature : IFormattable
    {
        private decimal temp;

        public Temperature(decimal temperature)
        {
            if (temperature < -273.15m)
                throw new ArgumentOutOfRangeException(String.Format("{0}低于绝对零度",
                                                      temperature));
            this.temp = temperature;
        }

        /// <summary>
        /// 摄氏度
        /// </summary>
        public decimal Celsius
        {
            get { return temp; }
        }

        /// <summary>
        /// 华氏温度
        /// </summary>
        public decimal Fahrenheit
        {
            get { return temp * 9 / 5 + 32; }
        }

        /// <summary>
        /// 开尔文(热力学温标或称绝对温标)
        /// </summary>
        public decimal Kelvin
        {
            get { return temp + 273.15m; }
        }

        public override string ToString()
        {
            return this.ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (String.IsNullOrEmpty(format)) format = "G";
            if (provider == null) provider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "G":
                case "C":
                    return temp.ToString("F2", provider) + " °C";
                case "F":
                    return Fahrenheit.ToString("F2", provider) + " °F";
                case "K":
                    return Kelvin.ToString("F2", provider) + " K";
                default:
                    throw new FormatException(String.Format("{0}格式不被支持", format));
            }
        }
    }
}
