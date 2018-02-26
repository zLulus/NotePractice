using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamarinDemo.CustomizingCell.Models
{
    public class Employee //: INotifyPropertyChanged
    {
        private string displayName;
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                if (displayName != value)
                {
                    displayName = value;
                    //OnPropertyChanged("DisplayName");
                }
            }
        }

        private string twitter;
        public string Twitter
        {
            get
            {
                return twitter;
            }
            set
            {
                if (twitter != value)
                {
                    twitter = value;
                    //OnPropertyChanged("Twitter");
                }
            }
        }

        private string imageUri;
        public string ImageUri
        {
            get
            {
                return imageUri;
            }
            set
            {
                if (imageUri != value)
                {
                    imageUri = value;
                    //OnPropertyChanged("ImageUri");
                }
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    var changed = PropertyChanged;
        //    if (changed != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }
}
