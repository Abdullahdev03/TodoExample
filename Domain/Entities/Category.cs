namespace Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TaskTodo> TaskTodos { get; set; }
    
    
    
}