using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Entities;
using TodoApp.Data.Repositories.Interfaces;

namespace TodoApp.Data.Repositories;

public class TodoRepository(AppDbContext db) : ITodoRepository
{
    public async Task<TodoItem> CreateAsync(TodoItem todoItem)
    {
        if (todoItem == null)
        {
            throw new ArgumentNullException(nameof(todoItem));
        }

        todoItem.CreatedAt = DateTime.Now;
        todoItem.UpdatedAt = DateTime.Now;

        await db.TodoItems.AddAsync(todoItem);

        await db.SaveChangesAsync();

        return todoItem;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var todoItem = await db.TodoItems.FindAsync(id);

        if (todoItem == null)
        {
            return false;
        }

        db.TodoItems.Remove(todoItem);

        return await db.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await db.TodoItems.ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await db.TodoItems.FindAsync(id);
    }

    public async Task<TodoItem> UpdateAsync(TodoItem todoItem)
    {
        var existingTodoItem = await db.TodoItems.FindAsync(todoItem.Id);

        if (existingTodoItem == null)
        {
            throw new ArgumentException($"TodoItem with id {todoItem.Id} not found.");
        }

        existingTodoItem.Title = todoItem.Title;
        existingTodoItem.Description = todoItem.Description;
        existingTodoItem.IsCompleted = todoItem.IsCompleted;
        existingTodoItem.UpdatedAt = DateTime.Now;

        await db.SaveChangesAsync();

        return existingTodoItem;
    }
}