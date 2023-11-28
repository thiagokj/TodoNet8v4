using MediatR;
using TodoApp.Core.Contexts.AccountContext.Entities;
using TodoApp.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using TodoApp.Core.Contexts.AccountContext.ValueObjects;

namespace TodoApp.Core.Contexts.AccountContext.UseCases.Create;
public class Handler(IRepository repository, IService service) : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository = repository;
    private readonly IService _service = service;

    public async Task<Response> Handle(
        Request request,
        CancellationToken cancellationToken)
    {
        #region 01. Valida a requisição

        try
        {
            var specification = Specification.Ensure(request);
            if (!specification.IsValid)
                return new Response("Requisição inválida", 400, specification.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }

        #endregion

        #region 02. Gera os Objetos

        Email email;
        Password password;
        User user;

        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name, email, password);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion

        #region 03. Verifica se o usuário existe no banco

        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("Este E-mail já está em uso", 400);
        }
        catch
        {
            return new Response("Falha ao verificar E-mail cadastrado", 500);
        }

        #endregion

        #region 04. Persiste os dados

        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir dados", 500);
        }

        #endregion

        #region 05. Envia E-mail de ativação

        try
        {
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch
        {
            // Do nothing
        }

        #endregion

        return new Response(
            "Conta criada",
            new ResponseData(user.Id, user.Name, user.Email));
    }
}