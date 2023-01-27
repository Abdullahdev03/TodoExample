using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<UserDto>>> GetUsers()
    {
        try
        {
            var result = await _context.Users.ToListAsync();
            var mapped = _mapper.Map<List<UserDto>>(result);
            return new Response<List<UserDto>>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<List<UserDto>>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }

    public async Task<Response<LogInDto>> LogIn(string username, string password)
    {
        try
        {
            var existingStudent =  _context.Users.FirstOrDefault(x => x.UserName  == username && x.Password == password);
            if (existingStudent == null)
            
                return new Response<LogInDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Username or password is incorrect" });
            

            
            var mapped = _mapper.Map<LogInDto>(existingStudent);
            return new Response<LogInDto>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<LogInDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
    public async Task<Response<UserDto>> Register(UserDto user)
    {
        try
        {
            var existingStudent = _context.Users.Where(x => x.Email == user.Email || x.Phone ==user.Phone).AsTracking().FirstOrDefault();
            if (existingStudent != null)
            {
                return new Response<UserDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "you could not registered" });
            }
            var mapped = _mapper.Map<User>(user);
            await _context.Users.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<UserDto>(user);
            // else
            // {
            //     return new Response<UserDto>(HttpStatusCode.BadRequest,
            //         new List<string>() { "You are already registered" });  
            // }
            
        }
        catch (Exception ex)
        {
            return new Response<UserDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
    
    public async Task<Response<UserDto>> UpdateUser(UserDto user)
    {
        try
        {
            var update = _context.Users.Where(x => x.Id == user.Id );
            if (update == null) return new Response<UserDto>(HttpStatusCode.BadRequest, new List<string>() { "Id not found" });

            var mapped = _mapper.Map<User>(user);
            _context.Users.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<UserDto>(user);
        }
        catch (Exception ex)
        {
            return  new Response<UserDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<string>> DeleteUser(int id)
    {
        var delete = await _context.Users.FirstAsync(x => x.Id == id);
        _context.Users.Remove(delete);
        await _context.SaveChangesAsync();
        return new Response<string>("Deleted");

    }
    
    
}