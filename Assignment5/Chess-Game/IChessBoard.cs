using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public interface IChessBoard
    {
        int NumRows { get; }

        int NumCols { get; }

        event EventHandler<CellStatusChangedEventArgs> CellStatusChanged;

        void InitializeBoard();

        ChessCellModel GetCell(CellId cellId);

        bool IsValidMove(CellId source, CellId target);

        void MovePiece(CellId source, CellId target);

        void SwapPiece(CellId source, CellId target);

        bool HasMoved(CellId cell);
    }
}
