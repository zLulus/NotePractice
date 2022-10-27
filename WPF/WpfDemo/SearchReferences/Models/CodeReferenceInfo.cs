using System;
using System.Collections.Generic;

namespace WpfDemo.SearchReferences.Models
{
    internal class CodeReferenceInfo
    {
        internal string FilePath { get; set; }
        internal string FileName { get; set; }
        internal string MethodName { get; set; }
        internal string Parameters { get; set; }
        internal int MethodParameterCount { get; set; }
        internal string Code { get; set; }
        internal List<string> ParentReferences { get; set; }
        /// <summary>
        /// FilePath,InstanceName,MethodName_MethodParameterCount
        /// </summary>
        internal List<Tuple<string, string, string>> ChildReferences { get; set; }
        internal List<string> ChildReferencesDictionary { get; set; }

        public CodeReferenceInfo()
        {
            ParentReferences = new List<string>();
        }
    }
}
