using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamarinDemo.PhoneCalls.Models
{
    public class PhoneCallViewModel : INotifyPropertyChanged
    {
        string phoneNumber;

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (phoneNumber != value)
                {
                    phoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
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
