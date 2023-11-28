namespace TodoApp.Core.Contexts.TodoContext.UseCases.Delete.Contracts;
public interface IRepository
{
    Task DeleteAsync(Guid id);
}