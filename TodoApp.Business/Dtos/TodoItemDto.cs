namespace TodoApp.Business.Dtos;

public class TodoItemDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedAt { get; }

    public DateTime UpdatedAt { get; set; }        
}