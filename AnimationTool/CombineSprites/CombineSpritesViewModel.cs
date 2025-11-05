using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Color = System.Drawing.Color;

namespace AnimationTool.CombineSprites;

public partial class CombineSpritesViewModel : ObservableObject
{
    [ObservableProperty] private ImageSource? _imageSource =
        new BitmapImage(new Uri("pack://application:,,,/Images/Default.png"));

    [ObservableProperty] private ObservableCollection<KeyValuePair<string, string>> _sprites = [];
    [ObservableProperty] private KeyValuePair<string, string> _selectedSprite;
    [ObservableProperty] private Stretch _previewStretch = Stretch.None;
    [ObservableProperty] private int _ground = 0;
    public RelayCommand LoadSpritesCommand { get; }
    public RelayCommand IncreaseGroundCommand { get; }
    public RelayCommand DecreaseGroundCommand { get; }

    private Color GroundColor { get; } = Color.FromArgb(200, 125, 184, 68);
    private string CurrentPreview { get; set; } = "";
    
    public CombineSpritesViewModel()
    {
        LoadSpritesCommand = new RelayCommand(LoadSprites);
        IncreaseGroundCommand = new RelayCommand(IncreaseGround);
        DecreaseGroundCommand = new RelayCommand(DecreaseGround);
    }
    
    private void LoadSprites()
    {
        var dlg = new OpenFileDialog()
        {
            Filter = "PNG Files (*.png)|*.png",
            Multiselect = true
        };

        if (dlg.ShowDialog() == true)
        {
            Sprites.Clear();
            for (int i = 0; i < dlg.FileNames.Length; i++)
            {
                Sprites.Add(new KeyValuePair<string, string>(dlg.SafeFileNames[i], dlg.FileNames[i]));
            }
        }
        
        
        // dlg.Filter = "PNG Files (*.png)|*.png";
        // if (dlg.ShowDialog() == true)
        // {
        //     var bitmap = new Bitmap(dlg.FileName);
        //     if (_bitmapSize.Width == 0)
        //         _bitmapSize = new Size(bitmap.Width, bitmap.Height);
        //     else if (_bitmapSize.Width != bitmap.Width || _bitmapSize.Height != bitmap.Height)
        //     {
        //         MessageBox.Show("Das Image hat eine falsche Grösse");
        //         return;
        //     }
        //
        //     bitmap.Tag = dlg.FileName;
        //     _bitmaps.Add(bitmap);
        //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileNames)));
        //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanSave)));
        // }
            
    }

    private void ShowPreview(string fileName)
    {
        var bitmap = new Bitmap(fileName);

        for (var y = 0; y < Ground; y++)
        {
            for (var x = 0; x < bitmap.Width; x++)
            {
                bitmap.SetPixel(x, bitmap.Height - y - 1, GroundColor);
            }
        }
        
        ImageSource = ConvertToBitmapImage(bitmap);
    }

    private void IncreaseGround()
    {
        if (Ground < 100)
            Ground += 1;
        
        ShowPreview(CurrentPreview);
    }

    private void DecreaseGround()
    {
        if (Ground > 0)
            Ground -= 1;
        
        ShowPreview(CurrentPreview);
    }

    private BitmapImage ConvertToBitmapImage(Bitmap bitmap)
    {
        using MemoryStream memory = new MemoryStream();
        // Bitmap in Stream speichern
        bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
        memory.Position = 0;

        // BitmapImage aus Stream laden
        BitmapImage bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
            
        bitmapImage.StreamSource = memory;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();

        return bitmapImage;
    }
    
    partial void OnSelectedSpriteChanged(KeyValuePair<string, string> value)
    {
        CurrentPreview = value.Value;
        ShowPreview(value.Value);
    }
}