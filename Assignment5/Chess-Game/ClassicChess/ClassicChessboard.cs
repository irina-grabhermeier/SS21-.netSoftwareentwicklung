using System;
using System.Collections.Generic;

namespace Exercise_2
{
    public class ClassicChessboard : IChessBoard
    {
        private Dictionary<CellId, ChessCellModel> _cells;
        private IGameRules _gameRules;
        private MoveHistory _moveHistory;

        public int NumRows
        {
            get { return 8; }
        }

        public int NumCols
        {
            get { return 8; }
        }

        public event EventHandler<CellStatusChangedEventArgs> CellStatusChanged = delegate { };

        public ClassicChessboard(IGameRules gameRules)
        {
            _gameRules = gameRules;
            _moveHistory = new MoveHistory();
        }


        private CellId keyFor(int row, int col)
        {
            return CellId.Create(row, col);
        }

        public void SetPiece(CellId identifier, EChessPieceType piece, EChessPieceColor color)
        {
            EChessPieceType oldPiece = _cells[identifier].PieceType;
            EChessPieceColor oldColor = _cells[identifier].PieceColor;

            _cells[identifier].SetPiece(piece, color);
            CellStatusChanged(this, new CellStatusChangedEventArgs(identifier, oldPiece, oldColor, piece, color));
        }

        public void SetPiece(int row, int col, EChessPieceType piece, EChessPieceColor color)
        {
            SetPiece(keyFor(row, col), piece, color);
        }

        public void InitializeBoard()
        {
            _cells = new Dictionary<CellId, ChessCellModel>();
            for (int row = 0; row < NumRows; row++)
            {
                for (int col = 0; col < NumCols; col++)
                {
                    _cells.Add(keyFor(row, col), new ChessCellModel(row, col));
                }
            }
            SetupBoard();
        }

        public void SetupBoard()
        {
            // Initialize Pieces on the Board. 
            //
            SetPiece(0, 0, EChessPieceType.Rook, EChessPieceColor.Black);
            SetPiece(0, 1, EChessPieceType.Knight, EChessPieceColor.Black);
            SetPiece(0, 2, EChessPieceType.Bishop, EChessPieceColor.Black);
            SetPiece(0, 3, EChessPieceType.Queen, EChessPieceColor.Black);
            SetPiece(0, 4, EChessPieceType.King, EChessPieceColor.Black);
            SetPiece(0, 5, EChessPieceType.Bishop, EChessPieceColor.Black);
            SetPiece(0, 6, EChessPieceType.Knight, EChessPieceColor.Black);
            SetPiece(0, 7, EChessPieceType.Rook, EChessPieceColor.Black);
            SetPiece(1, 0, EChessPieceType.Pawn, EChessPieceColor.Black);
            SetPiece(1, 1, EChessPieceType.Pawn, EChessPieceColor.Black);
            SetPiece(1, 2, EChessPieceType.Pawn, EChessPieceColor.Black);
            SetPiece(1, 3, EChessPieceType.Pawn, EChessPieceColor.Black);
            SetPiece(1, 4, EChessPieceType.Pawn, EChessPieceColor.Black);
            SetPiece(1, 5, EChessPieceType.Pawn, EChessPieceColor.Black);
            SetPiece(1, 6, EChessPieceType.Pawn, EChessPieceColor.Black);
            SetPiece(1, 7, EChessPieceType.Pawn, EChessPieceColor.Black);

            SetPiece(7, 0, EChessPieceType.Rook, EChessPieceColor.White);
            SetPiece(7, 1, EChessPieceType.Knight, EChessPieceColor.White);
            SetPiece(7, 2, EChessPieceType.Bishop, EChessPieceColor.White);
            SetPiece(7, 3, EChessPieceType.Queen, EChessPieceColor.White);
            SetPiece(7, 4, EChessPieceType.King, EChessPieceColor.White);
            SetPiece(7, 5, EChessPieceType.Bishop, EChessPieceColor.White);
            SetPiece(7, 6, EChessPieceType.Knight, EChessPieceColor.White);
            SetPiece(7, 7, EChessPieceType.Rook, EChessPieceColor.White);
            SetPiece(6, 0, EChessPieceType.Pawn, EChessPieceColor.White);
            SetPiece(6, 1, EChessPieceType.Pawn, EChessPieceColor.White);
            SetPiece(6, 2, EChessPieceType.Pawn, EChessPieceColor.White);
            SetPiece(6, 3, EChessPieceType.Pawn, EChessPieceColor.White);
            SetPiece(6, 4, EChessPieceType.Pawn, EChessPieceColor.White);
            SetPiece(6, 5, EChessPieceType.Pawn, EChessPieceColor.White);
            SetPiece(6, 6, EChessPieceType.Pawn, EChessPieceColor.White);
            SetPiece(6, 7, EChessPieceType.Pawn, EChessPieceColor.White);
        }

        public ChessCellModel GetCell(CellId cellId)
        {
            return _cells[cellId];
        }

        public bool IsValidMove(CellId source, CellId target)
        {
            return _gameRules.IsValidMove(source, target, this);
        }

        public void MovePiece(CellId source, CellId target)
        {
            if (!_gameRules.IsValidMove(source, target, this))
            {
                throw new InvalidOperationException("This is an invalid move.");
            }

            var pieceType = _cells[source].PieceType;
            var pieceColor = _cells[source].PieceColor;
           
            SetPiece(target, pieceType, pieceColor);
            SetPiece(source, EChessPieceType.Empty, EChessPieceColor.Empty);
            _moveHistory.AddMove(new Move(source, target));
        }
        public void SwapPiece(CellId source, CellId target)
        {
            if (!_gameRules.IsValidMove(source, target, this))
            {
                throw new InvalidOperationException("This is an invalid move.");
            }

            var pieceType1 = _cells[source].PieceType;
            var pieceType2 = _cells[target].PieceType;
            var pieceColor1 = _cells[source].PieceColor;
            var pieceColor2 = _cells[target].PieceColor;

            SetPiece(target, pieceType1, pieceColor1);
            SetPiece(source, pieceType2, pieceColor2);
            // Can we use this for castling??
            //
            _moveHistory.AddMove(new Move(source, target));
        }


        // We need this for castling and other things.
        //
        public bool HasMoved(CellId cell)
        {
            // Just look through the history and check if some piece has moved to this cell.
            //
            foreach (var move in _moveHistory)
            {
                if (move.Target.Equals(cell))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
