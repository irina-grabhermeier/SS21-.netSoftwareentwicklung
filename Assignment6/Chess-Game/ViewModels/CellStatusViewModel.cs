using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Exercise_2.Annotations;

namespace Exercise_2
{
    public class CellStatusViewModel : ICellStatusViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler<CellId> CellSelected = delegate { };

        private readonly EFieldType _fieldType;
        public EFieldType FieldType
        {
            get { return _fieldType; }
        }

        private bool _isCellSelected = false;
        public bool IsCellSelected
        {
            get { return _isCellSelected; }
            set
            {
                if (_isCellSelected != value)
                {
                    _isCellSelected = value;
                    OnPropertyChanged(nameof(IsCellSelected));
                }
            }
        }

        private bool _isValidMoveTarget = false;
        public bool IsValidMoveTarget
        {
            get { return _isValidMoveTarget; }
            set
            {
                if (_isValidMoveTarget != value)
                {
                    _isValidMoveTarget = value;
                    OnPropertyChanged(nameof(IsValidMoveTarget));
                }
            }
        }

        public bool IsEmpty
        {
            get { return _chessPieceType == EChessPieceType.Empty; }
        }

        private EChessPieceType _chessPieceType = EChessPieceType.Empty;
        public EChessPieceType ChessPieceType
        {
            get { return _chessPieceType; }
            private set
            {
                if (_chessPieceType != value)
                {
                    _chessPieceType = value;
                    OnPropertyChanged(nameof(ChessPieceType));
                }
            }
        }

        private EChessPieceColor _chessPieceColor = EChessPieceColor.Empty;
        public EChessPieceColor ChessPieceColor
        {
            get { return _chessPieceColor; }
            private set
            {
                if (_chessPieceColor != value)
                {
                    _chessPieceColor = value;
                    OnPropertyChanged(nameof(ChessPieceColor));
                }
            }
        }

        private RelayCommand _cellSelectedCommand = null;
        public RelayCommand CellSelectedCommand
        {
            get
            {
                return _cellSelectedCommand ??
                       (_cellSelectedCommand = new RelayCommand(
                           (x) =>
                           {
                               CellSelected(this, CellId.Create(_rowIndex, _colIndex));

                           }, // Execute
                           (x) => { return true; } // CanExecute
                       ));
            }
        }

        public void SetPiece(EChessPieceType type, EChessPieceColor color)
        {
            ChessPieceType = type;
            ChessPieceColor = color;
        }

        public CellId Identifier
        {
            get { return CellId.Create(_rowIndex, _colIndex);}
        }

        private int _rowIndex;
        private int _colIndex;

        public CellStatusViewModel(int row, int col, EFieldType fieldType)
        {
            _fieldType = fieldType;
            _colIndex = col;
            _rowIndex = row;
        }

    }
}
