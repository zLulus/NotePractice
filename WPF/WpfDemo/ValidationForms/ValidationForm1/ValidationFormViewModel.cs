using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.ValidationForms.ViewModels
{
    public class ValidationFormViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        /// <summary>
        /// Need to implement this interface in order to get data binding
        /// to work properly.
        /// </summary>
        /// <param name="propertyName"></param>
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        private String name;
        public String Name
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
        private String age;
        public String Age
        {
            get { return age; }
            set
            {
                if (age != value)
                {
                    age = value;
                    NotifyPropertyChanged(nameof(Age));
                }
            }
        }
        private String remark;
        public String Remark
        {
            get { return remark; }
            set
            {
                if (remark != value)
                {
                    remark = value;
                    NotifyPropertyChanged(nameof(Remark));
                }
            }
        }
    }
}
