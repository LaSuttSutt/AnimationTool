using System.Windows;

namespace AnimationTool.Ui._Controls;

public partial class TabUc
{
    public static List<string> TestTabs => ["Tab1", "Tab2", "Tab3", "Tab4"];
    public static int TestIndexLastItem => 3;

    public static readonly DependencyProperty TabNamesProperty = DependencyProperty.Register(
        nameof(TabNames), typeof(List<string>), typeof(TabUc), new PropertyMetadata(default(List<string>)));

    public static readonly DependencyProperty SelectedTabProperty = DependencyProperty.Register(
        nameof(SelectedTab), typeof(string), typeof(TabUc), new PropertyMetadata(default(string)));

    public static readonly DependencyProperty LastTabIndexProperty = DependencyProperty.Register(
        nameof(LastTabIndex), typeof(int), typeof(TabUc), new PropertyMetadata(0));
    
    public List<string> TabNames
    {
        get => (List<string>) GetValue(TabNamesProperty);
        set => SetValue(TabNamesProperty, value);
    }
    
    public string SelectedTab
    {
        get => (string) GetValue(SelectedTabProperty);
        set => SetValue(SelectedTabProperty, value);
    }
    
    public int LastTabIndex
    {
        get => (int) GetValue(LastTabIndexProperty);
        set => SetValue(LastTabIndexProperty, value);
    }
    
    public TabUc()
    {
        InitializeComponent();
    }
}