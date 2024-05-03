using Barter.Domain.Models;
using Barter.Domain.Models.Search;
using Barter.Domain.Models.Search.Context;

namespace Barter.Application.Service.Interface;

public interface IItemService
{
    Task<Guid> AddItemAsync(Item item);

    Task<SearchResult<Item>> SearchItemWithPagingAsync(ItemSearchContext context);

    Task<Item> UpdateItemAsync(Item item);

    Task DeleteItemAsync(string name);
}
