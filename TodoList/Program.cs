using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var todoGroup = app.MapGroup("/todos");

todoGroup.MapPost("/", async Task<Created<Todo>> (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/todos/{todo.Id}", todo);
})
.WithName("Create Todo")
.WithOpenApi();

todoGroup.MapGet("/{id}", async Task<Results<Ok<Todo>, NotFound>> (int id, TodoDb todoDb) =>
{
    return await todoDb.Todos.FindAsync(id) is Todo todo
        ? TypedResults.Ok(todo)
        : TypedResults.NotFound();
})
.WithName("Get Todo")
.WithOpenApi();

todoGroup.MapPut("/", async Task<Results<NotFound, NoContent>> (Todo requestTodo, TodoDb todoDb) =>
{
    var todo = await todoDb.Todos.FindAsync(requestTodo.Id);

    if (todo is null) return TypedResults.NotFound();

    todo.Name = requestTodo.Name;
    todo.IsComplete = requestTodo.IsComplete;

    await todoDb.SaveChangesAsync();

    return TypedResults.NoContent();
})
.WithName("Update Todo")
.WithOpenApi();

todoGroup.MapGet("/complete", async Task<Ok<List<Todo>>> (TodoDb todoDb) => TypedResults.Ok(await todoDb.Todos.Where(t => t.IsComplete).ToListAsync()))
.WithName("Get Complete Todos")
.WithOpenApi();

todoGroup.MapGet("/", async Task<Ok<List<Todo>>> (TodoDb todoDb) => TypedResults.Ok(await todoDb.Todos.ToListAsync()))
.WithName("Get Todos")
.WithOpenApi();

todoGroup.MapDelete("/{id}", async Task<Results<NotFound, NoContent>> (int id, TodoDb todoDb) =>
{
    var todo = await todoDb.Todos.FindAsync(id);

    if (todo is null) return TypedResults.NotFound();

    todoDb.Todos.Remove(todo);
    await todoDb.SaveChangesAsync();
    return TypedResults.NoContent();
})
.WithName("Delete Todo")
.WithOpenApi();

app.Run();