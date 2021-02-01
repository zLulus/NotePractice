using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeLibraryForDotNetCore.IndexerAndCollection
{
    //完全可以写成泛型
    public class TestCollection
    {
        private List<DataModel> dataList;
        public TestCollection(List<DataModel> dataList)
        {
            this.dataList = dataList;
        }

        public TestCollection()
        {
            this.dataList = new List<DataModel>();
        }

        public DataModel this[string name]
        {
            get { return dataList.Find(x => x.Name == name); }
        }

        public DataModel this[int index]
        {
            get { return dataList[index]; }
        }

        public void Add(DataModel data)
        {
            dataList.Add(data);
        }

        public void Remove(int index)
        {
            dataList.RemoveAt(index);
        }

        public void Remove(Predicate<DataModel> predicate)
        {
            dataList.RemoveAll(predicate);
        }
    }
}
