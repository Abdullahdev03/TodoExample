namespace Domain.Entities;

public class TaskTodo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }   // 01.01.0001 
    public DateTime CreationDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public int CategoryId { get; set; }
    public Category Categories { get; set; }
    public List<User> Users { get; set; }
    public List<Comment> Comments { get; set; }
    
}