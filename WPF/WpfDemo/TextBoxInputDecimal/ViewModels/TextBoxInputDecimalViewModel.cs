using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.TextBoxInputDecimal.ViewModels
{
    public class TextBoxInputDecimalViewModel : INotifyPropertyChanged
    {
        private decimal inputNumber2;
        public decimal InputNumber2
        {
            get
            {
                return inputNumber2;
            }
            set
            {
                inputNumber2 = value;
                NotifyPropertyChanged(nameof(InputNumber2));
            }
        }
        private decimal inputNumber;
        public decimal InputNumber
        {
            get
            {
                return inputNumber;
            }
            set
            {
                inputNumber = value;
                NotifyPropertyChanged(nameof(InputNumber));
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
