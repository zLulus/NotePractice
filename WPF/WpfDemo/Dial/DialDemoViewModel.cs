using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfDemo.Dial
{
    public class DialDemoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //实现INotifyPropertyChanged接口
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string ip;
        public string IP
        {
            get { return ip; }
            set
            {
                ip = value;
                NotifyPropertyChanged(nameof(IP));
            }
        }
        private string adapterName;
        public string AdapterName
        {
            get { return adapterName; }
            set
            {
                adapterName = value;
                NotifyPropertyChanged(nameof(AdapterName));
            }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                NotifyPropertyChanged(nameof(UserName));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }
        private ComboBoxItem protocol;
        public ComboBoxItem Protocol
        {
            get { return protocol; }
            set
            {
                protocol = value;
                NotifyPropertyChanged(nameof(Protocol));
            }
        }
        private string preSharedKey;
        public string PreSharedKey
        {
            get { return preSharedKey; }
            set
            {
                preSharedKey = value;
                NotifyPropertyChanged(nameof(PreSharedKey));
            }
        }
    }
}
