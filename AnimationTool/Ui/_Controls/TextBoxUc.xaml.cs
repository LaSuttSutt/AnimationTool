using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AnimationTool.Ui._Controls;

public partial class TextBoxUc
{
    public TextBoxUc()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty OnStartEditModeProperty = DependencyProperty.Register(
        nameof(OnStartEditMode), typeof(ICommand), typeof(TextBoxUc), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty OnEndEditModeProperty = DependencyProperty.Register(
        nameof(OnEndEditMode), typeof(ICommand), typeof(TextBoxUc), new PropertyMetadata(default(ICommand)));
    
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(TextBoxUc), new PropertyMetadata("Fallback"));

    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
        nameof(IsSelected), typeof(bool), typeof(TextBoxUc), new PropertyMetadata(false));

    public static readonly DependencyProperty ReadModeProperty = DependencyProperty.Register(
        nameof(ReadMode), typeof(Visibility), typeof(TextBoxUc), new PropertyMetadata(Visibility.Visible));

    public static readonly DependencyProperty EditModeProperty = DependencyProperty.Register(
        nameof(EditMode), typeof(Visibility), typeof(TextBoxUc), new PropertyMetadata(Visibility.Collapsed));
    
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public bool IsSelected
    {
        get => (bool) GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    
    public Visibility EditMode
    {
        get => (Visibility) GetValue(EditModeProperty);
        set => SetValue(EditModeProperty, value);
    }
    
    public Visibility ReadMode
    {
        get => (Visibility) GetValue(ReadModeProperty);
        set => SetValue(ReadModeProperty, value);
    }
    
    public ICommand OnStartEditMode
    {
        get => (ICommand) GetValue(OnStartEditModeProperty);
        set => SetValue(OnStartEditModeProperty, value);
    }
    
    public ICommand OnEndEditMode
    {
        get => (ICommand) GetValue(OnEndEditModeProperty);
        set => SetValue(OnEndEditModeProperty, value);
    }

    private void EditBtn_OnClick(object sender, RoutedEventArgs e)
    {
        ReadMode = Visibility.Collapsed;
        EditMode = Visibility.Visible;
    }

    private void OkBtn_OnClick(object sender, RoutedEventArgs e)
    {
        OnEndEditMode.Execute(null);
        ReadMode = Visibility.Visible;
        EditMode = Visibility.Collapsed;
        Keyboard.ClearFocus();
    }
    
    private void UIElement_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        var textbox = sender as TextBox;
        textbox?.Focus();
        textbox?.SelectAll();
        Keyboard.Focus(textbox);
    }

    private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        OkBtn_OnClick(sender, new RoutedEventArgs());
        OnEndEditMode.Execute(null);
        e.Handled = true;
    }
}