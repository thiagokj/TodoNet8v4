using TodoApp.Core.Contexts.TodoContext.Entities;

namespace TodoApp.Core.Contexts.TodoContext.UseCases.Update.Contracts;
public interface IRepository
{
    Task<Todo?> GetByIdAsync(Guid id);
    Task UpdateAsync(Todo todo);
}