using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.UseComboBox.ViewModels
{
    internal class SettingWithComboBoxDemoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FruitViewModel> Fruits { get; set; }

        private FruitViewModel selectFruit;
        public FruitViewModel SelectFruit
        {
            get { return selectFruit; }
            set
            {
                if (selectFruit != value)
                {
                    selectFruit = value;
                    NotifyPropertyChanged(nameof(SelectFruit));
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
