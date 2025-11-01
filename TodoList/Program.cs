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

todoGroup.MapPost("/", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", todo);
})
.WithName("Create Todo")
.WithOpenApi();

todoGroup.MapGet("/{id}", async (int id, TodoDb todoDb) =>
{
    return await todoDb.Todos.FindAsync(id) is Todo todo
        ? Results.Ok(todo)
        : Results.NotFound();
})
.WithName("Get Todo")
.WithOpenApi();

todoGroup.MapPut("/", async (Todo requestTodo, TodoDb todoDb) =>
{
    var todo = await todoDb.Todos.FindAsync(requestTodo.Id);

    if (todo is null) return Results.NotFound();

    todo.Name = requestTodo.Name;
    todo.IsComplete = requestTodo.IsComplete;

    await todoDb.SaveChangesAsync();

    return Results.NoContent();
})
.WithName("Update Todo")
.WithOpenApi();

todoGroup.MapGet("/complete", async (TodoDb todoDb) => await todoDb.Todos.Where(t => t.IsComplete).ToListAsync())
.WithName("Get Complete Todos")
.WithOpenApi();

todoGroup.MapGet("/", async (TodoDb todoDb) => await todoDb.Todos.ToListAsync())
.WithName("Get Todos")
.WithOpenApi();

todoGroup.MapDelete("/{id}", async (int id, TodoDb todoDb) =>
{
    var todo = await todoDb.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todoDb.Todos.Remove(todo);
    await todoDb.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("Delete Todo")
.WithOpenApi();

app.Run();