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

app.MapPost("/todos", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", todo);
})
.WithName("Create Todo")
.WithOpenApi();

app.MapGet("/todos/{id}", async (int id, TodoDb todoDb) =>
{
    return await todoDb.Todos.FindAsync(id) is Todo todo
        ? Results.Ok(todo)
        : Results.NotFound();
})
.WithName("Get Todo")
.WithOpenApi();

app.MapPut("/todos", async (Todo requestTodo, TodoDb todoDb) =>
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

app.MapGet("/todos/complete", async (TodoDb todoDb) => await todoDb.Todos.Where(t => t.IsComplete).ToListAsync());

app.MapGet("/todos", async (TodoDb todoDb) => await todoDb.Todos.ToListAsync())
.WithName("Get Todos")
.WithOpenApi();

app.Run();