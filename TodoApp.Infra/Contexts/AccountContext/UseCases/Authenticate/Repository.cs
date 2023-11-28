using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Contexts.AccountContext.Entities;
using TodoApp.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using TodoApp.Infra.Data;

namespace TodoApp.Infra.Contexts.AccountContext.UseCases.Authenticate;
public class Repository(AppDbContext context) : IRepository
{
    private readonly AppDbContext _context = context;

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) =>
        await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
}