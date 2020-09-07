using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfDemo.MultiBinding.MultiBinding2
{
    public class MultiBinding2Converter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var selectItem = values[0] as ComboBoxItem;
            var isChecked = values[1] as bool?;
            //此处不需要该参数，这里只是展示Converter传参一个固定字符串
            var para = parameter as string;
            if(selectItem!=null && selectItem.Tag.ToString()=="1" &&
                isChecked.HasValue && isChecked.Value)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
