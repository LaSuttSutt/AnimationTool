using CommunityToolkit.Mvvm.ComponentModel;

namespace AnimationTool.Model;

public partial class Project : ObservableObject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [ObservableProperty] private  string _name = "New project";
}