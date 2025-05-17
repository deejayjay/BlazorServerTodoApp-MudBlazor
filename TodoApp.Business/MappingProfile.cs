using AutoMapper;
using TodoApp.Business.Dtos;
using TodoApp.Data.Entities;

namespace TodoApp.Business;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoItem, TodoItemDto>().ReverseMap();
    }
}