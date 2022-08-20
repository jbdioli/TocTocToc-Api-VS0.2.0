using System;
using System.Globalization;
using Xamarin.Forms;

namespace TocTocToc.Converters
{
    class IsVisibleFromStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = value != null;

            return isVisible.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
