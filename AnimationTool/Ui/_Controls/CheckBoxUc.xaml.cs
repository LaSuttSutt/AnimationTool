using System.Windows;
using System.Windows.Controls;

namespace AnimationTool.Ui._Controls;

public partial class CheckBoxUc : UserControl
{
    public CheckBoxUc()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty CheckBoxLabelProperty = DependencyProperty.Register(
        nameof(CheckBoxLabel), typeof(string), typeof(CheckBoxUc), new PropertyMetadata(default(string)));
    
    public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
        nameof(IsChecked), typeof(bool), typeof(CheckBoxUc), new PropertyMetadata(false));
    
    public bool IsChecked
    {
        get => (bool) GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }
    
    public string CheckBoxLabel
    {
        get => (string) GetValue(CheckBoxLabelProperty);
        set => SetValue(CheckBoxLabelProperty, value);
    }
}