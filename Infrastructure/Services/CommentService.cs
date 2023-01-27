using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class CommentService
{
    
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CommentService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<CommentDto>>> GetComments()
    {
        try
        {
            var result = await _context.Comments.ToListAsync();
            var mapped = _mapper.Map<List<CommentDto>>(result);
            return new Response<List<CommentDto>>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<List<CommentDto>>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<CommentDto>>  AddComment(CommentDto comment)
    {
        try
        {
            var existingStudent = _context.Comments.Where(x => x.Id != comment.Id ).AsTracking().FirstOrDefault();
            if (existingStudent != null)
            {
                return new Response<CommentDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "you cant add a comment" });
            }
            
            var mapped = _mapper.Map<Comment>(comment);
            await _context.Comments.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<CommentDto>(comment);
        }
        catch (Exception ex)
        {
            return new Response<CommentDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
    
    public async Task<Response<CommentDto>> UpdateComment(CommentDto comment)
    {
        try
        {
            var update = _context.TaskTodos.Where(x => x.Id == comment.Id );
            if (update == null) return new Response<CommentDto>(HttpStatusCode.BadRequest, new List<string>() { "Id not found" });

            var mapped = _mapper.Map<Comment>(comment);
            _context.Comments.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<CommentDto>(comment);
        }
        catch (Exception ex)
        {
            return  new Response<CommentDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<string>> DeleteTask(int id)
    {
        var delete = await _context.Comments.FirstAsync(x => x.Id == id);
        _context.Comments.Remove(delete);
        await _context.SaveChangesAsync();
        return new Response<string>("Deleted");

    }
    
    
}
