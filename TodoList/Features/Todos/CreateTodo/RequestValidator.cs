using FluentValidation;

namespace TodoList.Features.Todos.CreateTodo
{
    public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
    {
        public CreateTodoRequestValidator()
        {
            RuleFor(x => x.Name).MaximumLength(20).MinimumLength(1);
        }
    }
}
