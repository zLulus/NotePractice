using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.InterfacesAndAbstractClasses
{
    /// <summary>
    /// 吃饭接口
    /// </summary>
    public interface IEat
    {
        //Eat方法多次重载
        void Eat(decimal money);
        void Eat(string dishName);
    }
}
