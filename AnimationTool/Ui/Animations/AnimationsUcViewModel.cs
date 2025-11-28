using System.Collections.ObjectModel;
using System.Windows.Media;
using AnimationTool.Model;
using AnimationTool.Ui._Controls;
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
    [ObservableProperty] private int _ground;
    [ObservableProperty] private OptionUcModel _selectedOption;
    [ObservableProperty] private List<OptionUcModel> _options;

    public RelayCommand LoadSpritesCommand { get; }
    public RelayCommand<string> ChangeGroundCommand { get; }

    public AnimationsUcViewModel()
    {
        LoadSpritesCommand = new RelayCommand(LoadSprites);
        ChangeGroundCommand = new RelayCommand<string>(ChangeGround);
        Options = [
            new OptionUcModel("1x", 1), 
            new OptionUcModel("2x", 2), 
            new OptionUcModel("3x", 3), 
            new OptionUcModel("4x", 4, true)];
        SelectedOption = Options[0];
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
        ImageSource = selected?.ConvertToBitmapImage(Ground, SelectedOption.Value);
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

    // ReSharper disable once UnusedParameterInPartialMethod
    partial void OnSelectedOptionChanged(OptionUcModel value)
    {
        ShowPreview(SelectedSprite);
    }
}