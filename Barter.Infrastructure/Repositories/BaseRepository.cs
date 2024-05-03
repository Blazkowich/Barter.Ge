using Barter.Application.Repository;
using Barter.Domain.Models.Paging;
using Barter.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Barter.Infrastructure.Repositories;

internal class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    private readonly BarterDbContext _dbContext;

    public BaseRepository(BarterDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbContext.Set<TEntity>().ToListAsync(ct);
    }

    public TEntity Add(TEntity entity)
    {
        var result = _dbContext.Set<TEntity>().Add(entity);
        return result.Entity;
    }

    public virtual async Task<EntitiesPagingResponse<TEntity>> SearchWithPagingAsync(
            EntitiesPagingRequest<TEntity> request,
            CancellationToken ct = default)
    {
        var itemsToReturn = _dbContext.Set<TEntity>().AsQueryable();

        if (request.Filter is not null)
        {
            itemsToReturn = itemsToReturn.Where(request.Filter);
        }

        if (request.Sorting is not null)
        {
            itemsToReturn = request.Sorting(itemsToReturn);
        }

        var totalItemsCount = await itemsToReturn.CountAsync(ct);
        var pagedItems = await itemsToReturn
            .Skip((request.PageNumber - 1) * request.PerPage)
            .Take(request.PerPage)
            .ToListAsync(ct);

        var response = new EntitiesPagingResponse<TEntity>
        {
            Items = pagedItems,
            ItemsTotalCount = totalItemsCount,
            PageNumber = request.PageNumber,
            PerPage = request.PerPage
        };
        return response;
    }

    public async Task<TEntity> GetByIdAsync(Guid? id, CancellationToken ct = default)
    {
        return await _dbContext.Set<TEntity>()
                            .FindAsync([id], ct);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
        var primaryKey = entityType.FindPrimaryKey();

        var keyValues = primaryKey.Properties
                                     .Select(p => entity.GetType().GetProperty(p.Name).GetValue(entity))
                                     .ToArray();

        var existingEntity = await _dbContext.FindAsync<TEntity>(keyValues);

        if (existingEntity == null)
        {
            return null;
        }
        _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _dbContext.SaveChangesAsync(ct);
        return existingEntity;
    }


    public async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync(ct);
    }
}


