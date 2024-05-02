using Barter.Ge.DAL.Context;
using Barter.Ge.DAL.Context.Entities;
using Barter.Ge.DAL.Repositories.Interfaces;

namespace Barter.Ge.DAL.Repositories;

internal class ItemRepository(BarterDbContext barterDbContext) : BaseRepository<ItemEntity>(barterDbContext), IItemRepository
{
    private readonly BarterDbContext _barterDbContext = barterDbContext;
}
