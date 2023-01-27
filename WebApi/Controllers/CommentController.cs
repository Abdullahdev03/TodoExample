using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
[Route("Controller")]

public class CommentController: ControllerBase
{

    private readonly CommentService _commentService;
    

    [HttpGet("GetComments")]
    public async Task<Response<List<CommentDto>>> GetComments()
    {
        return await _commentService.GetComments();
    }

    [HttpPost("AddComment")]
    public async  Task<Response<CommentDto>> AddComment(CommentDto comment)
    {
        if (ModelState.IsValid)
        {
            return await _commentService.AddComment(comment);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<CommentDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }

    [HttpPut("UpdateComment")]
    public async Task<Response<CommentDto>> UpdateComment(CommentDto comment)
    {
        if (ModelState.IsValid)
        {
            return await _commentService.UpdateComment(comment);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<CommentDto>(HttpStatusCode.BadRequest, errors);
        }
    }
    
    [HttpDelete("DeleteComment")]
    public async Task<Response<string>> DeletecComment(int id)
    {
        await _commentService.DeleteTask(id);
        return new Response<string>("Deleeeted");
    }
    
}
