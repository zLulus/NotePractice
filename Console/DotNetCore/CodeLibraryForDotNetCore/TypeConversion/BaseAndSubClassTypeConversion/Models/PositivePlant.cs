using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.TypeConversion.Models
{
    /// <summary>
    /// 阳性植物
    /// </summary>
    public class PositivePlant: Plant
    {
        public decimal MinimumSurvivalTemperature { get; set; }
    }
}
