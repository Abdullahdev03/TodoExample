using System.Security.AccessControl;

namespace Domain.Entities;

public class Comment
{
    public int Id { get; set; }    
    public string CommentText { get; set; }
    public int TaskTodoId { get; set; }
    public TaskTodo TaskTodos { get; set; }
    
    
}