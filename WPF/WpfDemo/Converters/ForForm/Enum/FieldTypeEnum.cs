using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Converters.ForForm
{
    public enum FieldTypeEnum
    {
        [Description("字符串")]
        String = 1,
        [Description("整型")]
        Int = 2,
        [Description("长整型")]
        Long = 3,
        [Description("双精度小数")]
        Double = 4,
        [Description("精确小数")]
        Decimal = 5,
        [Description("时间")]
        Datetime = 6,
        [Description("日期")]
        Date = 7,
    }
}
