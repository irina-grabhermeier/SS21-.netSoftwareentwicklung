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
    class MainWindowViewModel
    {
        private readonly List<List<ICellStatusViewModel>> _cells = new List<List<ICellStatusViewModel>>();
        public List<List<ICellStatusViewModel>> Cells
        {
            get { return _cells; }
        }

        public MainWindowViewModel()
        {
            generateCellViewModels(8, 8);
        }

        private void generateCellViewModels(int rows, int cols)
        {
            int id = 0;
            for (int row = 0; row < rows; row++)
            {
                List<ICellStatusViewModel> colList = new List<ICellStatusViewModel>();
                _cells.Add(colList);
                for (int col = 0; col < cols; col++)
                {
                    EFieldType fieldType = ((row % 2) + col) % 2 == 0 ? EFieldType.Light : EFieldType.Dark;
                    EPieceType pieceType = getPieceType(row, col);
                    EPieceColor pieceColor = getPieceColor(row);
                    colList.Add(new CellStatusViewModel(id, fieldType, pieceType, pieceColor));
                    id++;
                }
            }
        }

        private EPieceColor getPieceColor(int row)
        {
            if (row < 2)
            {
                return EPieceColor.Black;
            }
            if (row > 5)
            {
                return EPieceColor.White;
            }
            return EPieceColor.Empty;
        }

        private EPieceType getPieceType(int row, int col)
        {
            if (getPieceColor(row) == EPieceColor.Empty)
            {
                return EPieceType.Empty;
            }
            if (row == 1 || row == 6)
            {
                return EPieceType.Pawn;
            }
            switch (col)
            {
                case 0:
                case 7:
                    return EPieceType.Rook;
                case 1:
                case 6:
                    return EPieceType.Knight;
                case 2:
                case 5:
                    return EPieceType.Bishop;
                case 3:
                    return EPieceType.Queen;
                case 4:
                    return EPieceType.King;
            }
            return EPieceType.Empty;
        }
    }
}
