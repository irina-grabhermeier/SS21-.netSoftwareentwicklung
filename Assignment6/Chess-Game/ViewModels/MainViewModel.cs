using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Exercise_2.Annotations;

namespace Exercise_2
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IGameLogic _gameLogic;

        private readonly List<List<CellStatusViewModel>> _cells = new List<List<CellStatusViewModel>>();
        public List<List<CellStatusViewModel>> Cells
        {
            get { return _cells; }
        }

        private ObservableCollection<Tuple<EChessPieceType, EChessPieceColor>> _capturedWhitePieces =
            new ObservableCollection<Tuple<EChessPieceType, EChessPieceColor>>();
        public ObservableCollection<Tuple<EChessPieceType, EChessPieceColor>> CapturedWhitePieces
        {
            get { return _capturedWhitePieces; }
        }

        private ObservableCollection<Tuple<EChessPieceType, EChessPieceColor>> _capturedBlackPieces =
            new ObservableCollection<Tuple<EChessPieceType, EChessPieceColor>>();
        public ObservableCollection<Tuple<EChessPieceType, EChessPieceColor>> CapturedBlackPieces
        {
            get { return _capturedBlackPieces; }
        }


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

        private void displayValidMovesForPiece(CellStatusViewModel selectedCell)
        {
            clearValidMoves();
            var validMoves = _gameLogic.ValidMovesForCell(selectedCell.Identifier);
            foreach (var moveTarget in validMoves)
            {
                fetchCell(moveTarget).IsValidMoveTarget = true;
            }
        }

        private CellStatusViewModel fetchCell(CellId identifier)
        {
            return Cells[identifier.Row][identifier.Col];
        }


        private void clearValidMoves()
        {
            foreach (var row in Cells)
            {
                foreach (var cell in row)
                {
                    cell.IsValidMoveTarget = false;
                }
            }
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
            if (_isGameFinished)
            {
                return;
            }

            // If an empty cell or a cell of the opponent color was selected
            // unselect the previous cell.
            //
            var cell = fetchCell(selectedCellIdentifier);
            if (_isPieceSelected == true) // The user previously selected a valid piece. 
            {

                if (_gameLogic.IsValidMove(_selectedPiece.Identifier, selectedCellIdentifier))
                {
                    _gameLogic.MovePiece(_selectedPiece.Identifier, selectedCellIdentifier);
                    unselectAllCells();
                    clearValidMoves();
                    _isPieceSelected = false;
                    ActivePlayer = _gameLogic.ActivePlayer;
                }
                else
                {
                    // It is not a valid move. If another valid piece was selected change the selection.
                    //
                    if (!cell.IsEmpty)
                    {
                        if (ActivePlayer == EActivePlayer.Black && cell.ChessPieceColor == EChessPieceColor.Black ||
                            ActivePlayer == EActivePlayer.White && cell.ChessPieceColor == EChessPieceColor.White)
                        {
                            unselectAllCells();
                            clearValidMoves();
                            _selectedPiece = cell;
                            _selectedPiece.IsCellSelected = true;
                            displayValidMovesForPiece(_selectedPiece);
                        }
                    }
                }
            }

            if (_isPieceSelected == false)  // We have no selected piece. 
            {
                if (cell.IsEmpty) // Nothing to do here
                {
                    return;
                }
                // Check the user selected a valid piece. 
                //
                if (_gameLogic.ActivePlayer == EActivePlayer.White && cell.ChessPieceColor == EChessPieceColor.White ||
                    _gameLogic.ActivePlayer == EActivePlayer.Black && cell.ChessPieceColor == EChessPieceColor.Black)
                {
                    _selectedPiece = cell;
                    _selectedPiece.IsCellSelected = true;
                    _isPieceSelected = true;
                    displayValidMovesForPiece(_selectedPiece);
                }
                // User selected a an invalid piece. 
                //
                else
                {
                    return;
                }
            }
        }

        private CellStatusViewModel GetCell(Tuple<int, int> cellId)
        {
            return Cells[cellId.Item1][cellId.Item2];
        }

        private void unselectAllCells()
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                for (int j = 0; j < Cells[i].Count; j++)
                {
                    Cells[i][j].IsCellSelected = false;
                }
            }
        }


        private CellStatusViewModel _selectedPiece;
        private bool _isPieceSelected = false;
        private bool _isGameFinished = false;

        public bool IsGameFinished
        {
            get { return _isGameFinished; }
            set
            {
                if (_isGameFinished != value)
                {
                    _isGameFinished = value;
                    OnPropertyChanged(nameof(IsGameFinished));
                }
            }
        }

        private EActivePlayer _winner;
        public EActivePlayer Winner
        {
            get { return _winner; }
            set
            {
                if (_winner != value)
                {
                    _winner = value;
                    OnPropertyChanged(nameof(Winner));
                }
            }
        }

        public MainViewModel(IGameLogic gameLogic)
        {
            _gameLogic = gameLogic;
            _gameLogic.CellStatusChanged += _chessboard_CellStatusChanged;
            _gameLogic.PieceCaptured += _gameLogic_PieceCaptured;
            _gameLogic.GameFinished += _gameLogic_GameFinished;

            SetupCellStatusViewModels(_gameLogic.NumRows, _gameLogic.NumCols);
            _gameLogic.StartGame();

            _surrenderGameCommand = new RelayCommand(SurrenderGameCommandHandle, (e) => true);
            _restartGameCommand = new RelayCommand(RestartGameCommandHandle, (e) => true);
        }

        private void _gameLogic_GameFinished(object sender, GameEndedEventArgs e)
        {
            Winner = e.Winner;
            IsGameFinished = true;
            Task.Delay(2000).ContinueWith((x) => { IsGameFinished = false; });
        }

        private void _gameLogic_PieceCaptured(object sender, PieceCapturedEventArgs e)
        {
            if (e.PieceColor == EChessPieceColor.Black)
            {
                _capturedBlackPieces.Add(new Tuple<EChessPieceType, EChessPieceColor>(e.PieceType, e.PieceColor));
            }
            else if (e.PieceColor == EChessPieceColor.White)
            {
                _capturedWhitePieces.Add(new Tuple<EChessPieceType, EChessPieceColor>(e.PieceType, e.PieceColor));
            }
        }

        private void _chessboard_CellStatusChanged(object sender, CellStatusChangedEventArgs e)
        {
            fetchCell(e.Identifier).SetPiece(e.NewPiece, e.NewColor);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private RelayCommand _restartGameCommand;

        public RelayCommand RestartGameCommand
        {
            get
            {
                // XXX: Implement the restart game. Hint: use the _gameLogic methods and re-set the values in the MainViewModel
                //      correctly.
                return _restartGameCommand;

            }
        }

        private RelayCommand _surrenderGameCommand;

        public RelayCommand SurrenderGameCommand
        {
            get
            {
                // XXX: Implement the surrender game. Hint: use the _gameLogic.Surrender() method.
                //
                return _surrenderGameCommand;
            }
        }


        private void SurrenderGameCommandHandle(object obj)
        {
            _gameLogic.SurrenderGame();
            RestartGameCommandHandle(obj);
        }


        private void RestartGameCommandHandle(object obj)
        {

            _capturedBlackPieces.Clear();
            _capturedWhitePieces.Clear();

            _cells
                .SelectMany(cells => cells)
                .ToList()
                .ForEach(cell => cell.SetPiece(EChessPieceType.Empty, EChessPieceColor.Empty));
            _gameLogic.StartGame();
            _activePlayer = _gameLogic.ActivePlayer;
        }

    }
}
