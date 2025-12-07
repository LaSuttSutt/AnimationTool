using AnimationTool.Model;

namespace AnimationTool.Ui;

public class ProjectsUcViewModel
{
    public static List<ProjectItem> TestData =
    [
        new(new() {Name = "Knight"}),
        new(new() {Name = "Monster"}) {EditMode = true},
        new(new() {Name = "Trap"})
    ];
}

public class ProjectItem
{
    public Project Project { get; set; }
    public bool EditMode { get; set; }

    public ProjectItem(Project project)
    {
        Project = project;
    }
}