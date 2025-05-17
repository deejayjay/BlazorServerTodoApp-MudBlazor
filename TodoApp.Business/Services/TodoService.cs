using AutoMapper;
using TodoApp.Business.Dtos;
using TodoApp.Business.Services.Interfaces;
using TodoApp.Data.Entities;
using TodoApp.Data.Repositories.Interfaces;

namespace TodoApp.Business.Services;

public class TodoService (ITodoRepository todoRepository, IMapper mapper) : ITodoService
{
    public async Task<TodoItemDto> CreateAsync(TodoItemDto todoItem)
    {
        if (todoItem == null)
            throw new ArgumentNullException(nameof(todoItem));

        var todoEntity = mapper.Map<TodoItem>(todoItem);

        var newTodoItem = await todoRepository.CreateAsync(todoEntity);

        return mapper.Map<TodoItemDto>(newTodoItem);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await todoRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
    {
        var todoItemsFromDb = await todoRepository.GetAllAsync();

        return mapper.Map<IEnumerable<TodoItemDto>>(todoItemsFromDb);
    }

    public async Task<TodoItemDto> GetByIdAsync(int id)
    {
        var todoItemFromDb = await todoRepository.GetByIdAsync(id);

        if (todoItemFromDb == null)
            throw new ArgumentException($"TodoItem with id {id} not found.");

        return mapper.Map<TodoItemDto>(todoItemFromDb);
    }

    public async Task<TodoItemDto> UpdateAsync(TodoItemDto todoItem)
    {
        var todoEntity = mapper.Map<TodoItem>(todoItem);

        var updatedTodoItem = await todoRepository.UpdateAsync(todoEntity);

        return mapper.Map<TodoItemDto>(updatedTodoItem);
    }
}