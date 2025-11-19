using System.Windows.Controls;

namespace AnimationTool.Ui;

public partial class NavigationUc : UserControl
{
    public NavigationUc()
    {
        InitializeComponent();
        DataContext = new NavigationUcViewModel();
    }
}