using System.Globalization;

namespace qlbb2.Helper
{
    public class FirstCharConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string userName && !string.IsNullOrEmpty(userName))
            {
                return userName.Substring(0, 1).ToUpper();
            }
            return "?";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 