using System;
using System.Globalization;
using Xamarin.Forms;

namespace EthereumWallet.Common.Converters
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;
            if (double.TryParse(stringValue, out double doubleValue))
            {
                return doubleValue;
            }

            return 0;
        }
    }
}