using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Entities;

namespace TodoApp.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems { get; set; }
}