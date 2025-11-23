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
    [ObservableProperty] private int _ground = 0;
    private int _zoom = 1;
    
    public RelayCommand LoadSpritesCommand { get; }
    public RelayCommand<string> SetZoomCommand { get; }
    public RelayCommand<string> ChangeGroundCommand { get; }

    public AnimationsUcViewModel()
    {
        LoadSpritesCommand = new RelayCommand(LoadSprites);
        SetZoomCommand = new RelayCommand<string>(SetZoom);
        ChangeGroundCommand = new RelayCommand<string>(ChangeGround);
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
        ImageSource = selected?.ConvertToBitmapImage(Ground, _zoom);
    }

    private void SetZoom(string? zoom)
    {
        if (zoom == null) return;
        _zoom = int.Parse(zoom);
        ShowPreview(SelectedSprite);
    }

    private void ChangeGround(string? value)
    {
        if (value == null) return;
        Ground += int.Parse(value);
        if (Ground < 0) Ground = 0;
        ShowPreview(SelectedSprite);
    }
    
    partial void OnSelectedSpriteChanged(AnimationBitmap? value)
    {
        SelectedSprite = value;
        ShowPreview(SelectedSprite);
    }
}