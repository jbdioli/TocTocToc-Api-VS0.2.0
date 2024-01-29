using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace TocTocToc.Converters;

public class IsEnabledConverter: IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {

        foreach (var value in values)
        {
            if (value is bool and false)
            {
                return false;
            }
        }

        return true;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return null;
    }
}