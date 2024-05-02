using Barter.Ge.DAL.Context.Paging;

namespace Barter.Ge.DAL.Repositories.Interfaces;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    public TEntity Add(TEntity entity);

    Task<List<TEntity>> GetAllAsync(CancellationToken ct = default);

    Task<EntitiesPagingResponse<TEntity>> SearchWithPagingAsync(
        EntitiesPagingRequest<TEntity> pagingRequest,
        CancellationToken ct = default);

    Task<TEntity> GetByIdAsync(Guid? id, CancellationToken ct = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default);

    Task DeleteAsync(TEntity entity, CancellationToken ct = default);
}

