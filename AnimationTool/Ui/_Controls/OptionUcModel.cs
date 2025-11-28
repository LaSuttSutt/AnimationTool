namespace AnimationTool.Ui._Controls;

public class OptionUcModel
{
    public string Display { get; }
    public int Value { get; }
    public bool IsLast { get; }
    
    public OptionUcModel(string display, int value, bool isLast = false)
    {
        Display = display; 
        Value = value;
        IsLast = isLast;
    }
}