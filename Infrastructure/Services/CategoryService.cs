using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class CategoryService
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CategoryService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<CategoryDto>>> GetCategories()
    {
        try
        {
            var result = await _context.Categories.ToListAsync();
            var mapped = _mapper.Map<List<CategoryDto>>(result);
            return new Response<List<CategoryDto>>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<List<CategoryDto>>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<CategoryDto>>  AddCategoty(CategoryDto category)
    {
        try
        {
            var existingStudent = _context.Categories.Where(x => x.Id == category.Id ).AsTracking().FirstOrDefault();
            if (existingStudent != null)
            {
                return new Response<CategoryDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "You could not add a category becouse this id already exsist " });
            }
            
            var mapped = _mapper.Map<Category>(category);
            await _context.Categories.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<CategoryDto>(category);
        }
        catch (Exception ex)
        {
            return new Response<CategoryDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
    
    public async Task<Response<CategoryDto>> UpdateCategoty(CategoryDto category)
    {
        try
        {
            var update = _context.TaskTodos.Where(x => x.Id == category.Id );
            if (update == null) return new Response<CategoryDto>(HttpStatusCode.BadRequest, new List<string>() { "Id not found" });

            var mapped = _mapper.Map<Category>(category);
            _context.Categories.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<CategoryDto>(category);
        }
        catch (Exception ex)
        {
            return  new Response<CategoryDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<string>> DeleteCategory(int id)
    {
        var delete = await _context.Categories.FirstAsync(x => x.Id == id);
        _context.Categories.Remove(delete);
        await _context.SaveChangesAsync();
        return new Response<string>("Deleted");

    }
    
    
}
