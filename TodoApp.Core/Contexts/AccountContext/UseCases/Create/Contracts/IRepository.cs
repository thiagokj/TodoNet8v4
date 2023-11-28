using TodoApp.Core.Contexts.AccountContext.Entities;

namespace TodoApp.Core.Contexts.AccountContext.UseCases.Create.Contracts;
public interface IRepository
{
    Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
    Task SaveAsync(User user, CancellationToken cancellationToken);
}