using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibraryForDotNetCore.UseEqualsAndHashCodes
{
    public class UseEqualsAndHashCodesDemo
    {
        public static void TestHashCode()
        {
            var t1 = new Task(() => { });
            var t2 = t1;
            var t3 = new Task(() => { });
            //t1与t2相同，二者与t3不同
            Console.WriteLine($"t1:{t1.GetHashCode()}");
            Console.WriteLine($"t2:{t2.GetHashCode()}");
            Console.WriteLine($"t3:{t3.GetHashCode()}");

            var p1 = new Person() { Name="HAHA"};
            var p2 = p1;
            var p3 = new Person() { Name = "HAHA" };
            Console.WriteLine($"p1:{p1.GetHashCode()}");
            Console.WriteLine($"p2:{p2.GetHashCode()}");
            Console.WriteLine($"p3:{p3.GetHashCode()}");
        }

        public static void TestEquals()
        {
            //值类型
            var i1 = 60;
            var i2 = 60;
            //i1和i2hashCode相同
            Console.WriteLine($"i1:{i1.GetHashCode()}");
            Console.WriteLine($"i2:{i2.GetHashCode()}");
            Console.WriteLine($"i1==i2?:{i1==i2}");//true
            //Object.ReferenceEquals静态方法比较:比较的是引用地址
            Console.WriteLine($"ReferenceEquals(i1, i2):{ReferenceEquals(i1, i2)}");//false

            //引用类型
            var l1 = new List<int>();
            var l2 = l1;
            var l3 = new List<int>();
            Console.WriteLine($"l1==l2?:{l1 == l2}");//true
            Console.WriteLine($"l1==l3?:{l1 == l3}");//false
            Console.WriteLine($"l2==l3?:{l2 == l3}");//false
            //Object.ReferenceEquals静态方法比较:比较的是引用地址
            Console.WriteLine($"ReferenceEquals(l1, l2):{ReferenceEquals(l1, l2)}");//true
            Console.WriteLine($"ReferenceEquals(l1, l3):{ReferenceEquals(l1, l3)}");//false
            Console.WriteLine($"ReferenceEquals(l2, l3):{ReferenceEquals(l2, l3)}");//false

            //string:特殊引用类型，比较的是数值
            var s1 = "aaa";
            var s2 = s1;
            var s3 = "aaa";
            Console.WriteLine($"s1==s2?:{s1 == s2}");//true
            Console.WriteLine($"s1==s3?:{s1 == s3}");//true
            Console.WriteLine($"s2==s3?:{s2 == s3}");//true
            Console.WriteLine($"s1.Equals(s3)?:{s1.Equals(s3)}");//true
        }
    }
}
