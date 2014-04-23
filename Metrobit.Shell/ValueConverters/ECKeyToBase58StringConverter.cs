using System;
using System.Globalization;
using System.Windows.Data;
using com.google.bitcoin.core;
using Metrobit.Shell.Models;
using Microsoft.Practices.ServiceLocation;

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

            var parameters = ServiceLocator.Current.GetInstance<MetrobitWalletAppKit>().@params();
            return new Address(parameters, key.getPubKeyHash()).toString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
