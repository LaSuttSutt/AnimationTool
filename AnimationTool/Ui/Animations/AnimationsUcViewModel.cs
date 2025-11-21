using System.Collections.ObjectModel;
using System.Windows.Media;
using AnimationTool.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace AnimationTool.Ui.Animations;

public partial class AnimationsUcViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<AnimationBitmap> _sprites = [];
    [ObservableProperty] private AnimationBitmap? _selectedSprite;
    [ObservableProperty] private Stretch _previewStretch = Stretch.None;
    [ObservableProperty] private ImageSource? _imageSource;
    
    [ObservableProperty] private int _ground = 24;
    
    public RelayCommand LoadSpritesCommand { get; }

    public AnimationsUcViewModel()
    {
        LoadSpritesCommand = new RelayCommand(LoadSprites);
    }
    
    private void LoadSprites()
    {
        var dlg = new OpenFileDialog()
        {
            Filter = "PNG Files (*.png)|*.png",
            Multiselect = true
        };

        if (dlg.ShowDialog() != true) return;

        Sprites.Clear();
        for (var i = 0; i < dlg.FileNames.Length; i++)
        {
            Sprites.Add(new AnimationBitmap
            {
                Name = dlg.SafeFileNames[i],
                PngFileName = dlg.FileNames[i]
            });
        }
    }
    
    private void ShowPreview(AnimationBitmap? selected)
    {
        ImageSource = selected?.ConvertToBitmapImage(Ground, 4);
    }
    
    partial void OnSelectedSpriteChanged(AnimationBitmap? value)
    {
        SelectedSprite = value;
        ShowPreview(SelectedSprite);
    }
}