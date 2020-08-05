using System;
using System.Globalization;
using Xamarin.Forms;

namespace EthereumWallet.Common.Converters
{
    public class TokenPriceAvailableSupplyEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var doubleValue = (double)value;
            if (doubleValue > 0)
            {
                return doubleValue.ToString();
            }

            return "No Data";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
