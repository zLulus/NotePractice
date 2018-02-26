using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinDemo.Bindings.Models
{
    public class Employee : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value.Equals(_firstName, StringComparison.Ordinal))
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }
                _firstName = value;
                OnPropertyChanged();

            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
