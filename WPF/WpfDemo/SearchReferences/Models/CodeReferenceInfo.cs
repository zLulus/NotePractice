using System;
using System.Collections.Generic;

namespace WpfDemo.SearchReferences.Models
{
    internal class CodeReferenceInfo
    {
        internal string FilePath { get; set; }
        internal string FileName { get; set; }
        internal string MethodName { get; set; }
        internal int MethodParameterCount { get; set; }
        internal string Code { get; set; }
        /// <summary>
        /// FilePath,InstanceName,MethodName_MethodParameterCount
        /// </summary>
        internal List<Tuple<string, string, string>> References { get; set; }
        internal List<string> ReferencesDictionary { get; set; }
    }
}
