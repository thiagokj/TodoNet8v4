# Todo NET8 v4 - Minimal API

Olá Dev! 😎

Esse projeto é a continuidade do projeto [v3][v3].

Vamos adicionar o uso de autenticação e autorização para acesso as rotas.

Passos:

1. API - Adicione as rotas o método para exigir autenticação e autorização.
1. API - Adicione o uso de autenticação e autorização no arquivo de inicialização da API.

## API

### Atualizando o TodoContextExtension

1. Adicione o método **.RequireAuthorization()** no fim da rota, exigindo a autorização para acesso.

   ```csharp
   public static void MapTodoEndpoints(this WebApplication app)
       {
           #region Create

           app.MapPost("api/v1/todos", async (
               TodoApp.Core.Contexts.TodoContext.UseCases.Create.Request request,
               IRequestHandler<
                   TodoApp.Core.Contexts.TodoContext.UseCases.Create.Request,
                   TodoApp.Core.Contexts.TodoContext.UseCases.Create.Response> handler) =>
           {
               var result = await handler.Handle(request, new CancellationToken());
               return result.IsSuccess
                 ? Results.Created($"api/v1/todos/{result.Data?.Id}", result)
                 : Results.Json(result, statusCode: result.Status);
           }).RequireAuthorization();

           #endregion
        //...
       }
   ```

   Por padrão, será exigido o token de acesso na requisição para permitir o acesso.

### Atualizando o Program.cs

1. Atualize o arquivo de inicialização:

   ```csharp
   using TodoApp.Api;
   using TodoApp.Api.Extensions;

   var builder = WebApplication.CreateBuilder(args);
   builder.AddConfiguration();
   builder.AddDatabase();
   builder.AddJwtAuthentication();
   builder.AddTodoContext();
   builder.AddAccountContext();

   builder.AddMediator();

   var app = builder.Build();

   // Instruções para habilitar o acesso controlado
   app.UseAuthentication();
   app.UseAuthorization();

   app.MapTodoEndpoints();
   app.MapAccountEndpoints();

   app.Run();
   ```

### Rodando a Web API

Testando a rota de criação de tarefa sem o token de autenticação. O acesso não é permitido.

![CreateTodoRoute][CreateTodoRoute]

Gerado token válido e informado no cabeçalho.

![AuthWithToken][AuthWithToken]

Testando novamente a rota de criação de tarefa, passando no cabeçalho o token de autenticação válido.

![AuthOK][AuthOK]

### Por enquanto, é isso aí. Bons estudos e bons códigos! 👍

[v3]: https://github.com/thiagokj/TodoNet8v3
[CreateTodoRoute]: Doc/auth-unauthorized.png
[AuthWithToken]: Doc/auth-with-token.png
[AuthOK]: Doc/auth-ok.png
