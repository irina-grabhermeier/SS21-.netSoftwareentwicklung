using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public class ClassicChessGameRules : IGameRules
    {
        private List<GameRule> _gameRules = new List<GameRule>();

        public ClassicChessGameRules()
        {
            setupGameRules();
        }

        public IReadOnlyList<CellId> ValidMovesForCell(CellId cellId, IChessBoard board)
        {
            var validMoveList = new List<CellId>();

            var source = board.GetCell(cellId);
            for (int row = 0; row < board.NumRows; row++)
            {
                for (int col = 0; col < board.NumCols; col++)
                {
                    // Figure out the status of the cell. 
                    //
                    var target = board.GetCell(CellId.Create(row, col));
                    if (IsValidMove(source, target, board))
                    {
                        validMoveList.Add(CellId.Create(row, col));
                    }
                }
            }
            return validMoveList;
        }

        public bool IsValidMove(CellId source, CellId target, IChessBoard board)
        {
            var sourceCell = board.GetCell(source);
            var targetCell = board.GetCell(target);
            return IsValidMove(sourceCell, targetCell, board);
        }


        public bool IsValidMove(ChessCellModel sourceCell, ChessCellModel targetCell, IChessBoard chessboard)
        {
            // Find all rules that apply to this source cell. 
            //
            var rules = _gameRules.FindAll(x => x.AppliesTo(sourceCell));

            if (rules.Any(x => x.IsInvalidMove(sourceCell, targetCell, chessboard)))
            {
                return false;
            }

            // Check if there is a rule that explicitly allows the move. 
            //
            if (rules.Any(x => x.IsValidMove(sourceCell, targetCell, chessboard)) == true)
            {
                return true;
            }
            return false;
        }

        private void setupGameRules()
        {
            setupGenericRules();
            setupWhitePawnRules();
            setupBlackPawnRules();
            setupKnightRules();
            setupBishopRules();
            setupRookRules();
            setupKingRules();
            setupQueenRules();
        }

        private void setupGenericRules()
        {
            var whiteRules =
                GameRule.Create().Filter(x => x.PieceColor == EChessPieceColor.White);

            whiteRules.InvalidMove((s, t, c) =>
            {
                // Can not move to a field occupied by a white piece, unless it is a king trying to castle.
                //
                return s.PieceType != EChessPieceType.King && t.PieceColor == EChessPieceColor.White;
            });
            _gameRules.Add(whiteRules);

            var blackRules =
                GameRule.Create().Filter(x => x.PieceColor == EChessPieceColor.Black);

            blackRules.InvalidMove((s, t, c) =>
            {
                // Can not move to a field occupied by a black piece.
                //
                return s.PieceType != EChessPieceType.King && t.PieceColor == EChessPieceColor.Black;
            });
            _gameRules.Add(blackRules);

            var genericRules =
                GameRule.Create().Filter(x => true);
            genericRules.InvalidMove((s, t, c) =>
            {
                if (s.RowIndex == t.RowIndex && s.ColIndex == t.ColIndex)
                {
                    return true;
                }

                return false;
            });
            _gameRules.Add(genericRules);
        }

        private void setupWhitePawnRules()
        {
            // White pawn rules. 
            //
            var whitePawnRules =
                GameRule.Create().Filter(x => x.PieceColor == EChessPieceColor.White && x.PieceType == EChessPieceType.Pawn);
            // White pawns 
            //
            whitePawnRules.ValidMove((s, t, c) =>
            {
                // Can only move 1 step forward.
                //
                return (s.RowIndex - t.RowIndex) == 1 && s.ColIndex == t.ColIndex && t.PieceType == EChessPieceType.Empty;
            });
            whitePawnRules.ValidMove((s, t, c) =>
            {
                // Can move 2 steps if it is the first move.
                //
                return s.RowIndex == 6 && (s.RowIndex - t.RowIndex) == 2 && s.ColIndex == t.ColIndex;
            });
            whitePawnRules.ValidMove((s, t, c) =>
            {
                // Can move one step diagonal to beat a black piece. 
                //
                if (t.PieceColor == EChessPieceColor.Black)
                {
                    if (Math.Abs(s.ColIndex - t.ColIndex) == 1 && s.RowIndex - t.RowIndex == 1)
                    {
                        return true;
                    }
                }
                return false;
            });

            _gameRules.Add(whitePawnRules);
        }

        private void setupBlackPawnRules()
        {
            // White pawn rules. 
            //
            var blackPawnRules =
                GameRule.Create().Filter(x => x.PieceColor == EChessPieceColor.Black && x.PieceType == EChessPieceType.Pawn);
            // White pawns 
            //
            blackPawnRules.ValidMove((s, t, c) =>
            {
                // Can only move 1 step forward.
                //
                return (s.RowIndex - t.RowIndex) == -1 && s.ColIndex == t.ColIndex && t.PieceType == EChessPieceType.Empty;
            });
            blackPawnRules.ValidMove((s, t, c) =>
            {
                // Can move 2 steps if it is the first move.
                //
                return s.RowIndex == 1 && (s.RowIndex - t.RowIndex) == -2 && s.ColIndex == t.ColIndex;
            });
            blackPawnRules.ValidMove((s, t, c) =>
            {
                // Can move one step diagonal to beat a black piece. 
                //
                if (t.PieceColor == EChessPieceColor.White)
                {
                    if (Math.Abs(s.ColIndex - t.ColIndex) == 1 && s.RowIndex - t.RowIndex == -1)
                    {
                        return true;
                    }
                }
                return false;
            });
            _gameRules.Add(blackPawnRules);
        }

        private void setupKnightRules()
        {
            var whiteKnightRules =
                GameRule.Create().Filter(x => x.PieceType == EChessPieceType.Knight);

            whiteKnightRules.ValidMove((s, t, c) =>
            {
                int colDiff = Math.Abs(s.ColIndex - t.ColIndex);
                int rowDiff = Math.Abs(s.RowIndex - t.RowIndex);
                // Knights can move either 2 cols and 1 row or two cols and 1 row. 
                //
                if (colDiff == 2 && rowDiff == 1 || colDiff == 1 && rowDiff == 2)
                {
                    return true;
                }

                return false;
            });
            _gameRules.Add(whiteKnightRules);
        }


        private void setupBlackBishopRules()
        {
            var blackBishopRules =
                GameRule.Create().Filter(x => x.PieceColor == EChessPieceColor.Black && x.PieceType == EChessPieceType.Bishop);

            blackBishopRules.InvalidMove((s, t, c) =>
            {
                // Can not move to a field occupied by a black piece.
                //
                return t.PieceColor == EChessPieceColor.Black;
            });

            blackBishopRules.InvalidMove((s, t, c) =>
            {
                // Can not move past another piece. 
                //
                int colDiff = s.ColIndex - t.ColIndex;
                int rowDiff = s.RowIndex - t.RowIndex;

                if (Math.Abs(colDiff) != Math.Abs(rowDiff)) return false; // Check its a diagonal move

                int stepRow = rowDiff > 0 ? -1 : 1;
                int stepCol = colDiff > 0 ? -1 : 1;

                // Check there is no piece on the way to the target. 
                //
                for (int step = 1; t.ColIndex != s.ColIndex + stepCol * step; step++)
                {
                    int row = s.RowIndex + stepRow * step;
                    int col = s.ColIndex + stepCol * step;

                    // We reached the target it is not an invalid move. 
                    //
                    if (row == t.RowIndex || col == t.RowIndex)
                    {
                        return false;
                    }

                    // We reached another piece, but it can not be the target of our move
                    // therefore this move is not valid.
                    //
                    var cell = c.GetCell(CellId.Create(row, col));
                    if (cell.PieceType != EChessPieceType.Empty)
                    {
                        return true;
                    }
                }
                return false;
            });

            blackBishopRules.ValidMove((s, t, c) =>
            {
                // Bishop can move diagonally to take any piece.
                //
                int colDiff = Math.Abs(s.ColIndex - t.ColIndex);
                int rowDiff = Math.Abs(s.RowIndex - t.RowIndex);

                if (colDiff != rowDiff)
                {
                    return false;
                }
                return true;
            });
            _gameRules.Add(blackBishopRules);
        }

        private void setupBishopRules()
        {

            var genericBishopRules =
                GameRule.Create().Filter(x => x.PieceType == EChessPieceType.Bishop);

            genericBishopRules.InvalidMove((s, t, c) =>
            {
                // Can not move to our own position. This would cause an indexing issue. 
                //
                if (s.ColIndex == t.ColIndex && s.RowIndex == t.RowIndex)
                {
                    return true;
                }

                // Can not move past another piece. 
                //
                int colDiff = s.ColIndex - t.ColIndex;
                int rowDiff = s.RowIndex - t.RowIndex;

                if (Math.Abs(colDiff) != Math.Abs(rowDiff)) return false; // Check its a diagonal move

                int stepRow = rowDiff > 0 ? -1 : 1;
                int stepCol = colDiff > 0 ? -1 : 1;

                // Check there is no piece on the way to the target. 
                //
                for (int step = 1; t.ColIndex != s.ColIndex + stepCol * step; step++)
                {
                    int row = s.RowIndex + stepRow * step;
                    int col = s.ColIndex + stepCol * step;

                    // We reached the target it is not an invalid move. 
                    //
                    if (row == t.RowIndex || col == t.ColIndex)
                    {
                        return false;
                    }

                    if (row == c.NumCols - 1 && col == c.NumRows - 1)
                    {
                        return true;
                    }

                    // We reached another piece, but it can not be the target of our move
                    // therefore this move is not valid.
                    //
                    var cell = c.GetCell(CellId.Create(row, col));
                    if (cell.PieceType != EChessPieceType.Empty)
                    {
                        return true;
                    }
                }
                return false;
            });

            genericBishopRules.ValidMove((s, t, c) =>
            {
                // Bishop can move diagonally to take any piece.
                //
                int colDiff = Math.Abs(s.ColIndex - t.ColIndex);
                int rowDiff = Math.Abs(s.RowIndex - t.RowIndex);

                if (colDiff != rowDiff)
                {
                    return false;
                }
                return true;
            });
            _gameRules.Add(genericBishopRules);
        }

        private void setupRookRules()
        {
            var rookRules =
                GameRule.Create().Filter(x =>
                    x.PieceType == EChessPieceType.Rook);

            rookRules.ValidMove((s, t, c) =>
            {
                // Rook can move diagonally or vertically. 
                //
                int colDiff = Math.Abs(s.ColIndex - t.ColIndex);
                int rowDiff = Math.Abs(s.RowIndex - t.RowIndex);

                if (colDiff == 0 || rowDiff == 0)
                {
                    return true;
                }
                return false;
            });

            rookRules.InvalidMove((s, t, c) =>
            {
                // Can not move to our own position. This would cause an indexing issue. 
                //
                if (s.ColIndex == t.ColIndex && s.RowIndex == t.RowIndex)
                {
                    return true;
                }

                // Can not move past another piece. 
                //
                int colDiff = s.ColIndex - t.ColIndex;
                int rowDiff = s.RowIndex - t.RowIndex;

                int stepRow = rowDiff > 0 ? -1 : 1;
                int stepCol = colDiff > 0 ? -1 : 1;

                if (colDiff == 0) stepCol = 0;
                if (rowDiff == 0) stepRow = 0;

                // Check there is no piece on the way to the target. 
                //
                for (int step = 1; ; step++)
                {
                    int row = s.RowIndex + stepRow * step;
                    int col = s.ColIndex + stepCol * step;

                    if (row < 0 || row >= c.NumRows || col < 0 || col >= c.NumCols)
                    {
                        return true;
                    }

                    // We reached the target it is not an invalid move. 
                    //
                    if (row == t.RowIndex && col == t.ColIndex)
                    {
                        return false;
                    }

                    if (row == c.NumCols - 1 && col == c.NumRows - 1)
                    {
                        return true;
                    }

                    // We reached another piece, but it can not be the target of our move
                    // therefore this move is not valid.
                    //
                    var cell = c.GetCell(CellId.Create(row, col));
                    if (cell.PieceType != EChessPieceType.Empty)
                    {
                        return true;
                    }
                }
            });
            _gameRules.Add(rookRules);
        }

        private void setupKingRules()
        {
            var kingRules =
                GameRule.Create().Filter(x =>
                    x.PieceType == EChessPieceType.King);

            kingRules.InvalidMove((s, t, c) =>
            {
                if (t.PieceColor != s.PieceColor)
                {
                    return false;
                }
                
                // Can not move to a piece of the same color unless it is a rook.
                // Some more castle rules are checked below.
                //
                if (t.PieceColor == s.PieceColor && t.PieceType == EChessPieceType.Rook)
                {
                    return false;
                }

                return true;
            });

            kingRules.ValidMove((s, t, c) =>
            {
                int colDiff = Math.Abs(s.ColIndex - t.ColIndex);
                int rowDiff = Math.Abs(s.RowIndex - t.RowIndex);

                return colDiff <= 1 && rowDiff <= 1;
            });

            // Setup up the castle, it is only possible if the king and the rook have not been moved before and 
            // there is no piece between the king and the rook.
            //
            kingRules.ValidMove((s, t, c) =>
            {
                // Check the target is a friendly rook. 
                //
                if (t.PieceType != EChessPieceType.Rook || s.PieceColor != t.PieceColor)
                {
                    return false;
                }
                // Check the rook and the king have not been moved.
                //
                if (c.HasMoved(s.Identifier) || c.HasMoved(t.Identifier))
                {
                    return false;
                }
                // Check there is no piece between the king and the rook.
                //
                int colDiff = s.ColIndex - t.ColIndex;
                int colStep = colDiff > 0 ? -1 : 1;
                for (int step = 1; s.ColIndex + step * colStep != t.ColIndex; step++)
                {
                    int col = s.ColIndex + step * colStep;
                    var cell = c.GetCell(CellId.Create(s.RowIndex, col));
                    if (cell.PieceType != EChessPieceType.Empty)
                    {
                        return false;
                    }
                }

                return true;
            });
            _gameRules.Add(kingRules);
        }

        private void setupQueenRules()
        {
            var queenRules =
                GameRule.Create().Filter(x =>
                    x.PieceType == EChessPieceType.Queen);


            queenRules.InvalidMove((s, t, c) =>
            {
                // Can not move to our own position. This would cause an indexing issue. 
                //
                if (s.ColIndex == t.ColIndex && s.RowIndex == t.RowIndex)
                {
                    return true;
                }

                // Can not move past another piece. 
                //
                int colDiff = s.ColIndex - t.ColIndex;
                int rowDiff = s.RowIndex - t.RowIndex;

                int stepRow = rowDiff > 0 ? -1 : 1;
                int stepCol = colDiff > 0 ? -1 : 1;

                if (colDiff == 0) stepCol = 0;
                if (rowDiff == 0) stepRow = 0;

                // Check there is no piece on the way to the target. 
                //
                for (int step = 1; ; step++)
                {
                    int row = s.RowIndex + stepRow * step;
                    int col = s.ColIndex + stepCol * step;

                    if (row < 0 || row >= c.NumRows || col < 0 || col >= c.NumCols)
                    {
                        return true;
                    }

                    // We reached the target it is not an invalid move. 
                    //
                    if (row == t.RowIndex && col == t.ColIndex)
                    {
                        return false;
                    }

                    if (row == c.NumCols - 1 && col == c.NumRows - 1)
                    {
                        return true;
                    }

                    // We reached another piece, but it can not be the target of our move
                    // therefore this move is not valid.
                    //
                    var cell = c.GetCell(CellId.Create(row, col));
                    if (cell.PieceType != EChessPieceType.Empty)
                    {
                        return true;
                    }
                }
            });

            queenRules.InvalidMove((s, t, c) =>
            {
                // Can not move to our own position. This would cause an indexing issue. 
                //
                if (s.ColIndex == t.ColIndex && s.RowIndex == t.RowIndex)
                {
                    return true;
                }

                // Can not move past another piece. 
                //
                int colDiff = s.ColIndex - t.ColIndex;
                int rowDiff = s.RowIndex - t.RowIndex;

                if (Math.Abs(colDiff) != Math.Abs(rowDiff)) return false; // Check its a diagonal move

                int stepRow = rowDiff > 0 ? -1 : 1;
                int stepCol = colDiff > 0 ? -1 : 1;

                // Check there is no piece on the way to the target. 
                //
                for (int step = 1; t.ColIndex != s.ColIndex + stepCol * step; step++)
                {
                    int row = s.RowIndex + stepRow * step;
                    int col = s.ColIndex + stepCol * step;

                    // We reached the target it is not an invalid move. 
                    //
                    if (row == t.RowIndex || col == t.ColIndex)
                    {
                        return false;
                    }

                    if (row == c.NumCols - 1 && col == c.NumRows - 1)
                    {
                        return true;
                    }

                    // We reached another piece, but it can not be the target of our move
                    // therefore this move is not valid.
                    //
                    var cell = c.GetCell(CellId.Create(row, col));
                    if (cell.PieceType != EChessPieceType.Empty)
                    {
                        return true;
                    }
                }
                return false;
            });

            queenRules.ValidMove((s, t, c) =>
            {
                // Rook can move diagonally or vertically. 
                //
                int colDiff = Math.Abs(s.ColIndex - t.ColIndex);
                int rowDiff = Math.Abs(s.RowIndex - t.RowIndex);

                if (colDiff == 0 || rowDiff == 0)
                {
                    return true;
                }
                return false;
            });

            queenRules.ValidMove((s, t, c) =>
            {
                // Bishop can move diagonally to take any piece.
                //
                int colDiff = Math.Abs(s.ColIndex - t.ColIndex);
                int rowDiff = Math.Abs(s.RowIndex - t.RowIndex);

                if (colDiff != rowDiff)
                {
                    return false;
                }
                return true;
            });

            _gameRules.Add(queenRules);
        }


    }
}
