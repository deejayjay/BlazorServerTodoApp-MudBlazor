using TodoApp.Business.Dtos;

namespace TodoApp.Business.Services.Interfaces;

public interface ITodoService
{
    Task<IEnumerable<TodoItemDto>> GetAllAsync();
    Task<TodoItemDto> GetByIdAsync(int id);
    Task<TodoItemDto> CreateAsync(TodoItemDto todoItem);
    Task<TodoItemDto> UpdateAsync(TodoItemDto todoItem);
    Task<bool> DeleteAsync(int id);
}