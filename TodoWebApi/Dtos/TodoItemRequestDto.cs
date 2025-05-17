namespace TodoWebApi.Dtos;

public class TodoItemRequestDto
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsCompleted { get; set; } = false;
}