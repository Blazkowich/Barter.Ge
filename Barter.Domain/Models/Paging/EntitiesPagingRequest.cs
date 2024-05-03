using System.Linq.Expressions;

namespace Barter.Domain.Models.Paging;

public class EntitiesPagingRequest<T> where T : class
{
    public Expression<Func<T, bool>> Filter { get; set; }

    public Func<IQueryable<T>, IOrderedQueryable<T>> Sorting { get; set; }

    public int PageNumber { get; set; }

    public int PerPage { get; set; }
}