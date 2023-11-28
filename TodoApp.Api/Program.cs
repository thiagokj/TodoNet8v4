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
app.UseAuthentication();
app.UseAuthorization();

app.MapTodoEndpoints();
app.MapAccountEndpoints();

app.Run();
