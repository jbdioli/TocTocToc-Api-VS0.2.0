using System;
using System.Globalization;
using Xamarin.Forms;

namespace TocTocToc.Converters;

public class IsChangeColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var valueType = (string)parameter;
        if (string.IsNullOrEmpty(valueType))
            throw new Exception("[ ERROR in module IsChangeConverter ] : ConvertParameter can't be null");

        var isValid = value switch
        {
            string s => !string.IsNullOrEmpty(s),
            bool b => b,
            _ => false
        };


        var color = valueType switch
        {
            "TextError" => isValid ? Color.Black : Color.Red,
            "BorderActivePurple" => isValid ? Color.FromHex("FF00FF50") : Color.FromHex("61007D"), // Violet clair - Violet
            "BorderActive" => isValid ? Color.Green : Color.FromHex("61007D"),
            "BorderPause" => isValid ? Color.DarkOrange : Color.FromHex("61007D"),
            "BackgroundPause" => isValid ? Color.DarkOrange : Color.MediumPurple,
            "BorderPayed" => isValid ? Color.Green : Color.DarkMagenta,
            _ => Color.Black
        };

        return color;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new Exception("[ ConvertBack in module IsChangeColorConverter ] : is not implemented");
    }
}