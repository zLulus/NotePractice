using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet6.CodeLibrary.XMLToJsonTest
{
    public class XMLToJsonModel 
    {
        public XMLToJsonResponse response { get; set; }
    }

    public class XMLToJsonResponse
    {
        public ItemList itemList { get; set; }
    }

    public class ItemList
    {
        public Item[] item { get; set; }
    }

    public class Item
    {
        public long number { get; set; }
    }
}
