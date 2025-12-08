using System.Globalization;
using System.Windows.Data;

namespace AnimationTool.Converters;

public class IsSelectedAndMouseOverConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2) return false;
        
        var isSelected = (bool) values[0];
        var isMouseOver = (bool) values[1];
        return isSelected && isMouseOver;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return [0, 0];
    }
}