using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.Formats.UseIFormatProvider
{
    public class Temperature
    {
        internal decimal temp;

        public Temperature(decimal temperature)
        {
            if (temperature < -273.15m)
                throw new ArgumentOutOfRangeException(String.Format("{0}低于绝对零度.",temperature));
            this.temp = temperature;
        }
    }
}
