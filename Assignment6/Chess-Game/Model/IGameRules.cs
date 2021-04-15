using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public interface IGameRules
    {
        IReadOnlyList<CellId> ValidMovesForCell(CellId cellId, IChessBoard board);

        bool IsValidMove(CellId source, CellId target, IChessBoard board);
    }
}
