using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public class CellStatus
    {
        public int RowIndex { get; }

        public int ColIndex { get; }

        public bool IsEmpty
        {
            get { return PieceType == EChessPieceType.Empty; }
        }

        public EChessPieceType PieceType { get; }

        public CellStatus(int rowIndex, int colIndex)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex;
            PieceType = EChessPieceType.Empty;
        }
        public CellStatus(int rowIndex, int colIndex, EChessPieceType pieceType)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex;
            PieceType = pieceType;
        }
    }
}
