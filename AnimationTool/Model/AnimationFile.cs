using MahApps.Metro.Controls;

namespace AnimationTool.Model;

[Serializable]
public class AnimationFile
{
    public int Columns { get; set; }
    public int Rows { get; set; }
    private Dictionary<Position, AnimationBitmap> Mapping { get; set; }
}