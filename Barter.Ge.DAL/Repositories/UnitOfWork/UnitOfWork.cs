using Barter.Ge.DAL.Context;
using Barter.Ge.DAL.Repositories.Interfaces;

namespace Barter.Ge.DAL.Repositories.UnitOfWork;

internal class UnitOfWork : IUnitOfWork
{
    private readonly BarterDbContext _barterDbContext;
    private ICategoryRepository _categoryRepository;
    private IExchangeRepository _exchangeRepository;
    private IItemRepository _itemRepository;
    private IUserRepository _userRepository;

    public UnitOfWork(BarterDbContext barterDbContext)
    {
        _barterDbContext = barterDbContext;
    }

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_barterDbContext);

    public IExchangeRepository ExchangeRepository => _exchangeRepository ??= new ExchangeRepository(_barterDbContext);

    public IItemRepository ItemRepository => _itemRepository ??= new ItemRepository(_barterDbContext);

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_barterDbContext);

    public void Dispose() => _barterDbContext.Dispose();
    
    public async Task SaveAsync()
    {
        await _barterDbContext.SaveChangesAsync();
    }
}
