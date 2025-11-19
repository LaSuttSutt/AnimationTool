using System.Windows.Controls;
using AnimationTool.CombineSprites;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnimationTool.Ui;

public partial class NavigationUcViewModel : ObservableObject
{
    [ObservableProperty] private UserControl _selectedPage = new();
    public RelayCommand NavToAnimationsCommand { get; }

    private readonly UserControl _animationsUc = new CombineSprites.CombineSprites();

    public NavigationUcViewModel()
    {
        NavToAnimationsCommand = new RelayCommand(NavigateToAnimations);
        _animationsUc.DataContext = new CombineSpritesViewModel();
    }

    private void NavigateToAnimations()
    {
        SelectedPage = _animationsUc;
    }
}