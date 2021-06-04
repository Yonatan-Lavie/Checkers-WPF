using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using Checkers.Models;
namespace Checkers.Converters
{
    public class BorderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var BorderColor = (BorderColor)value;
            switch (BorderColor)
            {
                case BorderColor.NoneLighted:
                    return Brushes.Black;
                case BorderColor.LightedAvailable:
                    return Brushes.Green;
                case BorderColor.LightedSelected:
                    return Brushes.Red;
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
