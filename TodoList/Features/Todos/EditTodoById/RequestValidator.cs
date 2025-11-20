using FluentValidation;

namespace TodoList.Features.Todos.EditTodoById
{
    public class EditTodoByIdRequestValidator : AbstractValidator<EditTodoByIdRequest>
    {
        public EditTodoByIdRequestValidator()
        {
            RuleFor(x => x)
                .Must(r => r.Name is not null || r.IsComplete is not null)
                .WithMessage("Name 또는 IsComplete 중 최소 하나는 제공되어야 합니다.");
        }
    }
}