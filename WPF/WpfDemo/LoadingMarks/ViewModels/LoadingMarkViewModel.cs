using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.LoadingMarks.ViewModels
{
    public class LoadingMarkViewModel: INotifyPropertyChanged
    {
        private System.Windows.Visibility _loading;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public System.Windows.Visibility Loading
        {
            get
            {
                return _loading;
            }
            set
            {
                if (_loading != value)
                {
                    _loading = value;
                    RaisePropertyChanged(nameof(Loading));
                }
            }
        }
    }
}
