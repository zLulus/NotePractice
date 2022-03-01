using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.ReferenceTest
{
    public class ReferenceTestDemo
    {
        public static void Run()
        {
            ValueReference(new StructModel() { ID=Guid.NewGuid().ToString(),Name="from value type model"});
            Console.WriteLine();
            ObjectReference(new ReferenceModel() { ID = Guid.NewGuid().ToString(), Name = "from reference type model" });
        }

        public static StructModel ValueReference(in StructModel model)
        {
            Console.WriteLine($"原始 ID:{model.ID}\t Name:{model.Name}");
            Console.WriteLine($"原始 code:{ model.GetHashCode()}");

            StructModel testPoint = model;
            Console.WriteLine($"赋值后 code:{ testPoint.GetHashCode()}");

            StructModel newPoint = new StructModel() { ID = Guid.NewGuid().ToString(), Name = "from value type model(new 1)" };//这里看起来new了 其实哈西值后上面一样的
            Console.WriteLine($"new 1后code:{ newPoint.GetHashCode()}");

            Console.WriteLine($"new 1后 ID:{model.ID}\t Name:{model.Name}");

            StructModel newPoint2 = new StructModel() { ID = Guid.NewGuid().ToString(), Name = "from value type model(new 2)" };
            Console.WriteLine($"new 2后code:{ newPoint2.GetHashCode()}");

            Console.WriteLine($"new 2后 ID:{model.ID}\t Name:{model.Name}");
            return model;
        }

        public static ReferenceModel ObjectReference(in ReferenceModel model)
        {
            Console.WriteLine($"原始 ID:{model.ID}\t Name:{model.Name}");
            Console.WriteLine($"原始 code:{ model.GetHashCode()}");

            model.ID = Guid.NewGuid().ToString();
            model.Name = "from reference type model(new name)";

            Console.WriteLine($"赋值后 ID:{model.ID}\t Name:{model.Name}");
            Console.WriteLine($"赋值后 code:{ model.GetHashCode()}");

            return model;
        }
    }
}
