using Barter.Ge.BLL.Models.Search.Context;
using Barter.Ge.BLL.Models.Search;
using Barter.Ge.BLL.Models;

namespace Barter.Ge.BLL.Services.Interfaces;

public interface IUserService
{
    Task<Guid> AddUserAsync(User user);

    Task<SearchResult<User>> SearchUserWithPagingAsync(UserSearchContext context);

    Task<User> UpdateUserAsync(User user);

    Task DeleteUserAsync(string userName);
}
