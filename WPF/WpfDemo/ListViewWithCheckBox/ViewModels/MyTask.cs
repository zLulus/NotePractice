using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.ListViewWithCheckBox.ViewModels
{
    public class MyTask : INotifyPropertyChanged
    {
        private string taskName;
        public string TaskName
        {
            get
            {
                return taskName;
            }
            set
            {
                taskName = value;
                NotifyPropertyChanged(nameof(TaskName));
            }
        }
        private bool isChecked;
        public bool IsChecked
        {
            get 
            { 
                return isChecked; 
            }
            set
            {
                isChecked = value;
                NotifyPropertyChanged(nameof(IsChecked));
            }
        }

        /// <summary>
        /// 属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
