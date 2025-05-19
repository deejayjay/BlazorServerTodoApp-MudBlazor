using TodoWebApp.ViewModels;

namespace TodoWebApp.Services.Interfaces;

public interface ITodoService
{
    Task<IEnumerable<TodoItemResponseModel>> GetAllAsync();

    Task<TodoItemResponseModel> GetByIdAsync(int id);

    Task<TodoItemResponseModel> CreateAsync(TodoItemRequestModel todoItemRequestModel);

    Task<TodoItemResponseModel> UpdateAsync(int id, TodoItemRequestModel todoItemRequestModel);

    Task<TodoItemResponseModel> ChangeStatusAsync(int id);

    Task<bool> DeleteAsync(int id);
}