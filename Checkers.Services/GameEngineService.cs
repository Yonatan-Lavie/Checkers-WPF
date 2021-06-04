using Checkers.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Diagnostics;

namespace Checkers.Services
{
    public class GameEngineService
    {
        public BoardModel GameInit(string playerOneName, string PlayerTwoName)
        {
            BoardModel Board = new BoardModel();
            Board.GameBoard = new ObservableCollection<CellModel>();
            Board.PlayerOne = new PlayerModel(playerOneName, PieceColor.White);
            Board.PlayerTwo = new PlayerModel(PlayerTwoName, PieceColor.Black);
            Board.CurrentPlayer = Board.PlayerOne;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    CellModel c = new CellModel();
                    c.Row = i;
                    c.Col = j;
                    c.Color = (i + j) % 2 == 0 ? CellColor.White : CellColor.Black;
                    c.Piece = null;
                    if (i < 3 && (j + i) % 2 != 0)
                    {
                        c.Piece = new PieceModel()
                        {
                            PieceColor = PieceColor.Black,
                            PieceType = PieceType.Soldier
                        };
                    }
                    if (4 < i && (j + i) % 2 != 0)
                    {
                        c.Piece = new PieceModel()
                        {
                            PieceColor = PieceColor.White,
                            PieceType = PieceType.Soldier
                        };
                    }
                    Board.GameBoard.Add(c);
                }
            }
            
