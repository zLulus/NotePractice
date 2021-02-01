using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.IndexerAndCollection
{
    public class IndexerAndCollectionDemo
    {
        public static void Run()
        {
            TestCollection collection = new TestCollection();
            collection.Add(new DataModel() { Id = 1, Name = "No. 1" });
            collection.Add(new DataModel() { Id = 2, Name = "No. 2" });
            //使用索引器,从0开始计数
            var no2 = collection[1];
            var ni1 = collection["No. 1"];
            collection.Remove(0);
            collection.Remove(x => x.Id == 2);
        }
    }
}
