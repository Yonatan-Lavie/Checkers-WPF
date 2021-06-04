using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Checkers.Models
{
    public class BoardModel : BindableBase
    {
        #region Fields
        private ObservableCollection<CellModel> _gameBoard;
        private CellModel _selectedPiece;
        private List<CellModel> _availableCells;
        private PlayerModel _playerOne;
        private PlayerModel _playerTwo;
        private PlayerModel _currentPlayer;
        #endregion

        #region Properties
        public ObservableCollection<CellModel> GameBoard
        {
            get { return _gameBoard; }
            set { SetProperty(ref _gameBoard, value); }
        }
        public CellModel SelectedPiece
        {
            get { return _selectedPiece; }
            set { SetProperty(ref _selectedPiece, value); }
        }
        public List<CellModel> AvailableCells
        {
            get { return _availableCells; }
            set { SetProperty(ref _availableCells, value); }
        }
        public PlayerModel PlayerOne
        {
            get { return _playerOne; }
            set { SetProperty(ref _playerOne, value); }
        }
        public PlayerModel PlayerTwo
        {
            get { return _playerTwo; }
            set { SetProperty(ref _playerTwo, value); }
        }
        public PlayerModel CurrentPlayer
        {
            get { return _currentPlayer; }
            set { SetProperty(ref _currentPlayer, value); }
        }

        #endregion

        public BoardModel()
        {

        }
        //public BoardModel(string playerOneName, string PlayerTwoName)
        //{
        //    this._gameBoard = new ObservableCollection<CellModel>();
        //    this.PlayerOne = new PlayerModel(playerOneName, PieceColor.White);
        //    this.PlayerTwo = new PlayerModel(PlayerTwoName, PieceColor.Black);
        //    for (int i = 0; i < 8; i++)
        //    {
        //        for (int j = 0; j < 8; j++)
        //        {
        //            CellModel c = new CellModel();
        //            c.Row = i;
        //            c.Col = j;
        //            c.Color = (i + j) % 2 == 0 ? CellColor.White : CellColor.Black;
        //            c.Piece = null;
        //            if (i < 3 && (j + i) % 2 != 0)
        //            {
        //                c.Piece = new PieceModel()
        //                {
        //                    PieceColor = PieceColor.Black,
        //                    PieceType = PieceType.Soldier
        //                };
        //            }
        //            if (4 < i && (j + i) % 2 != 0)
        //            {
        //                c.Piece = new PieceModel()
        //                {
        //                    PieceColor = PieceColor.White,
        //                    PieceType = PieceType.Soldier
        //                };
        //            }
        //            GameBoard.Add(c);
        //        }
        //    }

        //}
    }
}
