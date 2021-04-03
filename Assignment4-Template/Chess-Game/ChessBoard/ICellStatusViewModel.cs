using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    /// <summary>
    /// Florian Eckhart, Irina Grabher Meier, Dennis Krobath
    /// </summary>
    interface ICellStatusViewModel : INotifyPropertyChanged
    {
        int Identifier
        {
            get;
        }
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
