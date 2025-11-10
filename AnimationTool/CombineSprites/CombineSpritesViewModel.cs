using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AnimationTool.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Point = System.Drawing.Point;

namespace AnimationTool.CombineSprites;

public partial class CombineSpritesViewModel : ObservableObject
{
    [ObservableProperty] private ImageSource? _imageSource =
        new BitmapImage(new Uri("pack://application:,,,/Images/Default.png"));

    [ObservableProperty] private ObservableCollection<AnimationBitmap> _sprites = [];
    [ObservableProperty] private AnimationBitmap? _selectedSprite;
    [ObservableProperty] private Stretch _previewStretch = Stretch.None;
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
    
    public CombineSpritesViewModel()
    {
        LoadSpritesCommand = new RelayCommand(LoadSprites);
        IncreaseGroundCommand = new RelayCommand(IncreaseGround);
        DecreaseGroundCommand = new RelayCommand(DecreaseGround);
        AddRowCommand = new RelayCommand(AddRow);
        AddColumnCommand = new RelayCommand(AddColumn);
        RemoveRowCommand = new RelayCommand(RemoveRow);
        RemoveColumnCommand = new RelayCommand(RemoveColumn);
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

    private void ShowPreview()
    {
        ImageSource = SelectedSprite?.ConvertToBitmapImage(Ground);
    }

    private void IncreaseGround()
    {
        if (Ground < 100)
            Ground += 1;
        
        ShowPreview();
    }

    private void DecreaseGround()
    {
        if (Ground > 0)
            Ground -= 1;
        
        ShowPreview();
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
        SelectedSprite  = value;
        ShowPreview();
    }
}