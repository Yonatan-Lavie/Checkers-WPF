using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class PieceModel : BindableBase
    {
        #region Fields
        private PieceColor _pieceColor;
        private PieceType _pieceType;
        #endregion

        #region Properties
        public PieceType PieceType
        {
            get { return _pieceType; }
            set { SetProperty(ref _pieceType, value); }
        }
        public PieceColor PieceColor
        {
            get { return _pieceColor; }
            set { SetProperty(ref _pieceColor, value); }
        }
        #endregion

    }
    #region Enums
    public enum PieceType
    {
        King,
        Soldier,
    }
    public enum PieceColor
    {
        White,
        Black,
    }
    #endregion

}
