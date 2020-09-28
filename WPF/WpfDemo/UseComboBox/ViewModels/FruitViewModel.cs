using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.UseComboBox.ViewModels
{
    public class FruitViewModel: INotifyPropertyChanged
    {
        private long id;
        public long Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    NotifyPropertyChanged(nameof(Id));
                }
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged(nameof(Name));
                }
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Need to implement this interface in order to get data binding
        /// to work properly.
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
