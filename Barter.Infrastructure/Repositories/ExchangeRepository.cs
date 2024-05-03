using Barter.Application.Repository;
using Barter.Domain.Models;
using Barter.Infrastructure.Context;

namespace Barter.Infrastructure.Repositories;

internal class ExchangeRepository(BarterDbContext barterDbContext) : BaseRepository<Exchange>(barterDbContext), IExchangeRepository
{
    private readonly BarterDbContext _barterDbContext = barterDbContext;
}
