using System.Windows;

namespace AnimationTool.Ui._Controls;

public partial class SwitchStateUc
{
    public SwitchStateUc()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(SwitchStateUc), new PropertyMetadata("Zoom:"));

    public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
        nameof(Items), typeof(List<KeyValuePair<string, string>>), typeof(SwitchStateUc),
        new PropertyMetadata(default(List<KeyValuePair<string, string>>)));

    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
        nameof(SelectedItem), typeof(KeyValuePair<string, string>), typeof(SwitchStateUc), new PropertyMetadata(default(KeyValuePair<string, string>)));

    public static List<TestItem> TestItems =>
        [new("1x", "1"), new("2x", "2"), new("3x", "3"), new("4x", "4", true)];
    
    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public List<KeyValuePair<string, string>> Items
    {
        get => (List<KeyValuePair<string, string>>) GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }
    
    public KeyValuePair<string, string> SelectedItem
    {
        get => (KeyValuePair<string, string>) GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
}

public class TestItem
{
    public string Key { get; set; }
    public string Value { get; set; }
    public bool IsLast {get; set; }
    
    public TestItem(string key, string value, bool isLast = false)
    {
        Key = key;
        Value = value;
        IsLast = isLast;
    }
}