using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.UseArray
{
    public class UseArrayDemo
    {
        public static void Run()
        {
            //ArrayList
            ArrayListTest();
            ArrayListTest(1);

            //Array
            ArrayTest();
            //触发报错：System.IndexOutOfRangeException: 'Index was outside the bounds of the array.'
            //ArrayTest(1);

            //List
            ListTest();
            ListTest(1);

            //ArrayList和List的区别
            //ArrayList存在装箱拆箱，类型不匹配的问题，List更优
        }

        private static void ListTest(int length = 0)
        {
            List<object> list;
            if (length > 0)
            {
                list = new List<object>(length);
            }
            else
            {
                list = new List<object>();
            }
            list.Add(1);
            list.Add("2");
            list.Add((decimal)5.5);
            var dic = new Dictionary<string, string>();
            dic.Add("key1", "value1");
            list.Add(dic);
        }

        private static void ArrayTest(int length = 0)
        {
            if (length <= 0)
            {
                length = 5;
            }
            object[] array = new object[length];
            array.SetValue(1,0);
            array.SetValue("2",1);
            array.SetValue((decimal)5.5,2);
            var dic = new Dictionary<string, string>();
            dic.Add("key1", "value1");
            array.SetValue(dic,3);
        }

        private static void ArrayListTest(int length=0)
        {
            //ArrayList:whose size is dynamically increased as required.
            ArrayList arrayList;
            if (length > 0)
            {
                arrayList = new ArrayList(length);
            }
            else
            {
                arrayList = new ArrayList();
            }
            arrayList.Add(1);
            arrayList.Add("2");
            arrayList.Add((decimal)5.5);
            var dic = new Dictionary<string, string>();
            dic.Add("key1", "value1");
            arrayList.Add(dic);
        }
    }
}
