using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.InterfacesAndAbstractClasses
{
    public interface IEat
    {
        void Eat(decimal money);
        void Eat(string dishName);
    }
}
