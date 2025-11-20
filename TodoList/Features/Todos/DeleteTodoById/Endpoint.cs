using Microsoft.AspNetCore.Http.HttpResults;
using TodoList.Data;

namespace TodoList.Features.Todos.DeleteTodoById
{
    public class DeleteTodoByIdEndpoint
    {
        public static async Task<Results<Ok, NotFound>> HandleAsync(int id, AppDbContext context)
        {
            var todo = await context.Todos.FindAsync(id);
            if (todo is null) return TypedResults.NotFound();

            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
            return TypedResults.Ok();
        }
    }
}