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

    public BitmapImage ConvertToBitmapImage(int groundHeight, int scaleFactor)
    {
        if (BitmapFile.Clone() is not Bitmap preview)
            return new BitmapImage();

        var result = new Bitmap(preview.Width * scaleFactor + 2, 
            preview.Height * scaleFactor + 2, PixelFormat.Format32bppArgb);
        
        var widthCounter = 0;
        var heightCounter = 0;
        var backColor1 = Color.FromArgb(143, 143, 143);
        var backColor2 = Color.FromArgb(191, 191, 191);
        var startColor = backColor1;
        
        for (var x = 1; x < result.Width - 1; x++)
        {
            var backColor = startColor;
            
            for (var y = 1; y < result.Height - 1; y++)
            {
                var previewCoordX = (x - 1) / scaleFactor;
                var previewCoordY = (y - 1) / scaleFactor;
                var previewColor = preview.GetPixel(previewCoordX, previewCoordY);
                result.SetPixel(x, y, previewColor.A > 0 ? previewColor : backColor);

                heightCounter++;
                if (heightCounter != 7) continue;
                heightCounter = 0;
                backColor = backColor == backColor1 ? backColor2 : backColor1;
            }

            heightCounter = 0;
            widthCounter++;
            if (widthCounter != 7) continue;
            widthCounter = 0;
            startColor = startColor == backColor1 ? backColor2 : backColor1;
        }

        for (var y = 0; y < groundHeight * scaleFactor; y++)
        {
            for (var x = 1; x < result.Width - 1; x++)
            {
                result.SetPixel(x, result.Height - y - 2, Color.FromArgb(125, 125, 184, 68));
            }
        }

        var borderColor = Color.FromArgb(27, 99, 255);
        for (var x = 0; x < result.Width; x++)
        {
            result.SetPixel(x, 0, borderColor);
            result.SetPixel(x, result.Height - 1, borderColor);
        }
        for (var y = 0; y < result.Height; y++)
        {
            result.SetPixel(0, y, borderColor);
            result.SetPixel(result.Width - 1, y, borderColor);
        }
        
        //result.RotateFlip(RotateFlipType.RotateNoneFlipX);

        using var memory = new MemoryStream();

        // Bitmap in Stream speichern
        result.Save(memory, ImageFormat.Png);
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