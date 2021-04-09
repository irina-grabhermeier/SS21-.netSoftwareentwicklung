using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public enum EActivePlayer
    {
        White,
        Black
    };

    public interface IGameLogic
    {
        int NumRows { get; }

        int NumCols { get; }

        bool IsValidMove(CellId source, CellId target);

        void MovePiece(CellId source, CellId target);

        IReadOnlyList<CellId> ValidMovesForCell(CellId source);

        void StartGame();

        EActivePlayer ActivePlayer { get; }

        event EventHandler<CellStatusChangedEventArgs> CellStatusChanged;

    }
}
