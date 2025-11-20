

namespace TodoList.Features.Todos.Model
{
    public class Todo
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public bool IsComplete { get; private set; }

        private Todo() { }

        public Todo(string? name, bool isComplete)
        {
            Name = name;
            IsComplete = isComplete;
        }

        public void updateName(string name)
        {
            Name = name;
        }

        public void updateComplete(bool isComplete)
        {
            IsComplete = isComplete;
        }
    }
}
