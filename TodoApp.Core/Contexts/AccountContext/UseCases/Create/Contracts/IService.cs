using TodoApp.Core.Contexts.AccountContext.Entities;

namespace TodoApp.Core.Contexts.AccountContext.UseCases.Create.Contracts;
public interface IService
{
    Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
}