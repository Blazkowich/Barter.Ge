using Barter.Application.Repository;
using Barter.Domain.Models;
using Barter.Infrastructure.Context;

namespace Barter.Infrastructure.Repositories;

internal class CategoryRepository(BarterDbContext barterDbContext) : BaseRepository<Category>(barterDbContext), ICategoryRepository
{
    private readonly BarterDbContext _barterDbContext = barterDbContext;
}
