using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Contexts.AccountContext.Entities;
using TodoApp.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using TodoApp.Infra.Data;

namespace TodoApp.Infra.Contexts.AccountContext.UseCases.Create;
public class Repository(AppDbContext context) : IRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken)
        => await _context
            .Users
            .AsNoTracking()
            .AnyAsync(x => x.Email.Address == email, cancellationToken: cancellationToken);

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}