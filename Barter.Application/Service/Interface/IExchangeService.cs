using Barter.Domain.Models;
using Barter.Domain.Models.Search;
using Barter.Domain.Models.Search.Context;

namespace Barter.Application.Service.Interface;

public interface IExchangeService
{
    Task<Guid> CreateExchangeAsync(Exchange exchange);

    Task<SearchResult<Exchange>> SearchExchangeWithPagingAsync(ExchangeSearchContext context);

    Task<Exchange> UpdateExchangeAsync(Exchange exchange);

    Task DeleteExchangeAsync(Guid id);
}
