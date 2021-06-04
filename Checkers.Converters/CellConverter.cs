using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Checkers.Converters
{
    public class CellConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cellColor = (CellColor)value;
            switch (cellColor)
            {
                case CellColor.White:
                    return Brushes.WhiteSmoke;
                case CellColor.Black:
                    return Brushes.DarkGray;
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
