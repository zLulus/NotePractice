using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Converters
{
    public class FakeDatabase
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public static List<FakeDatabase> GenerateFakeSource()
        {
            List<FakeDatabase> source = new List<FakeDatabase>();

            for (int i = 0; i < 10; i++)
            {
                FakeDatabase item = new FakeDatabase()
                {
                    Id = i,
                    ItemName = "Item" + i.ToString()
                };

                source.Add(item);
            }

            return source;
        }

        public FakeDatabase()
        {

        }
    }
}
