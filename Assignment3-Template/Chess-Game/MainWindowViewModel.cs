using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Game
{
    class MainWindowViewModel
    {
        private readonly List<List<ICellStatusViewModel>> _cells;
        public List<List<ICellStatusViewModel>> Cells
        {
            get { return _cells; }
        }

        public MainWindowViewModel()
        {
            _cells = new List<List<ICellStatusViewModel>>();
            for (int i = 0; i <= 8; i++)
            {
                List<ICellStatusViewModel> col = new List<ICellStatusViewModel>();
                _cells.Add(col);
                for (int j = 0; j <= 8; j++)
                {
                    col.Add(new CellStatusViewModel());
                }
            }
        }
    }
}
