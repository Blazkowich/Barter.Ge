using Barter.Domain.Models;
using Barter.Domain.Models.Enum;

namespace Barter.Application.Service.Interface;

public interface IUserService
{
    Task<User> AddUserAsync(User user);

    Task RemoveUserAsync(User user);

    Task UpdateUserAsync(User user);

    Task<User> GetUserByUserNameAsync(string userName);

    Task<User> SignUpAsync(string userName, string email, string password);

    Task SignInAsync(User user, string password);

    Task SignOutAsync();

    Task<List<User>> GetAllUsersAsync();

    Task<bool> AssignRoleToUser(string userId, UserRoles roleName);
}
