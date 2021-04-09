using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public class CellStatusChangedEventArgs : EventArgs
    {
        public CellId Identifier { get; }
        public EChessPieceType OldPiece { get; }

        public EChessPieceColor OldColor { get; }

        public EChessPieceType NewPiece { get; }

        public EChessPieceColor NewColor { get; }

        public CellStatusChangedEventArgs(CellId identifier, EChessPieceType oldPiece, EChessPieceColor oldColor, 
                                          EChessPieceType newPiece, EChessPieceColor newColor)
        {
            Identifier = identifier;
            OldPiece = oldPiece;
            NewPiece = newPiece;
            OldColor = oldColor;
            NewColor = newColor;
        }
    }
}
