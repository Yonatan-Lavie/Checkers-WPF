using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class PlayerModel : BindableBase
    {
        #region Fields
        private string _name;
        private PieceColor _color;
        private PlayerAte _playerAte;
        private int _numberOfPieces;
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public PieceColor Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }
        public PlayerAte PlayerAte
        {
            get { return _playerAte; }
            set { SetProperty(ref _playerAte, value); }
        }
        public int NumberOfPieces
        {
            get { return _numberOfPieces; }
            set { SetProperty(ref _numberOfPieces, value); }
        }
        #endregion

        public PlayerModel(string name, PieceColor color)
        {
            _numberOfPieces = 12;
            _playerAte = PlayerAte.No;
            _name = name;
            _color = color;
        }

    }
    public enum PlayerAte
    {
        Yes,
        No,
    }
}
