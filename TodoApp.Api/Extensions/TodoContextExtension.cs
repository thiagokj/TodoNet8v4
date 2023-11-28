using MediatR;

namespace TodoApp.Api.Extensions;
public static class TodoContextExtension
{
    public static void AddTodoContext(this WebApplicationBuilder builder)
    {
        #region Create

        builder.Services.AddTransient<
            TodoApp.Core.Contexts.TodoContext.UseCases.Create.Contracts.IRepository,
            TodoApp.Infra.Contexts.TodoContext.UseCases.Create.Repository>();

        #endregion

        #region Retrieve

        builder.Services.AddTransient<
            TodoApp.Core.Contexts.TodoContext.UseCases.Retrieve.Contracts.IRepository,
            TodoApp.Infra.Contexts.TodoContext.UseCases.Retrieve.Repository>();

        #endregion

        #region Update

        builder.Services.AddTransient<
            TodoApp.Core.Contexts.TodoContext.UseCases.Update.Contracts.IRepository,
            TodoApp.Infra.Contexts.TodoContext.UseCases.Update.Repository>();

        #endregion

        #region Delete

        builder.Services.AddTransient<
            TodoApp.Core.Contexts.TodoContext.UseCases.Delete.Contracts.IRepository,
            TodoApp.Infra.Contexts.TodoContext.UseCases.Delete.Repository>();

        #endregion
    }

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

        #region Retrieve

        app.MapGet("api/v1/todos/{id?}", async (
            Guid? id,
            IRequestHandler<
                TodoApp.Core.Contexts.TodoContext.UseCases.Retrieve.Request,
                TodoApp.Core.Contexts.TodoContext.UseCases.Retrieve.Response> handler) =>
        {
            var requestId = id ?? Guid.Empty; // Se id for nulo, atribui Guid.Empty
            var request = new TodoApp.Core.Contexts.TodoContext.UseCases.Retrieve.Request(requestId);

            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.Ok(result)
                : Results.Json(result, statusCode: result.Status);
        });

        #endregion

        #region Update

        app.MapPut("api/v1/todos", async (
            TodoApp.Core.Contexts.TodoContext.UseCases.Update.Request request,
            IRequestHandler<
                TodoApp.Core.Contexts.TodoContext.UseCases.Update.Request,
                TodoApp.Core.Contexts.TodoContext.UseCases.Update.Response> handler) =>
        {
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
              ? Results.Ok(result)
              : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion

        #region Delete

        app.MapDelete("api/v1/todos/{id}", async (
            Guid id,
            IRequestHandler<
                TodoApp.Core.Contexts.TodoContext.UseCases.Delete.Request,
                TodoApp.Core.Contexts.TodoContext.UseCases.Delete.Response> handler) =>
        {
            var request = new TodoApp.Core.Contexts.TodoContext.UseCases.Delete.Request(id);
            var result = await handler.Handle(request, new CancellationToken());
            return result.IsSuccess
                ? Results.NoContent()
                : Results.Json(result, statusCode: result.Status);
        }).RequireAuthorization();

        #endregion
    }
}