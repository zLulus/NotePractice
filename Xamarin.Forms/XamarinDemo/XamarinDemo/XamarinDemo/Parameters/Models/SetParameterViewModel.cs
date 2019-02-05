using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamarinDemo.Parameters.Models
{
    public class SetParameterViewModel : INotifyPropertyChanged
    {
        string data1;

        public string Data1
        {
            get
            {
                return data1;
            }
            set
            {
                if (data1 != value)
                {
                    data1 = value;
                    OnPropertyChanged("Data1");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
