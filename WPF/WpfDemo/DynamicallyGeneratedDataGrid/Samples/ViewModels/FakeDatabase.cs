using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.DynamicallyGeneratedDataGrid.Samples
{
    public class FakeDatabase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //实现INotifyPropertyChanged接口
        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public int Id { get; set; }

        public string ItemName { get; set; }

        public List<FakeDatabase> GenerateFakeSource()
        {
            List<FakeDatabase> source = new List<FakeDatabase>();

            for (int i = 0; i < 2000; i++)
            {
                FakeDatabase item = new FakeDatabase()
                {
                    Id = i,
                    ItemName = "Item" + i.ToString(),
                    IsChecked = i%2==0
                };

                source.Add(item);
            }

            return source;
        }

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                NotifyPropertyChanged(nameof(IsChecked));
            }
        }


        public FakeDatabase()
        {

        }
    }
}
