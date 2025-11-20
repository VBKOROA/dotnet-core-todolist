using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data;

namespace TodoList.Features.Todos.EditTodoById
{
    public class EditTodoByIdEndpoint
    {
        public static async Task<Results<Ok, NotFound>> HandlerAsync(int id, [FromBody] EditTodoByIdRequest request, AppDbContext context)
        {
            var todo = await context.Todos.FindAsync(id);
            if (todo is null) return TypedResults.NotFound();
            
            if (request.Name is not null)
            {
                todo.updateName(request.Name);
            }

            if (request.IsComplete is bool isComplete)
            {
                todo.updateComplete(isComplete);
            }

            await context.SaveChangesAsync();

            return TypedResults.Ok();
        }
    }
}