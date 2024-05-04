using AutoMapper;
using Barter.Application.CustomExceptions;
using Barter.Application.Service.Interface;
using Barter.Application.UnitOfWork;
using Barter.Domain.Models;
using Barter.Domain.Models.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Barter.Application.Service;

internal class UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IMapper _mapper = mapper;

    public async Task<User> AddUserAsync(User user)
    {
        var applicationUser = _mapper.Map<User>(user);
        var result = await _userManager.CreateAsync(applicationUser, user.Password);

        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors.FirstOrDefault().Description);
        }

        var userEntity = await _userManager.FindByNameAsync(user.UserName);

        var isInRole = await _userManager.IsInRoleAsync(userEntity, user.Roles.ToString());
        if (!isInRole)
        {
            var roleAssignmentResult = await _userManager.AddToRoleAsync(userEntity, user.Roles.ToString());
            if (!roleAssignmentResult.Succeeded)
            {
                throw new Exception("Failed to assign role to user.");
            }
        }

        return user;
    }

    public async Task RemoveUserAsync(User user)
    {
        var applicationUser = await _userManager.FindByNameAsync(user.UserName);
        if (applicationUser == null)
        {
            throw new NotFoundException("User not found");
        }

        await _userManager.DeleteAsync(applicationUser);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        var applicationUser = await _userManager.FindByNameAsync(user.UserName);
        if (applicationUser == null)
        {
            throw new NotFoundException("User not found");
        }

        applicationUser.Email = user.Email;
        await _userManager.UpdateAsync(applicationUser);
        await _unitOfWork.SaveAsync();
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        var applicationUser = await _userManager.FindByNameAsync(userName);
        return _mapper.Map<User>(applicationUser);
    }

    public async Task<User> SignUpAsync(string userName, string email, string password)
    {
        var user = new User { UserName = userName, Email = email };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors.First().Description);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        return _mapper.Map<User>(user);
    }

    // Need To Solve Check if user already signed in
    public async Task SignInAsync(User user, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return _mapper.Map<List<User>>(users);
    }

    public async Task<bool> AssignRoleToUser(string userName, UserRoles roleName)
    {
        try
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName.ToString());
            if (!roleExists)
            {
                var newRole = new IdentityRole(roleName.ToString());
                var result = await _roleManager.CreateAsync(newRole);
                if (!result.Succeeded)
                {
                    return false;
                }
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return false;
            }

            if (await _userManager.IsInRoleAsync(user, roleName.ToString()))
            {
                var removeFromRoleResult = await _userManager.RemoveFromRoleAsync(user, roleName.ToString());
                if (!removeFromRoleResult.Succeeded)
                {
                    return false;
                }
            }

            var roleAssignmentResult = await _userManager.AddToRoleAsync(user, roleName.ToString());
            if (!roleAssignmentResult.Succeeded)
            {
                return false;
            }

            user.Roles = roleName;
            await _userManager.UpdateAsync(user);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
