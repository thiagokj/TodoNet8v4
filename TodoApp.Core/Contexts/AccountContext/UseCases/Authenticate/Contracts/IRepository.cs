using TodoApp.Core.Contexts.AccountContext.Entities;

namespace TodoApp.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}