namespace TodoWebApp.ViewModels;

public class TodoItemRequestModel
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsCompleted { get; set; } = false;
}