using System;
using System.Globalization;
using TocTocToc.Resx;
using Xamarin.Forms;

namespace TocTocToc.Converters
{
    public class AddressTitleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //var text =AppResources.ResourceManager.GetObject("About");

            var isEditing = (bool)value;
            var title = AppResources.LabelTitleAddAddress;

            if (isEditing)
                title = AppResources.LabelTitleEditAddress;


            return title;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new object();
        }
    }
}