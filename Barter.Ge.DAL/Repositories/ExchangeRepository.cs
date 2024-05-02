using Barter.Ge.DAL.Context;
using Barter.Ge.DAL.Context.Entities;
using Barter.Ge.DAL.Repositories.Interfaces;

namespace Barter.Ge.DAL.Repositories;

internal class ExchangeRepository(BarterDbContext barterDbContext) : BaseRepository<ExchangeEntity>(barterDbContext), IExchangeRepository
{
    private readonly BarterDbContext _barterDbContext = barterDbContext;
}
