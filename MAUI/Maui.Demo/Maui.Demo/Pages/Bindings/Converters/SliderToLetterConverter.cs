using System.Globalization;

namespace Maui.Demo.Pages.Bindings.Converters
{
    public class SliderToLetterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                var intValue = (int)Math.Round((double)value);
                char letter = (char)('A' + intValue);
                return letter.ToString();
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
