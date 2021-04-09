using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exercise_2
{
    /// <summary>
    /// Interaction logic for ChessGridCell.xaml
    /// </summary>
    public partial class ChessGridCell : UserControl
    {

        public static readonly DependencyProperty ColumnProperty =
            DependencyProperty.Register("CellColumn", typeof(int), typeof(ChessGridCell));

        public static readonly DependencyProperty RowProperty =
            DependencyProperty.Register("CellRow", typeof(int), typeof(ChessGridCell));

        public int CellColumn
        {
            get { return (int)GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }

        public int CellRow
        {
            get { return (int)GetValue(RowProperty); }
            set { SetValue(RowProperty, value); }
        }

        public ChessGridCell()
        {
            InitializeComponent();
        }
    }
}
