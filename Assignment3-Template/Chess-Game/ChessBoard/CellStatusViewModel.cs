using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    class CellStatusViewModel : ICellStatusViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private EPieceColor _pieceColor;
        public EPieceColor PieceColor
        {
            get { return _pieceColor; }
            set
            {
                _pieceColor = value;
                onPropertyChanged(nameof(PieceColor));
            }
        }

        private EPieceType _pieceType;
        public EPieceType PieceType
        {
            get { return _pieceType; }
            set
            {
                _pieceType = value;
                onPropertyChanged(nameof(PieceType));
            }
        }

        private EFieldType _fieldType;
        public EFieldType FieldType
        {
            get { return _fieldType; }
            set
            {
                _fieldType = value;
                onPropertyChanged(nameof(FieldType));
            }
        }

        public bool IsEmpty
        {
            get { return PieceType == EPieceType.Empty || PieceColor == EPieceColor.Empty; }
        }

        private void onPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
