using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;

namespace TodoList.Features.Todos.DeleteTodoById
{
    public class DeleteTodoByIdEndpoint
    {
        public static async Task<Results<Ok, NotFound>> HandleAsync(int id, AppDbContext context)
        {

            // 첫 번째 방법
            // var todo = await context.Todos.FindAsync(id);
            // if (todo is null) return TypedResults.NotFound();

            // context.Todos.Remove(todo);
            // await context.SaveChangesAsync();
            // return TypedResults.Ok();

            // 두 번째 방법
            var affected = await context.Todos
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            if (affected == 0) return TypedResults.NotFound();
            return TypedResults.Ok();
        }
    }
}