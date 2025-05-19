using TodoWebApp.Services.Interfaces;
using TodoWebApp.ViewModels;

namespace TodoWebApp.Services;

public class TodoService : ITodoService
{
    private readonly HttpClient _httpClient;

    public TodoService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("TodoWebApi");
    }

    public async Task<TodoItemResponseModel> CreateAsync(TodoItemRequestModel todoItemRequestModel)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/todo", todoItemRequestModel);

            if (response is not null && response.IsSuccessStatusCode)
            {
                var todoItem = await response.Content.ReadFromJsonAsync<TodoItemResponseModel>();

                return todoItem ?? new TodoItemResponseModel();
            }

            throw new Exception($"Failed to create Todo item. Status code: {response?.StatusCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            return await _httpClient.DeleteAsync($"api/todo/{id}") is { IsSuccessStatusCode: true };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<IEnumerable<TodoItemResponseModel>> GetAllAsync()
    {
        try
        {
            var todoItems = await _httpClient.GetFromJsonAsync<IEnumerable<TodoItemResponseModel>>("api/todo");

            return todoItems ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<TodoItemResponseModel> GetByIdAsync(int id)
    {
        var todoItem = await _httpClient.GetFromJsonAsync<TodoItemResponseModel>($"api/todo/{id}");

        return todoItem ?? new TodoItemResponseModel();
    }

    public async Task<TodoItemResponseModel> ChangeStatusAsync(int id)
    {
        var todoItem = await _httpClient.GetFromJsonAsync<TodoItemResponseModel>($"api/todo/{id}");

        if (todoItem == null)
        {
            throw new Exception($"Todo item with Id {id} not found.");
        }

        var updatedTodoItem = await _httpClient.PutAsync($"api/todo/{id}/status", null);

        if (updatedTodoItem.IsSuccessStatusCode)
        {
            return await updatedTodoItem.Content.ReadFromJsonAsync<TodoItemResponseModel>() ?? new TodoItemResponseModel();
        }

        throw new Exception($"Failed to update the status of Todo item with Id {id}.");
    }

    public async Task<TodoItemResponseModel> UpdateAsync(int id, TodoItemRequestModel todoItemRequestModel)
    {
        var todoItem = await _httpClient.GetFromJsonAsync<TodoItemResponseModel>($"api/todo/{id}") ?? throw new Exception($"Todo item with Id {id} not found.");

        var updatedTodoItem = await _httpClient.PutAsJsonAsync($"api/todo/{id}", todoItemRequestModel);

        if (updatedTodoItem.IsSuccessStatusCode)
        {
            return await updatedTodoItem.Content.ReadFromJsonAsync<TodoItemResponseModel>() ?? new TodoItemResponseModel();
        }

        throw new Exception($"Failed to update the Todo item with Id {id}.");
    }
}