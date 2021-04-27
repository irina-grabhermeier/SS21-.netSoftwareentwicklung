using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Exercise_2.Annotations;

/*
 * Team: Richard Leierer and Irina Grabher Meier
*/

namespace Exercise_2
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly List<List<CellStatusViewModel>> _cells = new List<List<CellStatusViewModel>>();
        public List<List<CellStatusViewModel>> Cells
        {
            get { return _cells; }
        }

        private readonly IGameLogic _gameLogic;

        private void SetupCellStatusViewModels(int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                Cells.Add(new List<CellStatusViewModel>());
                for (int j = 0; j < cols; j++)
                {
                    EFieldType fieldType;
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0) // even rows start with black
                        {
                            fieldType = EFieldType.Black;
                        }
                        else
                        {
                            fieldType = EFieldType.White;
                        }
                    }
                    else
                    {
                        if (j % 2 == 0) // uneven rows start with white
                        {
                            fieldType = EFieldType.White;
                        }
                        else
                        {
                            fieldType = EFieldType.Black;
                        }
                    }

                    var cell = new CellStatusViewModel(i, j, fieldType);
                    cell.CellSelected += Cell_CellSelected;
                    Cells[i].Add(cell);
                }
            }
        }

        private CellStatusViewModel fetchCell(CellId identifier)
        {
            return Cells[identifier.Row][identifier.Col];
        }

        private EActivePlayer _activePlayer = EActivePlayer.White;
        public EActivePlayer ActivePlayer
        {
            get { return _activePlayer; }
            set
            {
                _activePlayer = value;
                OnPropertyChanged(nameof(ActivePlayer));
            }
        }

        private void Cell_CellSelected(object sender, CellId selectedCellIdentifier)
        {
            // XXX: Implement the move logic here.
            // Hint: Use the _gameLogic object to check for valid moves and for moving pieces. 
            // If a user selects a piece, we remember the selected piece using the _selectedPiece
            // field. If a user then selects a valid field, we move the piece. If the user selects
            // an invalid field, we reset the selection.
            //

            var targetCell = GetCell(selectedCellIdentifier);
            if (_selectedPiece == null)
            {
                if (!CanActivePlayerMove(targetCell.ChessPieceColor))
                {
                    return;
                }

                _selectedPiece = targetCell;
                _selectedPiece.IsCellSelected = true;

                SetValidMoveTargets(true, _selectedPiece.Identifier);

                return;
            }

            var sourceCell = _selectedPiece;
            _selectedPiece.IsCellSelected = false;
            _selectedPiece = null;

            SetValidMoveTargets(false, sourceCell.Identifier);

            bool isValidMove = _gameLogic
                .IsValidMove(sourceCell.Identifier, targetCell.Identifier);
            if (!isValidMove)
            {
                return;
            }

            _gameLogic.MovePiece(sourceCell.Identifier, targetCell.Identifier);
        }

        private void SetValidMoveTargets(bool isValidMoveTarget, CellId sourceCell)
        {
            var validMoveTargets = _gameLogic.ValidMovesForCell(sourceCell);
            foreach (var moveTarget in validMoveTargets)
            {
                GetCell(moveTarget).IsValidMoveTarget = isValidMoveTarget;
            }
        }

        private bool CanActivePlayerMove(EChessPieceColor color)
        {
            switch (color)
            {
                case EChessPieceColor.Black when _gameLogic.ActivePlayer == EActivePlayer.Black:
                case EChessPieceColor.White when _gameLogic.ActivePlayer == EActivePlayer.White:
                    return true;
                default:
                    return false;
            }
        }

        private CellStatusViewModel GetCell(CellId cellId)
        {
            return Cells[cellId.Row][cellId.Col];
        }


        private CellStatusViewModel _selectedPiece;
        private bool _isPieceSelected = false;

        public MainViewModel(IGameLogic gameLogic)
        {
            SurrenderGameCommand = new RelayCommand(SurrenderGameCommandHandle, (e) => true);
            RestartGameCommand = new RelayCommand(RestartGameCommandHandle, (e) => true);
            _gameLogic = gameLogic;
            _gameLogic.CellStatusChanged += _chessboard_CellStatusChanged;

            SetupCellStatusViewModels(_gameLogic.NumRows, _gameLogic.NumCols);
            _gameLogic.StartGame();
        }

        private void SurrenderGameCommandHandle(object obj)
        {
            // Just for demo reasons.
            MessageBox.Show("Game surrendered!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Warning);
            RestartGameCommandHandle(obj);
        }


        private void RestartGameCommandHandle(object obj)
        {
            _cells
                .SelectMany(cells => cells)
                .ToList()
                .ForEach(cell => cell.SetPiece(EChessPieceType.Empty, EChessPieceColor.Empty));
            _gameLogic.StartGame();
            _activePlayer = _gameLogic.ActivePlayer;
        }

        public RelayCommand SurrenderGameCommand { get; }

        public RelayCommand RestartGameCommand { get; }


        private void _chessboard_CellStatusChanged(object sender, CellStatusChangedEventArgs e)
        {
            // XXX: You probably have to implent this first to see any pieces. Handle the cell status
            // changed event. What do you have to do? Notify the CellStatusViewModel that the piece changed. 
            //

            var cell = GetCell(e.Identifier);
            cell.SetPiece(e.NewPiece, e.NewColor);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
