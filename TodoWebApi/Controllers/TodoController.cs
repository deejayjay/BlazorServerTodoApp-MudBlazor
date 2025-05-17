using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Business.Dtos;
using TodoApp.Business.Services.Interfaces;
using TodoWebApi.Dtos;

namespace TodoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(ITodoService todoService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemResponseDto>>> GetAllAsync()
        {
            var todoItemsDto = await todoService.GetAllAsync();

            if (todoItemsDto == null || !todoItemsDto.Any())
            {
                return NotFound("No todo items found.");
            }

            var todoItems = mapper.Map<IEnumerable<TodoItemResponseDto>>(todoItemsDto);

            return Ok(todoItems);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemResponseDto>> CreateAsync([FromBody] TodoItemRequestDto todoItemRequest)
        {
            var todoItemDto = mapper.Map<TodoItemDto>(todoItemRequest);

            var createdTodoItemDto = await todoService.CreateAsync(todoItemDto);
            var createdTodoItem = mapper.Map<TodoItemResponseDto>(createdTodoItemDto);

            return CreatedAtAction("GetById", new { id = createdTodoItem.Id }, createdTodoItem);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TodoItemResponseDto>> GetByIdAsync(int id)
        {
            var todoItemDto = await todoService.GetByIdAsync(id);

            if (todoItemDto == null)
            {
                return NotFound($"Todo item with id {id} not found.");
            }

            var todoItem = mapper.Map<TodoItemResponseDto>(todoItemDto);
            return Ok(todoItem);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isDeleted = await todoService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Todo item with id {id} not found.");
            }

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TodoItemResponseDto>> UpdateAsync(int id, [FromBody] TodoItemRequestDto todoItemRequest)
        {
            var todoItemDto = mapper.Map<TodoItemDto>(todoItemRequest);
            todoItemDto.Id = id;

            var updatedTodoItemDto = await todoService.UpdateAsync(todoItemDto);

            if (updatedTodoItemDto == null)
            {
                return NotFound($"Todo item with id {id} not found.");
            }

            var updatedTodoItem = mapper.Map<TodoItemResponseDto>(updatedTodoItemDto);

            return Ok(updatedTodoItem);
        }
    }
}