using System.Windows;

namespace AnimationTool.Ui._Controls;

public partial class OptionUc
{
    public static string TestText => "Zoom:";

    public static List<OptionUcModel> TestItems =>
        [new("1x", 1), new("2x", 2), new("3x", 3), new("4x", 4, true)];

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(OptionUc), new PropertyMetadata(default(string)));
    
    public static readonly DependencyProperty SelectedOptionProperty = DependencyProperty.Register(
        nameof(SelectedOption), typeof(OptionUcModel), typeof(OptionUc), new PropertyMetadata(default(OptionUcModel)));

    public static readonly DependencyProperty OptionsProperty = DependencyProperty.Register(
        nameof(Options), typeof(List<OptionUcModel>), typeof(OptionUc), new PropertyMetadata(default(List<OptionUcModel>)));
    
    public OptionUc()
    {
        InitializeComponent();
    }
    
    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public OptionUcModel SelectedOption
    {
        get => (OptionUcModel) GetValue(SelectedOptionProperty);
        set => SetValue(SelectedOptionProperty, value);
    }
    
    public List<OptionUcModel> Options
    {
        get => (List<OptionUcModel>) GetValue(OptionsProperty);
        set => SetValue(OptionsProperty, value);
    }
}