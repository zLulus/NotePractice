using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CodeLibraryForDotNetCore.EnumTools.Enums
{
    internal enum SexEnum
    {
        [Description("男性")]
        Man=1,
        [Description("女性")]
        Woman =2,
        [Description("未知")]
        Unknown =3
    }
}
