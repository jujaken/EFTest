using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EFTest.Converters
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class SelectedStateConverter : IValueConverter
    {
        public static Brush SelectedBrush = new SolidColorBrush()
        {
            Color = new Color { A = 255, R = 50, G = 250, B = 50 }
        };

        public static Brush NoSelectedBrush = new SolidColorBrush()
        {
            Color = new Color { A = 0, R = 0, G = 0, B = 0 }
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? SelectedBrush : NoSelectedBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Brush)value == SelectedBrush;
        }
    }
}
