using System.Globalization;

namespace Maui.Demo.Pages.Bindings.Converters
{
    public class SumConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return null;

            double result = 0;
            foreach (var value in values)
            {
                if (value != null && value is double)
                {
                    result += (double)value;
                }
            }

            return result.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
