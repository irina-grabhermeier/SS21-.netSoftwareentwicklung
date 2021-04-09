using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public class ChessCellModel
    {
        public int RowIndex { get; }
        public int ColIndex { get; }
        public EChessPieceType PieceType { get; private set; }

        public EChessPieceColor PieceColor { get; private set; }

        public ChessCellModel(int row, int col)
        {
            RowIndex = row;
            ColIndex = col;
            PieceType = EChessPieceType.Empty;
            PieceColor = EChessPieceColor.Empty;
        }

        public void SetPiece(EChessPieceType type, EChessPieceColor color)
        {
            PieceType = type;
            PieceColor = color;
        }

        public CellId Identifier
        {
            get { return CellId.Create(RowIndex, ColIndex); }
        }
    }
}
