using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace AnimationTool.Converters;

public class StretchToBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Stretch s)
            return s == Stretch.Uniform;

        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b ? Stretch.Uniform :  Stretch.None;

        return Stretch.None;
    }
}