using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Exercise_2
{
    public class ChessPieceTypeToImageConverter : IMultiValueConverter
    {
        private static Tuple<EChessPieceType, EChessPieceColor> key(EChessPieceType type, EChessPieceColor color)
        {
            return new Tuple<EChessPieceType, EChessPieceColor>(type, color);
        }

        private Dictionary<Tuple<EChessPieceType, EChessPieceColor>, string> _pieceToImageDictionary
        = new Dictionary<Tuple<EChessPieceType, EChessPieceColor>, string>
        {
            { key(EChessPieceType.Pawn, EChessPieceColor.Black), "Figures/pawn_black.png" },
            { key(EChessPieceType.Pawn, EChessPieceColor.White), "Figures/pawn_white.png" },
            { key(EChessPieceType.Bishop, EChessPieceColor.Black), "Figures/bishop_black.png" },
            { key(EChessPieceType.Bishop, EChessPieceColor.White), "Figures/bishop_white.png" },
            { key(EChessPieceType.Knight, EChessPieceColor.Black), "Figures/knight_black.png" },
            { key(EChessPieceType.Knight, EChessPieceColor.White), "Figures/knight_white.png" },
            { key(EChessPieceType.Rook, EChessPieceColor.Black), "Figures/rook_black.png" },
            { key(EChessPieceType.Rook, EChessPieceColor.White), "Figures/rook_white.png" },
            { key(EChessPieceType.Queen, EChessPieceColor.White), "Figures/queen_white.png" },
            { key(EChessPieceType.Queen, EChessPieceColor.Black), "Figures/queen_black.png" },
            { key(EChessPieceType.King, EChessPieceColor.White), "Figures/king_white.png" },
            { key(EChessPieceType.King, EChessPieceColor.Black), "Figures/king_black.png" }
        };


        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            EChessPieceType type = (EChessPieceType)values[0];
            EChessPieceColor color = (EChessPieceColor)values[1];

            if (type == EChessPieceType.Empty || color == EChessPieceColor.Empty)
            {
                return new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Figures/empty.png", UriKind.Absolute));
            }

            return new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + _pieceToImageDictionary[key(type, color)], UriKind.Absolute));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
