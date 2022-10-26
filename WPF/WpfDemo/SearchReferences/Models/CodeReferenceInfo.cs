using System.Collections.Generic;

namespace WpfDemo.SearchReferences.Models
{
    internal class CodeReferenceInfo
    {
        internal string FilePath { get; set; }
        internal string MethodName { get; set; }
        internal int MethodParameterCount { get; set; }
        internal string Code { get; set; }
        /// <summary>
        /// FilePath_MethodName_MethodParameterCount
        /// </summary>
        internal List<string> References { get; set; }
    }
}
