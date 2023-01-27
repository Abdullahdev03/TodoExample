namespace Domain.Dtos;

public class CommentDto
{
    public int Id { get; set; }    
    public string CommentText { get; set; }
    public int TaskTodoId { get; set; }
}