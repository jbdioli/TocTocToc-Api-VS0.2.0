using System;
using System.Globalization;
using Xamarin.Forms;

namespace TocTocToc.Converters
{
    internal class IsVisibleConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = false;

            if (value == null) return false;

            var type = value.GetType();

            if (type == typeof(bool))
                isVisible = (bool)value;

            if (type == typeof(string))
            {
                isVisible = true;
            }

            return isVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
