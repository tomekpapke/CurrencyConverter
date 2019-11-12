using System;
using System.Globalization;
using System.Windows.Data;

namespace CurrencyConverter.UI.Converters
{
    public class MultiBooleanToReadonlyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool value1 = default(bool);
            bool value2 = default(bool);

            if (values[0] != null && values[0] is bool)
            {
                value1 = (bool)values[0];
            }

            if (values[1] != null && values[1] is bool)
            {
                value2 = (bool)values[1];
            }

            return value1 && value2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
