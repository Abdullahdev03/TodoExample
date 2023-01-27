using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class TaskService
{
  
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public TaskService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<TaskDto>>> GetTasks()
    {
        try
        {
            var result = await _context.TaskTodos.ToListAsync();
            var mapped = _mapper.Map<List<TaskDto>>(result);
            return new Response<List<TaskDto>>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<List<TaskDto>>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<TaskDto>>  AddTask(TaskDto task)
    {
        try
        {
            var existingStudent = _context.TaskTodos.Where(x => x.Id == task.Id ).AsTracking().FirstOrDefault();
            if (existingStudent != null)
            {
                return new Response<TaskDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "you have an error to add a task" });
            }
            
            var mapped = _mapper.Map<TaskTodo>(task);
            await _context.TaskTodos.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<TaskDto>(task);
        }
        catch (Exception ex)
        {
            return new Response<TaskDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
    
    public async Task<Response<TaskDto>> UpdateTask(TaskDto task)
    {
        try
        {
            var update = _context.TaskTodos.Where(x => x.Id == task.Id );
            if (update == null) return new Response<TaskDto>(HttpStatusCode.BadRequest, new List<string>() { "Id not found" });

            var mapped = _mapper.Map<TaskTodo>(task);
            _context.TaskTodos.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<TaskDto>(task);
        }
        catch (Exception ex)
        {
            return  new Response<TaskDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<string>> DeleteTask(int id)
    {
        var delete = await _context.TaskTodos.FirstAsync(x => x.Id == id);
        _context.TaskTodos.Remove(delete);
        await _context.SaveChangesAsync();
        return new Response<string>("Deleted");

    }
    
    
}
