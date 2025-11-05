namespace TodoList.Features.Todos.CreateTodo
{
    public class CreateTodoRequest
    {
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}