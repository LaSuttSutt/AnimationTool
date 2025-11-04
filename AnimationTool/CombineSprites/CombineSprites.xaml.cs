using System.Windows.Controls;

namespace AnimationTool.CombineSprites;

public partial class CombineSprites : UserControl
{
    public CombineSprites()
    {
        InitializeComponent();
        DataContext = new CombineSpritesViewModel();
    }
}