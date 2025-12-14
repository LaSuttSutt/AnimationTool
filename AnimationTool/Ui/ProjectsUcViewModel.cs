using System.Collections.ObjectModel;
using System.Windows;
using AnimationTool.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnimationTool.Ui;

public partial class ProjectsUcViewModel : ObservableObject
{
    public static List<ProjectItem> TestData =
    [
        new(new() {Name = "Knight"}),
        new(new() {Name = "Monster"}),
        new(new() {Name = "Trap"})
    ];

    [ObservableProperty] private ProjectItem? _selectedItem;
    [ObservableProperty] private Visibility _readMode;
    [ObservableProperty] private ObservableCollection<ProjectItem> _projects = [];

    public RelayCommand<ProjectItem> OnStartEditModeCommand { get; }
    public RelayCommand OnEndEditModeCommand { get; }

    public ProjectsUcViewModel()
    {
        Projects.Add(new ProjectItem(new Project {Name = "Knight"}));
        Projects.Add(new ProjectItem(new Project {Name = "Monster"}));
        Projects.Add(new ProjectItem(new Project {Name = "Trap"}));
        
        OnStartEditModeCommand = new RelayCommand<ProjectItem>(OnStartEditMode);
        OnEndEditModeCommand = new RelayCommand(OnEndEditMode);
    }

    private void OnStartEditMode(ProjectItem? item)
    {
        foreach (var projectItem in Projects)
        {
            if (item == projectItem)
                continue;

            projectItem.ReadMode = false;
        }
    }

    private void OnEndEditMode()
    {
        foreach (var projectItem in Projects)
        {
            projectItem.ReadMode = true;
        }
    }
}

public partial class ProjectItem : ObservableObject
{
    public Project Project { get; set; }

    [ObservableProperty] private bool _readMode = true;
    
    public ProjectItem(Project project)
    {
        Project = project;
    }
}