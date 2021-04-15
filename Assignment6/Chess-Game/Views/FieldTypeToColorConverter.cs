using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Exercise_2
{
    public class FieldTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EFieldType type = (EFieldType) value;
            switch (type)
            {
                case EFieldType.Black:
                    return new SolidColorBrush(Colors.CadetBlue);
                case EFieldType.White:
                    return new SolidColorBrush(Colors.White);
                default:
                    throw new ArgumentException("Not supported field type.");
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
