using TodoApp.Core.Contexts.TodoContext.Entities;

namespace TodoApp.Core.Contexts.TodoContext.UseCases.Retrieve.Contracts;
public interface IRepository
{
    Task<Todo?> GetByIdAsync(Guid id);

    Task<List<Todo>> GetAllAsync(Guid? id = null);
}