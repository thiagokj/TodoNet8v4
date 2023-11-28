using MediatR;
using TodoApp.Core.Contexts.TodoContext.Entities;
using TodoApp.Core.Contexts.TodoContext.UseCases.Retrieve.Contracts;

namespace TodoApp.Core.Contexts.TodoContext.UseCases.Retrieve;
public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        List<Todo> todos;
        try
        {
            if (request.Id != Guid.Empty)
            {
                var todo = await _repository.GetByIdAsync(request.Id);
                if (todo == null)
                    return new Response("Tarefa não encontrada", 404);

                var responseData = new ResponseData(todo.Id, todo.Title, todo.IsComplete);
                return new Response("Tarefa recuperada", responseData);
            }

            todos = await _repository.GetAllAsync();
        }
        catch (Exception)
        {
            return new Response("Não foi possível recuperar as tarefas", 500);
        }

        var responseDataList = todos
            .Select(todo =>
                new ResponseData(todo.Id, todo.Title, todo.IsComplete))
            .ToList();

        return new Response("Todas as tarefas recuperadas", responseDataList);
    }
}