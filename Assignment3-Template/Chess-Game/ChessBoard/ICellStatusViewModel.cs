using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    interface ICellStatusViewModel : INotifyPropertyChanged
    {
        EPieceColor PieceColor
        {
            get;
            set;
        }
        EPieceType PieceType
        {
            get;
            set;
        }
        EFieldType FieldType
        {
            get;
            set;
        }
        bool IsEmpty
        {
            get;
        }
    }
}
