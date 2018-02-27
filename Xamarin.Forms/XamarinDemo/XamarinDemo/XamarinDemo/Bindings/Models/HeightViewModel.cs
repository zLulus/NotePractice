using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace XamarinDemo.Bindings.Models
{
    public class HeightViewModel : INotifyPropertyChanged
    {
        decimal customHeight;
        GridLength gridLength;

        public GridLength GridLength { get; set; }
        //{
        //    get
        //    {
        //        return gridLength;
        //    }
        //    set
        //    {
        //        if (gridLength != value)
        //        {
        //            gridLength = value;
        //            OnPropertyChanged("GridLength");
        //        }
        //    }
        //}

        public decimal CustomHeight
        {
            get
            {
                return customHeight;
            }
            set
            {
                if (customHeight != value)
                {
                    customHeight = value;
                    OnPropertyChanged("CustomHeight");
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
