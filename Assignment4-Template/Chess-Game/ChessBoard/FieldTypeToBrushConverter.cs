using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Chess_Game
{
    /// <summary>
    /// Florian Eckhart, Irina Grabher Meier, Dennis Krobath
    /// </summary>
    class FieldTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EFieldType fieldType = (EFieldType)value;
            var resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("ResourceDictionary.xaml", UriKind.RelativeOrAbsolute);
            switch (fieldType)
            {
                case EFieldType.Dark:
                    return resourceDictionary["ChessBoardDarkSquareColor"];
                case EFieldType.Light:
                    return resourceDictionary["ChessBoardLightSquareColor"];
            }
            throw new ArgumentException("Invalid input, could not convert");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
