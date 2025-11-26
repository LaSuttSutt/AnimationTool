using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace AnimationTool.Ui._Controls;

public partial class OptionUc
{
    public static ObservableCollection<KeyValuePair<string, string>> TestItems => 
        [new("1x", "1"), new("2x", "2"), new("3x", "3"), new("4x", "4")];

    public static string TestText => "Zoom:";

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(OptionUc), new PropertyMetadata(default(string)));
    
    public OptionUc()
    {
        InitializeComponent();
    }
    
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}