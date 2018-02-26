using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WpfDemo.Converts.Models;

namespace WpfDemo.Converts.Converts
{
    public class ColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //value为当前的对象
            var item = value as ListViewItem;
            var view = ItemsControl.ItemsControlFromItemContainer(item);
            var index = view.ItemContainerGenerator.IndexFromContainer(item);

            var data = view.Items[index] as Student;
            if (data.Age == 22)
                return Brushes.Red;
            if (data.Age % 2 == 0)
                return Brushes.Pink;
            else
                return Brushes.DeepSkyBlue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
