using System.Data;
using TodoApp.Core.Contexts.SharedContext.Entities;

namespace TodoApp.Core.Contexts.TodoContext.Entities;
public class Todo : Entity
{
    protected Todo()
    {
    }

    public Todo(string title, bool isComplete)
    {
        Title = title;
        IsComplete = isComplete;
    }

    public string Title { get; set; } = string.Empty;
    public bool IsComplete { get; set; } = false;
}