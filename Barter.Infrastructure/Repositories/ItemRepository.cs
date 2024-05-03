using Barter.Application.Repository;
using Barter.Domain.Models;
using Barter.Infrastructure.Context;

namespace Barter.Infrastructure.Repositories;

internal class ItemRepository(BarterDbContext barterDbContext) : BaseRepository<Item>(barterDbContext), IItemRepository
{
    private readonly BarterDbContext _barterDbContext = barterDbContext;
}
