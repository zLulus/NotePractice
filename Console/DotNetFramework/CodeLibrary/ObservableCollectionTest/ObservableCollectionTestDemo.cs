using System.Collections.ObjectModel;
using System.Linq;

namespace CodeLibrary.ObservableCollectionTest
{
    public class ObservableCollectionTestDemo
    {
        public static void Run()
        {
            ObservableCollection<object> collection = new ObservableCollection<object>();
            collection.Add(new object());
            var item = collection.ElementAtOrDefault(2);
            item = collection.ElementAtOrDefault(0);
        }
    }
}
