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
    [ObservableProperty] private bool _flipX;
    [ObservableProperty] private bool _playAnimation;
    [ObservableProperty] private bool _loopAnimation;
    [ObservableProperty] private decimal _animationSpeed = 0.1m;

    public RelayCommand LoadSpritesCommand { get; }
    public RelayCommand<string> ChangeGroundCommand { get; }
    public RelayCommand<string> ChangeAnimationSpeedCommand { get; }

    public AnimationsUcViewModel()
    {
        LoadSpritesCommand = new RelayCommand(LoadSprites);
        ChangeGroundCommand = new RelayCommand<string>(ChangeGround);
        ChangeAnimationSpeedCommand = new RelayCommand<string>(ChangeAnimationSpeed);
        Options = [
            new OptionUcModel("1x", 1), 
            new OptionUcModel("2x", 2), 
            new OptionUcModel("3x", 3), 
            new OptionUcModel("4x", 4, true)];
        SelectedOption = Options[0];
    }

    private void LoadSprites()
    {
        var dlg = new OpenFileDialog
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

    private void ShowPreview(AnimationBitmap? selected, bool redraw = false)
    {
        if (redraw)
            foreach (var animationBitmap in Sprites)
                animationBitmap.ImageSource = null;
        
        ImageSource = selected?.ConvertToBitmapImage(Ground, SelectedOption.Value, FlipX);
    }

    private void ChangeGround(string? value)
    {
        if (value == null) return;
        Ground += int.Parse(value);
        if (Ground < 0) Ground = 0;
        if (Ground > 100) Ground = 100;
        ShowPreview(SelectedSprite, true);
    }
    
    private async Task StartAnimation()
    {
        var index = 0;
        while (PlayAnimation)
        {
            ShowPreview(Sprites[index]);
            await Task.Delay((int)(AnimationSpeed * 1000));
            index++;

            if (index == Sprites.Count)
            {
                if (LoopAnimation)
                    index = 0;
                else
                {
                    PlayAnimation = false;
                    if (Sprites.Count > 0)
                        ShowPreview(Sprites[0]);
                }
            }
        }
    }

    private void ChangeAnimationSpeed(string? value)
    {
        if (value == null) return;
        AnimationSpeed += decimal.Parse(value);
        if (AnimationSpeed < 0.05m) AnimationSpeed = 0.05m;
        if (AnimationSpeed > 1.0m) AnimationSpeed = 1.0m;
    }

    partial void OnSelectedSpriteChanged(AnimationBitmap? value)
    {
        SelectedSprite = value;
        ShowPreview(SelectedSprite, true);
    }

    // ReSharper disable once UnusedParameterInPartialMethod
    partial void OnSelectedOptionChanged(OptionUcModel value)
    {
        ShowPreview(SelectedSprite, true);
    }

    // ReSharper disable once UnusedParameterInPartialMethod
    partial void OnFlipXChanged(bool value)
    {
        ShowPreview(SelectedSprite, true);
    }
    
    partial void OnPlayAnimationChanged(bool value)
    {
        if (value)
            _ = StartAnimation();
    }
}