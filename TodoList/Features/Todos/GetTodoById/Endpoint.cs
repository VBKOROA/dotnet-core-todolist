using Microsoft.AspNetCore.Http.HttpResults;
using TodoList.Data;
using TodoList.Features.Todos.Model;

namespace TodoList.Features.Todos.GetTodoById
{
    public class GetTodoByIdEndpoint
    {
        public static async Task<Results<Ok<GetTodoByIdResponse>, NotFound>> HandlerAsync(int id, AppDbContext context)
        {
            var todo = await context.Todos.FindAsync(id);
            if (todo is null) return TypedResults.NotFound();

            var response = GetTodoByIdResponse.FromModel(todo);

            return TypedResults.Ok(response);
        }
    }
}