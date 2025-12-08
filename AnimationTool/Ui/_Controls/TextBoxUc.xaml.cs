using System.Windows;
using System.Windows.Controls;

namespace AnimationTool.Ui._Controls;

public partial class TextBoxUc
{
    public TextBoxUc()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(TextBoxUc), new PropertyMetadata("Fallback"));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}