using System.Windows.Controls;

namespace AnimationTool.Ui;

public partial class ProjectsUc : UserControl
{
    public ProjectsUc()
    {
        DataContext = new ProjectsUcViewModel();
        InitializeComponent();
    }
}