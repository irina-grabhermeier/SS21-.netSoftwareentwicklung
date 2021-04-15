using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    public class PieceCapturedEventArgs : EventArgs
    {
        private EChessPieceType _pieceType;

        public EChessPieceType PieceType
        {
            get { return _pieceType; }
        }

        private EChessPieceColor _pieceColor;

        public EChessPieceColor PieceColor
        {
            get { return _pieceColor; }
        }

        public PieceCapturedEventArgs(EChessPieceType pieceType, EChessPieceColor pieceColor)
        {
            _pieceType = pieceType;
            _pieceColor = pieceColor;
        }
    }
}
