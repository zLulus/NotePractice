using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WpfDemo.Converters;

namespace WpfDemo.DynamicallyGeneratedDataGrid.Samples
{
    public class DeleteVisibilityConverter : IValueConverter
    {
        //当值从绑定源传播给绑定目标时，调用方法Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value as FakeDatabase;
            if (data.Id % 2 == 0)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }
        //当值从绑定目标传播给绑定源时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("");
        }
    }
}
