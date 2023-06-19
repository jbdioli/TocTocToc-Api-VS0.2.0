using System;
using System.Globalization;
using Xamarin.Forms;

namespace TocTocToc.Converters;

internal class IsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var returnValue = (bool)value;

        return returnValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}