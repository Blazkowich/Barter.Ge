using Barter.Domain.Models;
using Barter.Domain.Models.Search;
using Barter.Domain.Models.Search.Context;

namespace Barter.Application.Service.Interface;

public interface IUserService
{
    Task<Guid> AddUserAsync(User user);

    Task<SearchResult<User>> SearchUserWithPagingAsync(UserSearchContext context);

    Task<User> UpdateUserAsync(User user);

    Task DeleteUserAsync(string userName);
}
