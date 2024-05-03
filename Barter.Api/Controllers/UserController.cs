using AutoMapper;
using Barter.Application.CustomExceptions;
using Barter.Application.Models.Request;
using Barter.Application.Models.Response;
using Barter.Application.Service.Interface;
using Barter.Domain.Models;
using Barter.Domain.Models.Search.Context;
using Microsoft.AspNetCore.Mvc;

namespace Barter.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController(IUserService userService, IMapper mapper) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<Guid> AddUser([FromBody] CreateUserRequest user)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("The provided model is not valid.");
        }

        var addUser = await _userService.AddUserAsync(_mapper.Map<User>(user));

        return addUser;
    }

    [HttpGet("find/{id}")]
    public async Task<UserResponse> GetUserById(Guid id)
    {
        var searchContext = new UserSearchContext { Id = id };

        var getUserById = await _userService.SearchUserWithPagingAsync(searchContext);

        return _mapper.Map<UserResponse>(getUserById.Items.SingleOrDefault());
    }

    [HttpGet("{userName}")]
    public async Task<UserResponse> GetUserByUserName(string userName)
    {
        var searchContext = new UserSearchContext { Username = userName };

        var getUserByName = await _userService.SearchUserWithPagingAsync(searchContext);

        return _mapper.Map<UserResponse>(getUserByName.Items.SingleOrDefault());
    }

    [HttpGet]
    public async Task<List<UserResponse>> GetAllUsers([FromQuery] UserSearchContext searchContext)
    {
        var getAllUsers = await _userService.SearchUserWithPagingAsync(searchContext);

        return _mapper.Map<List<UserResponse>>(getAllUsers.Items);
    }

    [HttpPut]
    public async Task<UserResponse> UpdateUser([FromBody] UpdateUserRequest user)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("The provided model is not valid.");
        }

        var updateUser = await _userService.UpdateUserAsync(_mapper.Map<User>(user));

        return _mapper.Map<UserResponse>(updateUser);
    }

    [HttpDelete]
    public async Task DeleteUser(string userName)
    {
        await _userService.DeleteUserAsync(userName);
    }
}
