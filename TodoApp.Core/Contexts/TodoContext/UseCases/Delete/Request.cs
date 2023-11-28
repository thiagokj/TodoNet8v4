using MediatR;

namespace TodoApp.Core.Contexts.TodoContext.UseCases.Delete;
public record Request(Guid Id) : IRequest<Response>;