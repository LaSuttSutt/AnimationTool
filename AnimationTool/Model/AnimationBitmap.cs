using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Json.Serialization;
using System.Windows.Media.Imaging;

namespace AnimationTool.Model;

public class AnimationBitmap
{
    public string PngFileName { get; set; } = "";
    public string Name { get; set; } = "";

    [JsonIgnore]
    private Bitmap BitmapFile
    {
        get
        {
            if (_bitmapFile != null)
                return _bitmapFile;

            if (!File.Exists(PngFileName))
                return new Bitmap(0, 0, PixelFormat.Format32bppArgb);

            _bitmapFile = new Bitmap(PngFileName);
            return _bitmapFile;
        }
    }

    private Bitmap? _bitmapFile;

    public BitmapImage ConvertToBitmapImage(int groundHeight)
    {
        if (BitmapFile.Clone() is not Bitmap preview)
            return new BitmapImage();
        
        for (var y = 0; y < groundHeight; y++)
        {
            for (var x = 0; x < preview.Width; x++)
            {
                preview.SetPixel(x, preview.Height - y - 1, Color.FromArgb(200, 125, 184, 68));
            }
        }
        
        using var memory = new MemoryStream();

        // Bitmap in Stream speichern
        preview.Save(memory, ImageFormat.Png);
        memory.Position = 0;

        // BitmapImage aus Stream laden
        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();

        bitmapImage.StreamSource = memory;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();

        return bitmapImage;
    }
}