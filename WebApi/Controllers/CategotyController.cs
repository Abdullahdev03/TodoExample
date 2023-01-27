using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("Controller")]
public class CategotyController: ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategotyController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("GetCategories")]
    public async Task<Response<List<CategoryDto>>> GetCategoties()
    {
        return await _categoryService.GetCategories();
    }

    [HttpPost("AddCategory")]
    public async  Task<Response<CategoryDto>> AddCategory(CategoryDto category)
    {
        if (ModelState.IsValid)
        {
            return await _categoryService.AddCategoty(category);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<CategoryDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }

    [HttpPut("UpdateCategory")]
    public async Task<Response<CategoryDto>> UpdateCategory(CategoryDto category)
    {
        if (ModelState.IsValid)
        {
            return await _categoryService.UpdateCategoty(category);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<CategoryDto>(HttpStatusCode.BadRequest, errors);
        }
    }
    
    [HttpDelete("DeleteCategory")]
    public async Task<Response<string>> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategory(id);
        return new Response<string>("Deleeeted");
    }
    
}
