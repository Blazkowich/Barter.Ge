using Barter.Ge.BLL.Models.Search.Context;
using Barter.Ge.BLL.Models.Search;
using Barter.Ge.BLL.Models;

namespace Barter.Ge.BLL.Services.Interfaces;

public interface IItemService
{
    Task<Guid> AddItemAsync(Item item);

    Task<SearchResult<Item>> SearchItemWithPagingAsync(ItemSearchContext context);

    Task<Item> UpdateItemAsync(Item item);

    Task DeleteItemAsync(string name);
}
