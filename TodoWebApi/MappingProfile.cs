using AutoMapper;
using TodoApp.Business.Dtos;
using TodoWebApi.Dtos;

namespace TodoWebApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoItemDto, TodoItemResponseDto>();
        CreateMap<TodoItemRequestDto, TodoItemDto>();            
    }
}