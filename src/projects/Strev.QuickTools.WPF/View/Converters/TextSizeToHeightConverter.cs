using Strev.QuickTools.DomainModel.Enumeration;
using Strev.QuickTools.View.UserControls;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Strev.QuickTools.View.Converters
{
    public class TextSizeToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueEnum = (TextSizeType)value;
            return Helpers.GetControlHeightByTextSize(valueEnum);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}