            return Board;
        }
        
        public void CellSelected(ref BoardModel board, CellModel c)
        {
            //ClearHilightedCells(ref board);
            if (PieceExistInCell(c) == true)
            {
                if(AetFlageIsOn(board.CurrentPlayer))
                {
                    return;
                }
                else
                {
                    if(PieceBelongsToCurrentPlayer(board.CurrentPlayer, c))
                    {
                        ClearHilightedCells(ref board);
                        if (PieceIsKing(c))
                        {
                            MarkAvailableCellsForKing(ref board, c);
                        }
                        else 
                        {
                            MarkAcailableCellForSoldier(ref board, c);
                        }
                        board.SelectedPiece = c;
                    }
                    return;
                }
            }
            else
            {
                if(!CellIsAvailable(board.AvailableCells, c))
                {
                    return;
                }
                else
                {
                    Move(ref board, board.SelectedPiece, c);
                    if(PieceGetTheLastRow(board.CurrentPlayer, c))
                    {
                        UpgradeToKing(ref board, c);
                    }
                    if (AetFlageIsOn(board.CurrentPlayer))
                    {
                        board.SelectedPiece = board.GameBoard[c.Row * 8 + c.Col];
                        ClearHilightedCells(ref board);
                        if(CheckWining(ref board))
                        {
                            Debug.WriteLine(board.CurrentPlayer.Name + "Win!!");
                        }
                        if (PieceIsKing(board.SelectedPiece))
                        {
                            MarkAvailableCellsForKing(ref board, board.SelectedPiece);
                        }
                        else
                        {
                            MarkAcailableCellForSoldier(ref board, board.SelectedPiece);
                        }
                        if (AvailableCellsExist(board.AvailableCells))
                        {
                            return;
                        }
                        else
                        {
                            EndTurn(ref board);
                        }
                    }
                    else
                    {
                        EndTurn(ref board);
                    }
                }
            }
        }

        public bool CheckWining(ref BoardModel board)
        {
            if(board.CurrentPlayer.Color == PieceColor.Black)
            {
                if (board.PlayerOne.NumberOfPieces == 0)
                    return true;
                else
                {
                    return false;
                }
            }
            else
            {
                if (board.PlayerTwo.NumberOfPieces == 0)
                    return true;
                else
                {
                    return false;
                }
            }
        }
        public bool PieceExistInCell(CellModel c)
        {
            return c.Piece != null ? true : false;
        }
        public bool AetFlageIsOn(PlayerModel p)
        {
            return p.PlayerAte == PlayerAte.Yes ? true : false;
        }
        public bool PieceBelongsToCurrentPlayer(PlayerModel currentPlayer, CellModel c)
        {
            return currentPlayer.Color == c.Piece.PieceColor ? true : false;
        }
        public bool CellIsAvailable(List<CellModel> availableCells, CellModel c)
        {
            var res = availableCells.Any(cell => cell.Col == c.Col && cell.Row == c.Row);
            return res;
        }
        public bool PieceGetTheLastRow(PlayerModel currentPlayer, CellModel c)
        {
            if(currentPlayer.Color == PieceColor.Black)
            {
                return c.Row == 7 ? true : false;
            }
            else
            {
                return c.Row == 0 ? true : false;
            }
        }
        public bool PieceIsKing(CellModel c)
        {
            return c.Piece.PieceType == PieceType.King ? true : false;
        }
        public bool AvailableCellsExist(List<CellModel> availableCells)
        {
            return availableCells.Count > 0 ? true : false;
        }
        public void Move(ref BoardModel board, CellModel src, CellModel dst)
        {
            var RowDelta = Math.Abs(src.Row - dst.Row);

                if (RowDelta < 2)
                {
                    // Move Action
                    board.GameBoard[dst.Row * 8 + dst.Col].Piece = board.GameBoard[src.Row * 8 + src.Col].Piece;
                    board.GameBoard[src.Row * 8 + src.Col].Piece = null;
                }
                else
                {
                    if (dst.Row - src.Row < 0 && dst.Col - src.Col < 0)
                    {
                        if (board.GameBoard[(dst.Row + 1) * 8 + (dst.Col + 1)].Piece != null)
                        {
                            // Eat
                            board.GameBoard[dst.Row * 8 + dst.Col].Piece = board.GameBoard[src.Row * 8 + src.Col].Piece;
                            board.GameBoard[(dst.Row + 1) * 8 + (dst.Col + 1)].Piece = null;
                            board.GameBoard[src.Row * 8 + src.Col].Piece = null;

                            if (board.CurrentPlayer.Color != board.PlayerOne.Color)
                            {
                                board.PlayerOne.NumberOfPieces--;
                                //Check For Winning
                            }
                            else
                            {
                                board.PlayerTwo.NumberOfPieces--;
                                //Check For Winning
                            }
                            board.CurrentPlayer.PlayerAte = PlayerAte.Yes;
                        }
                        else
                        {
                            // Move
                            board.GameBoard[dst.Row * 8 + dst.Col].Piece = board.GameBoard[src.Row * 8 + src.Col].Piece;
                            board.GameBoard[src.Row * 8 + src.Col].Piece = null;
                        }
                    }
                    if (dst.Row - src.Row > 0 && dst.Col - src.Col < 0)
                    {
                        if (board.GameBoard[(dst.Row - 1) * 8 + (dst.Col + 1)].Piece != null)
                        {
                            // King Eat
                            board.GameBoard[dst.Row * 8 + dst.Col].Piece = board.GameBoard[src.Row * 8 + src.Col].Piece;
                            board.GameBoard[(dst.Row - 1) * 8 + (dst.Col + 1)].Piece = null;
                            board.GameBoard[src.Row * 8 + src.Col].Piece = null;

                            if (board.CurrentPlayer.Color != board.PlayerOne.Color)
                            {
                                board.PlayerOne.NumberOfPieces--;
                                //Check For Winning
                            }
                            else
                            {
                                board.PlayerTwo.NumberOfPieces--;
                                //Check For Winning
                            }
                            board.CurrentPlayer.PlayerAte = PlayerAte.Yes;
                        }
                        else
                        {
                            // Move
                            board.GameBoard[dst.Row * 8 + dst.Col].Piece = board.GameBoard[src.Row * 8 + src.Col].Piece;
                            board.GameBoard[src.Row * 8 + src.Col].Piece = null;
                        }
                    }
                    if (dst.Row - src.Row < 0 && dst.Col - src.Col > 0)
                    {
                        if (board.GameBoard[(dst.Row + 1) * 8 + (dst.Col - 1)].Piece != null)
                        {
                            // King Eat
                            board.GameBoard[dst.Row * 8 + dst.Col].Piece = board.GameBoard[src.Row * 8 + src.Col].Piece;
                            board.GameBoard[(dst.Row + 1) * 8 + (dst.Col - 1)].Piece = null;
                            board.GameBoard[src.Row * 8 + src.Col].Piece = null;

                            if (board.CurrentPlayer.Color != board.PlayerOne.Color)
                            {
                                board.PlayerOne.NumberOfPieces--;
                                //Check For Winning
                            }
                            else
                            {
                                board.PlayerTwo.NumberOfPieces--;
                                //Check For Winning
                            }
                            board.CurrentPlayer.PlayerAte = PlayerAte.Yes;
                        }
                        else
                        {
                            // Move
                            board.GameBoard[dst.Row * 8 + dst.Col].Piece = board.GameBoard[src.Row * 8 + src.Col].Piece;
                            board.GameBoard[src.Row * 8 + src.Col].Piece = null;
                        }
                    }
                    if (dst.Row - src.Row > 0 && dst.Col - src.Col > 0)
                    {
                        if (board.GameBoard[(dst.Row - 1) * 8 + (dst.Col - 1)].Piece != null)
                        {
                            // King Eat
                            board.GameBoard[dst.Row * 8 + dst.Col].Piece = board.GameBoard[src.Row * 8 + src.Col].Piece;
                            board.GameBoard[(dst.Row - 1) * 8 + (dst.Col - 1)].Piece = null;
                            board.GameBoard[src.Row * 8 + src.Col].Piece = null;

                            if (board.CurrentPlayer.Color != board.PlayerOne.Color)
                            {
                                board.PlayerOne.NumberOfPieces--;
                                //Check For Winning
                            }
                            else
                            {
                                board.PlayerTwo.NumberOfPieces--;
                                //Check For Winning
                            }
                            board.CurrentPlayer.PlayerAte = PlayerAte.Yes;
                        }
                        else
                        {
                            // Move
                            board.GameBoard[dst.Row * 8 + dst.Col].Piece = board.GameBoard[src.Row * 8 + src.Col].Piece;
                            board.GameBoard[src.Row * 8 + src.Col].Piece = null;
                        }
                    }
                }


        }
        public void ClearHilightedCells(ref BoardModel board)
        {
            board.GameBoard.All(x => { x.BorderColor = BorderColor.NoneLighted; return true; });
            board.AvailableCells = new List<CellModel>();
        }
        public void ResetAvailableCells(ref BoardModel board)
        {
            board.AvailableCells = new List<CellModel>();
        }
        public void UpgradeToKing(ref BoardModel board, CellModel c)
        {
            //board.GameBoard[c.Row * 8 + c.Col].Piece.PieceType = PieceType.King;
            board.GameBoard[c.Row * 8 + c.Col].Piece = new PieceModel() { PieceColor = c.Piece.PieceColor, PieceType = PieceType.King };
        }
        public void EndTurn(ref BoardModel board)
        {
            board.CurrentPlayer.PlayerAte = PlayerAte.No;
            ClearHilightedCells(ref board);
            board.AvailableCells = new List<CellModel>();
            board.CurrentPlayer = board.CurrentPlayer.Color == PieceColor.Black ? board.PlayerOne : board.PlayerTwo;
        }
        public void MarkAcailableCellForSoldier(ref BoardModel board, CellModel c)
        {
            int MoveDirection = board.CurrentPlayer.Color == PieceColor.Black ? 1 : -1;
            CellModel dst;
            CellModel middel;
            bool CheckEatingRight = c.Row + 2 * MoveDirection <= 7 && c.Row + 2 * MoveDirection >= 0 && c.Col + 2 <= 7 ? true : false;
            bool CheckEatingLeft = c.Row + 2 * MoveDirection <= 7 && c.Row + 2 * MoveDirection >= 0 && c.Col - 2  >= 0 ? true : false;
            bool CheckMoveRight = c.Row + 1 * MoveDirection <= 7 && c.Row + 1 * MoveDirection >= 0 && c.Col + 1 <= 7 ? true : false;
            bool CheckMoveLeft = c.Row + 1 * MoveDirection <= 7 && c.Row + 1 * MoveDirection >= 0 && c.Col - 1 >= 0 ? true : false;
            bool CheckEatingLeftBackWord = false;
            bool CheckEatingRightBackWord = false;
            
            if (board.CurrentPlayer.PlayerAte == PlayerAte.Yes)
            {
                CheckEatingRightBackWord = c.Row + 2 * MoveDirection <= 7 && c.Row + 2 * -1 * MoveDirection >= 0 && c.Col + 2 <= 7 ? true : false;
                CheckEatingLeftBackWord = c.Row + 2 * MoveDirection <= 7 && c.Row + 2 * -1 * MoveDirection >= 0 && c.Col - 2 >= 0 ? true : false;
                CheckMoveRight = false;
                CheckMoveLeft = false;
            }
            if (CheckEatingRightBackWord)
            {
                dst = board.GameBoard[(c.Row + 2 * -1 * MoveDirection) * 8 + c.Col + 2];
                middel = board.GameBoard[(c.Row + 1 * -1 * MoveDirection) * 8 + c.Col + 1]; ;
                if (middel.Piece != null && dst.Piece == null)
                {
                    if (middel.Piece.PieceColor != c.Piece.PieceColor)
                    {
                        // Piece can eat to the right cell
                        board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                        board.AvailableCells.Add(dst);
                        CheckMoveRight = false;
                        CheckMoveLeft = false;
                    }
                }
            }
            if (CheckEatingLeftBackWord)
            {
                dst = board.GameBoard[(c.Row + 2 * -1 * MoveDirection) * 8 + c.Col - 2];
                middel = board.GameBoard[(c.Row + 1 * -1 * MoveDirection) * 8 + c.Col - 1];
                if (middel.Piece != null && dst.Piece == null)
                {
                    if (middel.Piece.PieceColor != c.Piece.PieceColor)
                    {
                        // Piece can eat to the left cell
                        board.AvailableCells.Add(dst);
                        board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                        CheckMoveRight = false;
                        CheckMoveLeft = false;
                    }

                }
            }
            if (CheckEatingRight)
            {
                dst = board.GameBoard[(c.Row +  2 * MoveDirection) * 8 + c.Col + 2];
                middel = board.GameBoard[(c.Row + 1 * MoveDirection) * 8 + c.Col + 1]; ;
                if (middel.Piece != null && dst.Piece == null)
                {
                    if (middel.Piece.PieceColor != c.Piece.PieceColor)
                    {
                        // Piece can eat to the right cell
                        board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                        board.AvailableCells.Add(dst);
                        CheckMoveRight = false;
                        CheckMoveLeft = false;
                    }
                }
            }
            if (CheckEatingLeft)
            {
                dst = board.GameBoard[(c.Row + 2 * MoveDirection) * 8 + c.Col - 2];
                middel = board.GameBoard[(c.Row + 1 * MoveDirection) * 8 + c.Col - 1];
                if (middel.Piece != null && dst.Piece == null)
                {
                    if (middel.Piece.PieceColor != c.Piece.PieceColor)
                    {
                        // Piece can eat to the left cell
                        board.AvailableCells.Add(dst);
                        board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                        CheckMoveRight = false;
                        CheckMoveLeft = false;
                    }

                }
            }
            if (CheckMoveRight)
            {
                dst = board.GameBoard[(c.Row + 1 * MoveDirection) * 8 + c.Col + 1];
                if (dst.Piece == null)
                {
                    // Piece can move to the right cell
                    board.AvailableCells.Add(dst);
                    board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                }
            }
            if (CheckMoveLeft)
            {
                dst = board.GameBoard[(c.Row + 1 * MoveDirection) * 8 + c.Col - 1];
                if (dst.Piece == null)
                {
                    // Piece can move to the left cell
                    board.AvailableCells.Add(dst);
                    board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                }
            }
        }
        public void MarkAvailableCellsForKing(ref BoardModel board, CellModel c)
        {
            bool CheckRightMoveForword;
            bool CheckRightMoveBackword;
            bool CheckLeftMoveForword;
            bool CheckLeftMoveBackword;
            bool MustEatFlag = false;
            int MoveCellsAdded = 0;
            CellModel dst;
            CellModel middel;

            if(board.CurrentPlayer.PlayerAte == PlayerAte.Yes)
            {
                MustEatFlag = true;
            }


            //CheckRightMoveForword
            for (int i = 1; i < 8; i++)
            {
                CheckRightMoveForword = c.Row + i <= 7 && c.Col + i <= 7 ? true : false;
                if (CheckRightMoveForword)
                {
                    dst = board.GameBoard[(c.Row + i) * 8 + c.Col + i];
                    if (dst.Piece == null)
                    {
                        if (!MustEatFlag)
                        {
                            // Kink cen Move co this cell
                            board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                            board.AvailableCells.Add(dst);
                            MoveCellsAdded++;
                        }

                    }
                    else
                    {
                        if(c.Row + (i + 1) <= 7 && c.Col + (i + 1) <= 7 && dst.Piece.PieceColor != board.CurrentPlayer.Color)
                        {
                            middel = board.GameBoard[(c.Row + i) * 8 + c.Col + i];
                            dst = board.GameBoard[(c.Row + (i + 1)) * 8 + c.Col + (i + 1)];
                            if (dst.Piece == null)
                            {
                                // King need to eat
                                
                                if(MoveCellsAdded > 0 && MustEatFlag)
                                {
                                    ClearHilightedCells(ref board);
                                    board.AvailableCells.RemoveRange(board.AvailableCells.Count - MoveCellsAdded, MoveCellsAdded);
                                    MoveCellsAdded = 0;
                                }
                                board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                                board.AvailableCells.Add(dst);
                                MustEatFlag = true;
                            }
                        }
                        break;
                    }
                }
            }
            //CheckRightMoveBackword
            for (int i = 1; i < 8; i++)
            {
                CheckRightMoveBackword = c.Row - i >= 0 && c.Col + i <= 7 ? true : false;
                if (CheckRightMoveBackword)
                {
                    dst = board.GameBoard[(c.Row - i) * 8 + c.Col + i];
                    if (dst.Piece == null)
                    {
                        if (!MustEatFlag)
                        {
                            // Kink cen Move co this cell
                            board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                            board.AvailableCells.Add(dst);
                            MoveCellsAdded++;
                        }

                    }
                    else
                    {
                        if (c.Row - (i + 1) >= 0 && c.Col + (i + 1) <= 7 && dst.Piece.PieceColor != board.CurrentPlayer.Color)
                        {
                            dst = board.GameBoard[(c.Row - (i + 1)) * 8 + c.Col + (i + 1)];
                            if (dst.Piece == null)
                            {
                                // King need to eat
                                
                                if (MoveCellsAdded > 0 && MustEatFlag)
                                {
                                    ClearHilightedCells(ref board);
                                    board.AvailableCells.RemoveRange(board.AvailableCells.Count - MoveCellsAdded, MoveCellsAdded);
                                    MoveCellsAdded = 0;
                                }
                                board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                                board.AvailableCells.Add(dst);
                                MustEatFlag = true;

                            }
                        }
                        break;
                    }
                }

            }
            //CheckLeftMoveForword
            for (int i = 1; i < 8; i++)
            {
                CheckLeftMoveForword = c.Row + i <= 7 && c.Col - i >= 0 ? true : false;
                if (CheckLeftMoveForword)
                {
                    dst = board.GameBoard[(c.Row + i) * 8 + c.Col - i];
                    if (dst.Piece == null)
                    {
                        if (!MustEatFlag)
                        {
                            // Kink cen Move co this cell
                            board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                            board.AvailableCells.Add(dst);
                            MoveCellsAdded++;
                        }

                    }
                    else
                    {
                        if (c.Row + (i + 1) <= 7 && c.Col - (i + 1) >= 0 && dst.Piece.PieceColor != board.CurrentPlayer.Color)
                        {
                            dst = board.GameBoard[(c.Row + (i + 1)) * 8 + c.Col - (i + 1)];
                            if (dst.Piece == null)
                            {
                                // King need to eat
                                
                                if (MoveCellsAdded > 0 && MustEatFlag)
                                {
                                    ClearHilightedCells(ref board);
                                    board.AvailableCells.RemoveRange(board.AvailableCells.Count - MoveCellsAdded, MoveCellsAdded);
                                    MoveCellsAdded = 0;
                                }
                                board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                                board.AvailableCells.Add(dst);
                                MustEatFlag = true;

                            }
                        }
                        break;
                    }
                }
            }
            //CheckLeftMoveBackword
            for (int i = 1; i < 8; i++)
            {
                CheckLeftMoveBackword = c.Row - i >= 0 && c.Col - i >= 0 ? true : false;
                if (CheckLeftMoveBackword)
                {
                    dst = board.GameBoard[(c.Row - i) * 8 + c.Col - i];
                    if (dst.Piece == null)
                    {
                        if (!MustEatFlag)
                        {
                            // Kink cen Move co this cell
                            board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                            board.AvailableCells.Add(dst);
                            MoveCellsAdded++;
                        }

                    }
                    else
                    {
                        if (c.Row - (i + 1) >= 0 && c.Col - (i + 1) >= 0 && dst.Piece.PieceColor != board.CurrentPlayer.Color)
                        {
                            dst = board.GameBoard[(c.Row - (i + 1)) * 8 + c.Col - (i + 1)];
                            if (dst.Piece == null)
                            {
                                // King need to eat
                                
                                if (MoveCellsAdded > 0 && MustEatFlag)
                                {
                                    ClearHilightedCells(ref board);
                                    board.AvailableCells.RemoveRange(board.AvailableCells.Count - MoveCellsAdded, MoveCellsAdded);
                                    MoveCellsAdded = 0;
                                }
                                board.GameBoard[dst.Row * 8 + dst.Col].BorderColor = BorderColor.LightedAvailable;
                                board.AvailableCells.Add(dst);
                                MustEatFlag = true;
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}
