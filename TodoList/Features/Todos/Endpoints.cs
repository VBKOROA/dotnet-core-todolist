using TodoList.Common.Filters;
using TodoList.Features.Todos.CreateTodo;
using TodoList.Features.Todos.DeleteTodoById;
using TodoList.Features.Todos.EditTodoById;
using TodoList.Features.Todos.GetTodoById;

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

            todoGroup.MapGet("/{id}", GetTodoByIdEndpoint.HandlerAsync)
            .WithName("Get Todo By Id")
            .WithOpenApi();

            todoGroup.MapPut("/{id}", EditTodoByIdEndpoint.HandlerAsync)
            .WithName("Edit Todo By Id")
            .WithOpenApi()
            .AddEndpointFilter<ValidationFilter<EditTodoByIdRequest>>();

            todoGroup.MapDelete("/{id}", DeleteTodoByIdEndpoint.HandleAsync)
            .WithName("Delete Todo By Id")
            .WithOpenApi();
        }
    }
}

