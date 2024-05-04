using AutoMapper;
using Barter.Application.CustomExceptions;
using Barter.Application.Models.Request;
using Barter.Application.Models.Response;
using Barter.Application.Service.Interface;
using Barter.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Barter.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController(IUserService userService, IMapper mapper) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUserAsync(CreateUserRequest request)
    {
        try
        {
            var user = _mapper.Map<User>(request);
            user = await _userService.AddUserAsync(user);
            var response = _mapper.Map<UserResponse>(user);
            return CreatedAtAction(nameof(GetUserByUserNameAsync), new { userName = response.Username }, response);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{userName}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUserAsync(string userName)
    {
        try
        {
            var user = await _userService.GetUserByUserNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.RemoveUserAsync(user);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{userName}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUserAsync(string userName, UpdateUserRequest request)
    {
        try
        {
            var existingUser = await _userService.GetUserByUserNameAsync(userName);
            if (existingUser == null)
            {
                return NotFound();
            }

            var user = _mapper.Map<User>(request);
            user.UserName = userName;
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        var response = _mapper.Map<List<UserResponse>>(users);
        return Ok(response);
    }

    [HttpGet("{userName}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserByUserNameAsync(string userName)
    {
        var user = await _userService.GetUserByUserNameAsync(userName);
        if (user == null)
        {
            return NotFound();
        }

        var response = _mapper.Map<UserResponse>(user);
        return Ok(response);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUpAsync(CreateUserRequest request)
    {
        try
        {
            var user = await _userService.SignUpAsync(request.Username, request.Email, request.Password);
            var response = _mapper.Map<UserResponse>(user);
            return Ok(response);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInAsync(SignInRequest request)
    {
        try
        {
            await _userService.SignInAsync(_mapper.Map<User>(request), request.Password);
            return Ok("Sign-in successful");
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid username or password");
        }
    }

    [HttpPost("signout")]
    public async Task<IActionResult> SignOutAsync()
    {
        await _userService.SignOutAsync();
        return Ok("Sign-out successful");
    }

    [HttpPost("assign-role")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignRoleToUser(AssignRoleRequest request)
    {
        try
        {
            var success = await _userService.AssignRoleToUser(request.UserName, request.RoleName);
            if (success)
            {
                return Ok("Role assigned successfully");
            }
            else
            {
                return BadRequest("Failed to assign role");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
