using Microsoft.EntityFrameworkCore;
using TodoList.Features.Todos.Model;

namespace TodoList.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos => Set<Todo>();
}