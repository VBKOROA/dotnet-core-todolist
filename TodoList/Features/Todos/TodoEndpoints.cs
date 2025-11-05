using TodoList.Common.Filters;
using TodoList.Features.Todos.CreateTodo;

namespace TodoList.Features.Todos
{
    public static class TodoEndpoints
    {
        public static void MapTodoEndpoints(this IEndpointRouteBuilder routes)
        {
            var todoGroup = routes.MapGroup("/todos");

            todoGroup.MapPost("/", CreateTodoEndpoint.HandlerAsync)
            .WithName("Create Todo")
            .WithOpenApi()
            .AddEndpointFilter<ValidationFilter<CreateTodoRequest>>();
        }
    }
}

