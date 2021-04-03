using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Chess_Game
{
    /// <summary>
    /// Florian Eckhart, Irina Grabher Meier, Dennis Krobath
    /// </summary>
    public class ChessPieceTypeToImageConverter : IMultiValueConverter
    {
        private ResourceDictionary resourceDictionary;
        public ChessPieceTypeToImageConverter()
        {
            resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("Resources.xaml", UriKind.RelativeOrAbsolute);
        }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            EPieceType type = (EPieceType)values[0];
            EPieceColor color = (EPieceColor)values[1];

            if (type == EPieceType.Empty || color == EPieceColor.Empty)
            {
                return null;
            }

            string resourceKey = "";
            resourceKey += type.ToString();
            resourceKey += color.ToString();
            return resourceDictionary[resourceKey];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
