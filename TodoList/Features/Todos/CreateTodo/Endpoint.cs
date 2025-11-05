using Microsoft.AspNetCore.Http.HttpResults;
using TodoList.Data;
using TodoList.Features.Todos.Model;

namespace TodoList.Features.Todos.CreateTodo
{
    public class CreateTodoEndpoint
    {

        public static async Task<Created<Todo>> HandlerAsync(CreateTodoRequest request, AppDbContext context)
        {
            var todo = new Todo(request.Name, request.IsComplete);
            context.Todos.Add(todo);
            await context.SaveChangesAsync();
            return TypedResults.Created($"/todos/{todo.Id}", todo);
        }
    }
}