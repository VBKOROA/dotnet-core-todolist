namespace TodoList.Features.Todos;

public static class TodoEndpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder routes)
    {
        var todoGroup = routes.MapGroup("/todos");

        todoGroup.MapPost("/", CreateTodo.RequestHandler)
        .WithName("Create Todo")
        .WithOpenApi();
    }
}