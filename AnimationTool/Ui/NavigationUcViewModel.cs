using System.Windows.Controls;
using AnimationTool.Ui.Animations;
using AnimationTool.Ui.MultiFile;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnimationTool.Ui;

public partial class NavigationUcViewModel : ObservableObject
{
    [ObservableProperty] private UserControl _selectedPage = new();
    [ObservableProperty] private string _selectedTab = "Animations";
    [ObservableProperty] private List<string> _tabNames = ["Animations", "Multi-File"];
    public RelayCommand NavToAnimationsCommand { get; }
    public RelayCommand NavToMultiFileCommand { get; }
    
    private readonly UserControl _animationsUc = new AnimationsUc();
    private readonly UserControl _multiFileUc = new MultiFileUc();

    public NavigationUcViewModel()
    {
        NavToAnimationsCommand = new RelayCommand(NavigateToAnimations);
        NavToMultiFileCommand = new RelayCommand(NavigateToMultiFile);
        _animationsUc.DataContext = new AnimationsUcViewModel();
        _multiFileUc.DataContext = new MultiFileUcViewModel();
        NavigateToAnimations();
    }

    private void NavigateToAnimations()
    {
        SelectedPage = _animationsUc;
    }

    private void NavigateToMultiFile()
    {
        SelectedPage = _multiFileUc;
    }

    partial void OnSelectedTabChanged(string value)
    {
        switch (value)
        {
            case "Animations":
                NavigateToAnimations();
                break;
            case "Multi-File":
                NavigateToMultiFile();
                break;
        }
    }
}