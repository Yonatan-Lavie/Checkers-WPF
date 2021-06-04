using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class CellModel : BindableBase
    {
        #region Fields
        private int _col;
        private int _row;
        private CellColor _color;
        private BorderColor _borderColor = BorderColor.NoneLighted;
        private PieceModel _piece;
        #endregion

        #region Properties
        public int Col
        {
            get { return _col; }
            set { SetProperty(ref _col, value); }
        }
        public int Row
        {
            get { return _row; }
            set { SetProperty(ref _row, value); }
        }
        public CellColor Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }
        public BorderColor BorderColor
        {
            get { return _borderColor; }
            set { SetProperty(ref _borderColor, value); }
        }
        public PieceModel Piece
        {
            get { return _piece; }
            set { SetProperty(ref _piece, value); }
        }
        #endregion

    }
    #region Enums
    public enum CellColor
    {
        White,
        Black,
    }
    public enum BorderColor
    {
        NoneLighted,
        LightedAvailable,
        LightedSelected,

    }
    #endregion
}
