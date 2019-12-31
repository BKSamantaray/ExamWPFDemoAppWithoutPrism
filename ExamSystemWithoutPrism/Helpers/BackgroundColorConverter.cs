using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ExamSystemWithoutPrism.Helpers
{
    public class BackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush backGroundBrush = Brushes.Transparent;
            if (value != null && value is bool)
            {
                backGroundBrush = (bool)value ? Brushes.Green : Brushes.Red;
            }
            return backGroundBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
