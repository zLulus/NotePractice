using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.TypeConversion.Models
{
    /// <summary>
    /// 阴性植物
    /// </summary>
    public class NegativePlant: Plant
    {
        public decimal MaximumSurvivalTemperature { get; set; }
    }
}
