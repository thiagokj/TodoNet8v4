using MediatR;

namespace TodoApp.Core.Contexts.TodoContext.UseCases.Update;
public record Request(Guid Id, string Title, bool IsComplete) : IRequest<Response>;