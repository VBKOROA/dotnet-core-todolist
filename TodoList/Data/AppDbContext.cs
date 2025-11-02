using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos => Set<Todo>();
}