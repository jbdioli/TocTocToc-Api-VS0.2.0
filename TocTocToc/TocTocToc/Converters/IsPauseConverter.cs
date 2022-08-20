using System;
using System.Globalization;
using Xamarin.Forms;

namespace TocTocToc.Converters;

public class IsPauseConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var color = "#61007D";

        if ((bool)value)
            color = "#FF00FF50";

        return color;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}