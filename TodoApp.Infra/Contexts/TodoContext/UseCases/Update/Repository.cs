using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Contexts.TodoContext.Entities;
using TodoApp.Core.Contexts.TodoContext.UseCases.Update.Contracts;
using TodoApp.Infra.Data;

namespace TodoApp.Infra.Contexts.TodoContext.UseCases.Update;
public class Repository(AppDbContext context) : IRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Todo?> GetByIdAsync(Guid id)
        => await _context.Todos.FindAsync(id);

    public async Task UpdateAsync(Todo todo)
    {
        var existingTodo = await
                _context
                .Todos
                .FindAsync(todo.Id);

        if (existingTodo != null)
        {
            existingTodo.Title = todo.Title;
            existingTodo.IsComplete = todo.IsComplete;

            await _context.SaveChangesAsync();
        }
    }
}

