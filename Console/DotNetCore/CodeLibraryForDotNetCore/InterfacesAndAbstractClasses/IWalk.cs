using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.InterfacesAndAbstractClasses
{
    /// <summary>
    /// 走路接口
    /// </summary>
    public interface IWalk
    {
        void Walk();
        void Walk(double weightBearing);
    }
}
