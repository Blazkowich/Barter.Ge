using Barter.Application.Repository;

namespace Barter.Application.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository CategoryRepository { get; }

    IExchangeRepository ExchangeRepository { get; }

    IItemRepository ItemRepository { get; }

    IUserRepository UserRepository { get; }

    Task SaveAsync();
}