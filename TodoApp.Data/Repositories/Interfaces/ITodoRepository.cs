using TodoApp.Data.Entities;

namespace TodoApp.Data.Repositories.Interfaces;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync();

    Task<TodoItem?> GetByIdAsync(int id);

    Task<TodoItem> CreateAsync(TodoItem todoItem);

    Task<TodoItem> UpdateAsync(TodoItem todoItem);

    Task<bool> DeleteAsync(int id);
}