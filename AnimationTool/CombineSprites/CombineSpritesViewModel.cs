using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Threading;
using AnimationTool.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace AnimationTool.CombineSprites;

public partial class CombineSpritesViewModel : ObservableObject
{
    [ObservableProperty] private ImageSource? _imageSource; // =
    [ObservableProperty] private ObservableCollection<AnimationBitmap> _sprites = [];
    [ObservableProperty] private AnimationBitmap? _selectedSprite;
    [ObservableProperty] private Stretch _previewStretch = Stretch.None;
    [ObservableProperty] private string _playBtnText = "Play";
    [ObservableProperty] private decimal _playSpeed = 0.1m;
    [ObservableProperty] private bool _loopAnimation;
    [ObservableProperty] private int _ground;
    [ObservableProperty] private int _rows = 1;
    [ObservableProperty] private int _columns = 1;
    public RelayCommand LoadSpritesCommand { get; }
    public RelayCommand IncreaseGroundCommand { get; }
    public RelayCommand DecreaseGroundCommand { get; }
    public RelayCommand AddColumnCommand { get; }
    public RelayCommand AddRowCommand { get; }
    public RelayCommand RemoveColumnCommand { get; }
    public RelayCommand RemoveRowCommand { get; }
    public AsyncRelayCommand TogglePlayAnimationCommand { get; }
    public RelayCommand IncreasePlaySpeedCommand { get; }
    public RelayCommand DecreasePlaySpeedCommand { get; }

    private bool _playAnimation;

    public CombineSpritesViewModel()
    {
        LoadSpritesCommand = new RelayCommand(LoadSprites);
        IncreaseGroundCommand = new RelayCommand(IncreaseGround);
        DecreaseGroundCommand = new RelayCommand(DecreaseGround);
        AddRowCommand = new RelayCommand(AddRow);
        AddColumnCommand = new RelayCommand(AddColumn);
        RemoveRowCommand = new RelayCommand(RemoveRow);
        RemoveColumnCommand = new RelayCommand(RemoveColumn);
        TogglePlayAnimationCommand =
            new AsyncRelayCommand(TogglePlayAnimation, AsyncRelayCommandOptions.AllowConcurrentExecutions);
        IncreasePlaySpeedCommand = new RelayCommand(IncreasePlaySpeed);
        DecreasePlaySpeedCommand = new RelayCommand(DecreasePlaySpeed);
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
        ImageSource = selected?.ConvertToBitmapImage(Ground);
    }

    private void IncreaseGround()
    {
        if (Ground < 100)
            Ground += 1;

        ShowPreview(SelectedSprite);
    }

    private void DecreaseGround()
    {
        if (Ground > 0)
            Ground -= 1;

        ShowPreview(SelectedSprite);
    }

    private async Task TogglePlayAnimation()
    {
        if (!_playAnimation && Sprites.Count > 0)
        {
            _playAnimation = true;
            PlayBtnText = "Stop";
            await PlayAnimation();
        }
        else
        {
            _playAnimation = false;
            PlayBtnText = "Play";
            if (Sprites.Count > 0)
                ShowPreview(Sprites[0]);
        }
    }

    private void IncreasePlaySpeed()
    {
        PlaySpeed += 0.1m;
    }

    private void DecreasePlaySpeed()
    {
        if (PlaySpeed > 0.1m)
            PlaySpeed -= 0.1m;
    }

    private void AddColumn()
    {
    }

    private void RemoveColumn()
    {
    }

    private void AddRow()
    {
    }

    private void RemoveRow()
    {
    }

    partial void OnSelectedSpriteChanged(AnimationBitmap? value)
    {
        SelectedSprite = value;
        ShowPreview(SelectedSprite);
    }

    private async Task PlayAnimation()
    {
        var index = 0;
        while (_playAnimation)
        {
            ShowPreview(Sprites[index]);
            await Task.Delay((int)(PlaySpeed * 1000));
            index++;

            if (index == Sprites.Count)
            {
                if (LoopAnimation)
                    index = 0;
                else
                {
                    _playAnimation = false;
                    PlayBtnText = "Play";
                    if (Sprites.Count > 0)
                        ShowPreview(Sprites[0]);
                }
            }
        }
    }
}