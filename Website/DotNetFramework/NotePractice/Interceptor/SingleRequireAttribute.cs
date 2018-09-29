using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotePractice.Interceptor
{
    /// <summary>
    /// 防止多次提交
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SingleRequireAttribute : Attribute, ISingleRequireAttribute
    {
        public SingleRequireAttribute() { }
    }
}