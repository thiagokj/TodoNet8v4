using MediatR;
using TodoApp.Core.Contexts.AccountContext.Entities;
using TodoApp.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;

namespace TodoApp.Core.Contexts.AccountContext.UseCases.Authenticate;
public class Handler(IRepository repository) : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
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

        #region 02. Recupera o perfil

        User? user;
        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                return new Response("Perfil não encontrado", 404);
        }
        catch (Exception)
        {
            return new Response("Não foi possível recuperar seu perfil", 500);
        }

        #endregion

        #region 03. Checa se a senha é válida

        if (!user.Password.Challenge(request.Password))
            return new Response("Usuário ou senha inválidos", 400);

        #endregion

        #region 04. Checa se a conta está verificada

        try
        {
            if (!user.Email.Verification.IsActive)
                return new Response("Conta inativa", 400);
        }
        catch
        {
            return new Response("Não foi possível verificar seu perfil", 500);
        }

        #endregion

        #region 05. Retorna os dados

        try
        {
            var data = new ResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles.Select(x => x.Name).ToArray()
            };

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Não foi possível obter os dados do perfil", 500);
        }

        #endregion
    }
}