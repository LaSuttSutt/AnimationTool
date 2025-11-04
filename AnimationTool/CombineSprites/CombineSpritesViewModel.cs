using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace AnimationTool.CombineSprites;

public partial class CombineSpritesViewModel : ObservableObject
{
    [ObservableProperty] private ImageSource? _imageSource =
        new BitmapImage(new Uri("pack://application:,,,/Images/Default.png"));

    [ObservableProperty] private ObservableCollection<KeyValuePair<string, string>> _sprites = [];
    [ObservableProperty] private KeyValuePair<string, string> _selectedSprite;
    public RelayCommand LoadSpritesCommand { get; }
    
    public CombineSpritesViewModel()
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
        ImageSource = new BitmapImage(new Uri(fileName));
    }
    
    partial void OnSelectedSpriteChanged(KeyValuePair<string, string> value)
    {
        ShowPreview(value.Value);
    }
}