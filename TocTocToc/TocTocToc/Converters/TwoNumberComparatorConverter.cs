using System;
using System.Globalization;
using TocTocToc.Shared;
using Xamarin.Forms;

namespace TocTocToc.Converters;

internal class TwoNumberComparatorConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var color = Color.Black;

        if (values is not { Length: > 0 }) return color;

        var isGreater = NumberHandling.IsMiniGreaterThan(values[0], values[1]);

        if (isGreater)
            color = Color.Red;

        return color;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}