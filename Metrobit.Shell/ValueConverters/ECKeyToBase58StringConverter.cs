using System;
using System.Globalization;
using System.Windows.Data;
using com.google.bitcoin.core;

namespace Metrobit.Shell.ValueConverters
{
    public class ECKeyToBase58StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = value as ECKey;

            if (key == null)
            {
                return value;
            }

            return Base58.encode(key.getPrivKeyBytes());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
