using TodoList.Features.Todos.Model;

namespace TodoList.Features.Todos.GetTodoById
{
    public class GetTodoByIdResponse(int id, string? name, bool isComplete)
    {
        public int Id { get; init; } = id;
        public string? Name { get; init; } = name;
        public bool IsComplete { get; init; } = isComplete;

        public static GetTodoByIdResponse FromModel(Todo todo)
        {
            return new GetTodoByIdResponse(todo.Id, todo.Name, todo.IsComplete);
        }
    }
}