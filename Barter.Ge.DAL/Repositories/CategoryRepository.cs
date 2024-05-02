using Barter.Ge.DAL.Context;
using Barter.Ge.DAL.Context.Entities;
using Barter.Ge.DAL.Repositories.Interfaces;

namespace Barter.Ge.DAL.Repositories;

internal class CategoryRepository(BarterDbContext barterDbContext) : BaseRepository<CategoryEntity>(barterDbContext), ICategoryRepository
{
    private readonly BarterDbContext _barterDbContext = barterDbContext;
}
