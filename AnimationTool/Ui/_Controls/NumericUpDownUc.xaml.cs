using System.Windows;
using System.Windows.Input;

namespace AnimationTool.Ui._Controls;

public partial class NumericUpDownUc
{
    public NumericUpDownUc()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty FieldNameProperty = DependencyProperty.Register(
        nameof(FieldName), typeof(string), typeof(NumericUpDownUc), new PropertyMetadata(default(string)));
    
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(NumericUpDownUc), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty ChangeValueCommandProperty = DependencyProperty.Register(
        nameof(ChangeValueCommand), typeof(ICommand), typeof(NumericUpDownUc), new PropertyMetadata(default(ICommand)));
    
    public static readonly DependencyProperty ChangeValueParameterUpProperty = DependencyProperty.Register(
        nameof(ChangeValueUpParameter), typeof(object), typeof(NumericUpDownUc), new PropertyMetadata(default(object)));
    
    public static readonly DependencyProperty ChangeValueParameterDownProperty = DependencyProperty.Register(
        nameof(ChangeValueDownParameter), typeof(object), typeof(NumericUpDownUc), new PropertyMetadata(default(object)));
    
    public string FieldName
    {
        get => (string) GetValue(FieldNameProperty);
        set => SetValue(FieldNameProperty, value);
    }  
    
    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public ICommand ChangeValueCommand
    {
        get => (ICommand) GetValue(ChangeValueCommandProperty);
        set => SetValue(ChangeValueCommandProperty, value);
    }
    
    public object ChangeValueUpParameter
    {
        get => GetValue(ChangeValueParameterUpProperty);
        set => SetValue(ChangeValueParameterUpProperty, value);
    }
    
    public object ChangeValueDownParameter
    {
        get => GetValue(ChangeValueParameterDownProperty);
        set => SetValue(ChangeValueParameterDownProperty, value);
    }
}