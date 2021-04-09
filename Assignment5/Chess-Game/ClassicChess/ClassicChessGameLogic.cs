using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public class ClassicChessGameLogic : IGameLogic
    {
        private EActivePlayer _activePlayer = EActivePlayer.White;
        private IChessBoard _chessboard;
        private IGameRules _gameRules;

        public event EventHandler<CellStatusChangedEventArgs> CellStatusChanged = delegate { };

        public ClassicChessGameLogic(IGameRules gameRules, IChessBoard chessboard)
        {
            _gameRules = gameRules;
            _chessboard = chessboard;
            _chessboard.CellStatusChanged += _chessboard_CellStatusChanged;
        }

        private void _chessboard_CellStatusChanged(object sender, CellStatusChangedEventArgs e)
        {
            CellStatusChanged(this, e);
        }

        public void StartGame()
        {
            _chessboard.InitializeBoard();
            _activePlayer = EActivePlayer.White;
        }

        private void nextTurn()
        {
            if (_activePlayer == EActivePlayer.White)
            {
                _activePlayer = EActivePlayer.Black;
            }
            else
            {
                _activePlayer = EActivePlayer.White;
            }
        }

        public int NumRows
        {
            get { return _chessboard.NumRows; }
        }

        public int NumCols
        {
            get { return _chessboard.NumCols; }
        }

        public bool IsValidMove(CellId source, CellId target)
        {
            return _gameRules.IsValidMove(source, target, _chessboard);
        }

        public void MovePiece(CellId source, CellId target)
        {
            if (!IsValidMove(source, target))
            {
                throw new InvalidOperationException("Invalid move!");
            }
            // Figure out if it is a castle operation.
            //
            var sourceCell = _chessboard.GetCell(source);
            var targetCell = _chessboard.GetCell(target);

            // We know it is a valid move. We just have to identify if it is a castle
            // operation. 
            //
            if (sourceCell.PieceType == EChessPieceType.King &&
                targetCell.PieceType == EChessPieceType.Rook &&
                sourceCell.PieceColor == targetCell.PieceColor)
            {
                _chessboard.SwapPiece(source, target);
            }
            else
            {
                _chessboard.MovePiece(source, target);
            }
            nextTurn();
        }

        public IReadOnlyList<CellId> ValidMovesForCell(CellId source)
        {
            return _gameRules.ValidMovesForCell(source, _chessboard);
        }

        public EActivePlayer ActivePlayer
        {
            get { return _activePlayer; }
        }
    }
}
