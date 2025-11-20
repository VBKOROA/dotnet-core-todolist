using FluentValidation;

namespace TodoList.Common.Filters
{
    public class ValidationFilter<T>(IValidator<T> validator) : IEndpointFilter where T : class
    {
        private readonly IValidator<T> _validator = validator;

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var argument = context.Arguments.FirstOrDefault(x => x?.GetType() == typeof(T));

            if (argument is not T model)
            {
                return await next(context);
            }

            var validationResult = await _validator.ValidateAsync(model);
            
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            return await next(context);
        }
    }
}
