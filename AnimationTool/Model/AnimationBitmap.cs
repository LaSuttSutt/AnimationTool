using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Json.Serialization;
using System.Windows.Media.Imaging;

namespace AnimationTool.Model;

public class AnimationBitmap
{
    public string PngFileName { get; init; } = "";
    public string Name { get; init; } = "";

    [JsonIgnore]
    public BitmapImage? ImageSource { get; set; }
    
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

    public BitmapImage ConvertToBitmapImage(int groundHeight, int scaleFactor, bool flipX)
    {
        if (ImageSource != null)
            return ImageSource;
        
        if (BitmapFile.Clone() is not Bitmap preview)
            return new BitmapImage();

        if (flipX) preview.RotateFlip(RotateFlipType.RotateNoneFlipX);
        
        var resultBitmap = CreateBitmapAndDrawBackground(preview.Width, preview.Height, scaleFactor);
        DrawGroundToBitmap(resultBitmap, groundHeight, scaleFactor);
        DrawPreviewToBitmap(resultBitmap, preview, scaleFactor);
        
        using var memory = new MemoryStream();

        // Bitmap in Stream speichern
        resultBitmap.Save(memory, ImageFormat.Png);
        memory.Position = 0;

        // BitmapImage aus Stream laden
        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();

        bitmapImage.StreamSource = memory;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();

        ImageSource = bitmapImage;
        return bitmapImage;
    }

    private Bitmap CreateBitmapAndDrawBackground(int width, int height, int scaleFactor)
    {
        var resultBitmap = new Bitmap(
            width * scaleFactor,
            height * scaleFactor,
            PixelFormat.Format32bppArgb);

        var backgroundTileSize = 7;
        var backColor1 = Color.FromArgb(143, 143, 143);
        var backColor2 = Color.FromArgb(191, 191, 191);
        var startColor = backColor1;
        var widthCounter = 0;
        var heightCounter = 0;

        for (var x = 0; x < resultBitmap.Width; x++)
        {
            var backColor = startColor;

            for (var y = 0; y < resultBitmap.Height; y++)
            {
                resultBitmap.SetPixel(x, y, backColor);

                heightCounter++;
                if (heightCounter != backgroundTileSize) continue;
                heightCounter = 0;
                backColor = backColor == backColor1 ? backColor2 : backColor1;
            }

            heightCounter = 0;
            widthCounter++;
            if (widthCounter != backgroundTileSize) continue;
            widthCounter = 0;
            startColor = startColor == backColor1 ? backColor2 : backColor1;
        }

        return resultBitmap;
    }

    private void DrawGroundToBitmap(Bitmap bitmap, int groundHeight, int scaleFactor)
    {
        for (var y = 0; y < groundHeight * scaleFactor; y++)
        {
            for (var x = 0; x < bitmap.Width; x++)
            {
                var previewColor = bitmap.GetPixel(x, bitmap.Height - y - 1);
                var resultColor = Color.FromArgb((int) (125 * .7 + previewColor.R * .3),
                    (int) (184 * .7 + previewColor.G * .3), (int) (68 * .7 + previewColor.B * .3));
                bitmap.SetPixel(x, bitmap.Height - y - 1, resultColor);
            }
        }
    }

    private void DrawPreviewToBitmap(Bitmap bitmap, Bitmap preview, int scaleFactor)
    {
        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                var previewCoordX = x / scaleFactor;
                var previewCoordY = y / scaleFactor;
                var previewColor = preview.GetPixel(previewCoordX, previewCoordY);
                if (previewColor.A > 0) bitmap.SetPixel(x, y, previewColor);
            }
        }
    }
}