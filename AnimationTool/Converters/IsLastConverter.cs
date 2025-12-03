using System.Globalization;
using System.Windows.Data;

namespace AnimationTool.Converters;

public class IsLastConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var index = (int) values[0];
        var count = (int) values[1];
        return index == count - 1;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return [0, 0];
    }
}