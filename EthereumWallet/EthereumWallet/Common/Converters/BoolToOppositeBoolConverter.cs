﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace EthereumWallet.Common.Converters
{
    public class BoolToOppositeBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}