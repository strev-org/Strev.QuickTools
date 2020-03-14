using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Strev.QuickTools.View.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valueBool = (bool)value;
            var parameterString = parameter.ToString();
            if (parameterString.Contains("|"))
            {
                var colorNames = parameterString.Split(new char[] { '|' }, 2);
                var name = colorNames[valueBool ? 1 : 0];
                var obj = new BrushConverter().ConvertFrom(name);
                return obj;
            }
            throw new ArgumentException("Parameter should exists and contain a pipe");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}