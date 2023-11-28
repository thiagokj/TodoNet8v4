using MediatR;

namespace TodoApp.Core.Contexts.TodoContext.UseCases.Retrieve;

public record Request(Guid Id) : IRequest<Response>;