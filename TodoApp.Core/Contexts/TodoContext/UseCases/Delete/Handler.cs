using MediatR;
using TodoApp.Core.Contexts.TodoContext.UseCases.Delete.Contracts;

namespace TodoApp.Core.Contexts.TodoContext.UseCases.Delete;
public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {

        #region 01. Exclui a tarefa

        try
        {
            if (request.Id == Guid.Empty)
                return new Response("Id não informado", 404);

            await _repository.DeleteAsync(request.Id);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        return new Response(
            "Tarefa excluída",
            new ResponseData(request.Id));
    }
}