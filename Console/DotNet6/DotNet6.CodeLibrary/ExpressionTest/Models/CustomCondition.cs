using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.ExpressionTest
{
    internal class CustomCondition
    {
        internal MethodTypeEnum MethodType { get; set; }
        internal ConnectionTypeEnum ConnectionType { get; set; }
        internal object Value { get; set; }
    }
}
