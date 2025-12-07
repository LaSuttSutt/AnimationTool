using System.Windows;

namespace AnimationTool.Ui._Controls;

public partial class LabelStd
{
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(LabelStd), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty FSizeProperty = DependencyProperty.Register(
        nameof(FSize), typeof(int), typeof(LabelStd), new PropertyMetadata(20));

    public int FSize
    {
        get => (int) GetValue(FSizeProperty);
        set => SetValue(FSizeProperty, value);
    }
    
    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public LabelStd()
    {
        InitializeComponent();
    }
}