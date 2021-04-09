using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2
{
    interface ICellStatusViewModel : INotifyPropertyChanged
    {
        EChessPieceType ChessPieceType { get; }
        EChessPieceColor ChessPieceColor { get; }
        bool IsEmpty { get; }
        EFieldType FieldType { get; }
        CellId Identifier { get; }
        void SetPiece(EChessPieceType type, EChessPieceColor color);
        bool IsCellSelected { get; set; }
        bool IsValidMoveTarget { get; set; }
        event EventHandler<CellId> CellSelected;
        RelayCommand CellSelectedCommand { get; }
    }
}
