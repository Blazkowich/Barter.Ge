using Barter.Ge.DAL.Repositories.Interfaces;

namespace Barter.Ge.DAL.Repositories.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository CategoryRepository { get; }
    
    IExchangeRepository ExchangeRepository { get; }

    IItemRepository ItemRepository { get; }

    IUserRepository UserRepository { get; }
    
    Task SaveAsync();
}