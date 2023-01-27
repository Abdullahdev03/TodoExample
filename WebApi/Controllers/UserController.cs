using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("Controller")]
public class UserController: ControllerBase
{

    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetUsers")]
    public async Task<Response<List<UserDto>>> GetUsers()
    {
        return await _userService.GetUsers();
    }
    [HttpGet("LogIn")]
    public async Task<Response<LogInDto>> LogIn(string username, string password)
    {
        return await _userService.LogIn(username, password);
    }

    [HttpPost("AddRegistr")]
    public async  Task<Response<UserDto>> Register(UserDto user)
    {
        if (ModelState.IsValid)
        {
            return await _userService.Register(user);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<UserDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }

    [HttpPut("UpdateUser")]
    public async Task<Response<UserDto>> UpdateUser(UserDto user)
    {
        if (ModelState.IsValid)
        {
            return await _userService.UpdateUser(user);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<UserDto>(HttpStatusCode.BadRequest, errors);
        }
    }
    
    [HttpDelete("DeleteUser")]
    public async Task<Response<string>> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);
        return new Response<string>("Deleeeted");
    }
    
}