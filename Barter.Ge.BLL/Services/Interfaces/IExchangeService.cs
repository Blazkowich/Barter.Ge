using Barter.Ge.BLL.Models.Search.Context;
using Barter.Ge.BLL.Models.Search;
using Barter.Ge.BLL.Models;

namespace Barter.Ge.BLL.Services.Interfaces;

public interface IExchangeService
{
    Task<Guid> CreateExchangeAsync(Exchange exchange);

    Task<SearchResult<Exchange>> SearchExchangeWithPagingAsync(ExchangeSearchContext context);

    Task<Exchange> UpdateExchangeAsync(Exchange exchange);

    Task DeleteExchangeAsync(Guid id);
}
