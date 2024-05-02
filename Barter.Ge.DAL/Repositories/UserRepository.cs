using Barter.Ge.DAL.Context;
using Barter.Ge.DAL.Context.Entities;
using Barter.Ge.DAL.Repositories.Interfaces;

namespace Barter.Ge.DAL.Repositories;

internal class UserRepository(BarterDbContext barterDbContext) : BaseRepository<UserEntity>(barterDbContext), IUserRepository
{
    private readonly BarterDbContext _barterDbContext = barterDbContext;
}
