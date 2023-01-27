using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Services;

namespace Infrastructure.MapperProfiles;

public class InfrastructureProfile: Profile
{
    public InfrastructureProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<User, LogInDto>();
        CreateMap<TaskTodo, TaskDto>();
        CreateMap<TaskDto, TaskTodo>();
        CreateMap<Comment, CommentDto>();
        CreateMap<CommentDto, Comment>();
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
        
    }
}