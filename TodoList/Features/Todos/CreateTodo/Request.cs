namespace TodoList.Features.Todos.CreateTodo
{
    public record CreateTodoRequest(string? Name, bool IsComplete);
}