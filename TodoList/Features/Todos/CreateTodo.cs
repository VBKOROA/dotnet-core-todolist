using Microsoft.AspNetCore.Http.HttpResults;
using TodoList.Data;

namespace TodoList.Features.Todos;

public class CreateTodo
{
    public record Request(string? Name, bool IsComplete);

    public static async Task<Created<Todo>> RequestHandler(Request request, AppDbContext context)
    {
        var todo = new Todo(request.Name, request.IsComplete);
        context.Todos.Add(todo);
        await context.SaveChangesAsync();
        return TypedResults.Created($"/todos/{todo.Id}", todo);
    }
}