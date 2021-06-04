using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Checkers.Converters
{
    public class PieceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Piece = (PieceModel)value;

            if (Piece == null)
                return null;
            if (Piece.PieceColor == PieceColor.Black)
            {
                if (Piece.PieceType == PieceType.Soldier)
                {
                    return Brushes.Black;
                }
                else
                {
                    return Brushes.Blue;
                }
            }
            else
            {
                if (Piece.PieceType == PieceType.Soldier)
                {
                    return Brushes.White;
                }
                else
                {
                    return Brushes.Green;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
