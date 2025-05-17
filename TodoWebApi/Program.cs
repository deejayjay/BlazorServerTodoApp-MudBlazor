using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TodoApp.Business.Services;
using TodoApp.Business.Services.Interfaces;
using TodoApp.Data;
using TodoApp.Data.Repositories;
using TodoApp.Data.Repositories.Interfaces;
using TodoWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlazorTodoDb")));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(TodoApp.Business.MappingProfile));

// Repositories
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

// Services
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "TODO API";
        options.ShowSidebar = true;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();