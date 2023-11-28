using MediatR;
using TodoApp.Core.Contexts.TodoContext.Entities;
using TodoApp.Core.Contexts.TodoContext.UseCases.Update.Contracts;

namespace TodoApp.Core.Contexts.TodoContext.UseCases.Update;
public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Valida a requisição

        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }

        #endregion

        #region 02. Recupera a tarefa

        Todo? todo;

        try
        {
            if (request.Id == Guid.Empty)
                return new Response("Id não informado", 404);

            todo = await _repository.GetByIdAsync(request.Id);
            if (todo == null)
                return new Response("Tarefa não encontrada", 404);

            todo.Title = request.Title;
            todo.IsComplete = request.IsComplete;
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        #region 03. Persiste os dados

        try
        {
            await _repository.UpdateAsync(todo);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        #endregion

        return new Response(
            "Tarefa atualizada",
            new ResponseData(todo.Id, todo.Title, todo.IsComplete));
    }
}