using Barter.Application.Repository;
using Barter.Domain.Models;
using Barter.Infrastructure.Context;

namespace Barter.Infrastructure.Repositories;

internal class UserRepository(BarterDbContext barterDbContext) : BaseRepository<User>(barterDbContext), IUserRepository
{
    private readonly BarterDbContext _barterDbContext = barterDbContext;
}
