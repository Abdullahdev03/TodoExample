namespace Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext: DbContext
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users {get; set;}
    public DbSet<TaskTodo> TaskTodos {get; set;}
    public DbSet<Comment> Comments {get; set;}
    public DbSet<Category> Categories {get; set;}
  
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<EmployeeJob>().HasKey(es => new
        // {
        //     es.EmployeeId, es.JobId
        // });
        // base.OnModelCreating(modelBuilder);
    }

  
}
