using System.ComponentModel.DataAnnotations;

namespace TodoApp.Data.Entities;

public class TodoItem
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}