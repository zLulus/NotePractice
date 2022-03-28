using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.MemoryTest
{
    public class MemoryTestDemo
    {
        public static void Run()
        {
            int testCount = 5000000;

            //11M内存
            IList<ValueType> valueTypes = new List<ValueType>();
            for (int i = 0; i < testCount; i++)
                valueTypes.Add(new ValueType());

            //258M内存
            IList<ReferenceType> referenceTypes = new List<ReferenceType>();
            for(int i = 0; i < testCount; i++)
                referenceTypes.Add(new ReferenceType());
        }
    }

    class ReferenceType
    {

    }

    struct ValueType
    {

    }
}
