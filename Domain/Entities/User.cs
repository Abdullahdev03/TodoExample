using System.Net.Sockets;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string UserName { get; set; }
    public string Address  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public int TaskTodoId { get; set; }
    public TaskTodo TaskTodos { get; set; }
    
}