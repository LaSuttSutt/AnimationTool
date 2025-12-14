using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AnimationTool.Converters;

public class IsSelectedAndMouseOverConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 3) return false;
        
        var readMode = (Visibility)values[0];
        var isSelected = (bool) values[1];
        var isMouseOver = (bool) values[2];
        return readMode == Visibility.Visible && isSelected && isMouseOver;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return [0, 0];
    }
}