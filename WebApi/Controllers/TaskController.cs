using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("Controller")]
public class TaskController: ControllerBase
{

    private readonly TaskService _taskService;

    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("GetTasks")]
    public async Task<Response<List<TaskDto>>> GetTasks()
    {
        return await _taskService.GetTasks();
    }

    [HttpPost("AddTask")]
    public async  Task<Response<TaskDto>> AddTask(TaskDto task)
    {
        if (ModelState.IsValid)
        {
            return await _taskService.AddTask(task);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<TaskDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }

    [HttpPut("UpdateTask")]
    public async Task<Response<TaskDto>> UpdateTask(TaskDto task)
    {
        if (ModelState.IsValid)
        {
            return await _taskService.UpdateTask(task);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<TaskDto>(HttpStatusCode.BadRequest, errors);
        }
    }
    
    [HttpDelete("DeleteTask")]
    public async Task<Response<string>> DeleteTask(int id)
    {
        await _taskService.DeleteTask(id);
        return new Response<string>("Deleeeted");
    }
    
}
