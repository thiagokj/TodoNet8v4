using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Contexts.TodoContext.Entities;
using TodoApp.Core.Contexts.TodoContext.UseCases.Retrieve.Contracts;
using TodoApp.Infra.Data;

namespace TodoApp.Infra.Contexts.TodoContext.UseCases.Retrieve;

public class Repository(AppDbContext context) : IRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Todo?> GetByIdAsync(Guid id) =>
        await _context
            .Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<Todo>> GetAllAsync(Guid? id = null)
    {
        IQueryable<Todo> query = _context.Todos.AsNoTracking();

        if (id.HasValue)
            query = query.Where(x => x.Id == id);

        return await query.ToListAsync();
    }
}
