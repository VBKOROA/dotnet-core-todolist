using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Features.Todos;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapTodoEndpoints();

// var todoGroup = app.MapGroup("/todos");

// todoGroup.MapPost("/", async Task<Created<Todo>> (Todo todo, AppDbContext context) =>
// {
//     context.Todos.Add(todo);
//     await context.SaveChangesAsync();
//     return TypedResults.Created($"/todos/{todo.Id}", todo);
// })
// .WithName("Create Todo")
// .WithOpenApi();

// todoGroup.MapGet("/{id}", async Task<Results<Ok<Todo>, NotFound>> (int id, AppDbContext context) =>
// {
//     return await context.Todos.FindAsync(id) is Todo todo
//         ? TypedResults.Ok(todo)
//         : TypedResults.NotFound();
// })
// .WithName("Get Todo")
// .WithOpenApi();

// todoGroup.MapPut("/", async Task<Results<NotFound, NoContent>> (Todo requestTodo, AppDbContext context) =>
// {
//     var todo = await context.Todos.FindAsync(requestTodo.Id);

//     if (todo is null) return TypedResults.NotFound();

//     todo.Name = requestTodo.Name;
//     todo.IsComplete = requestTodo.IsComplete;

//     await context.SaveChangesAsync();

//     return TypedResults.NoContent();
// })
// .WithName("Update Todo")
// .WithOpenApi();

// todoGroup.MapGet("/complete", async Task<Ok<List<Todo>>> (AppDbContext context) => TypedResults.Ok(await context.Todos.Where(t => t.IsComplete).ToListAsync()))
// .WithName("Get Complete Todos")
// .WithOpenApi();

// todoGroup.MapGet("/", async Task<Ok<List<Todo>>> (AppDbContext context) => TypedResults.Ok(await context.Todos.ToListAsync()))
// .WithName("Get Todos")
// .WithOpenApi();

// todoGroup.MapDelete("/{id}", async Task<Results<NotFound, NoContent>> (int id, AppDbContext context) =>
// {
//     var todo = await context.Todos.FindAsync(id);

//     if (todo is null) return TypedResults.NotFound();

//     context.Todos.Remove(todo);
//     await context.SaveChangesAsync();
//     return TypedResults.NoContent();
// })
// .WithName("Delete Todo")
// .WithOpenApi();

app.Run();