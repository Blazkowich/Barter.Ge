using Barter.Domain.Models;
using Barter.Domain.Models.Search;
using Barter.Domain.Models.Search.Context;

namespace Barter.Application.Service.Interface;

public interface ICategoryService
{
    Task<Guid> AddCategoryAsync(Category category);

    Task<SearchResult<Category>> SearchCategoriesWithPagingAsync(CategorySearchContext context);

    Task<Category> UpdateCategoryAsync(Category category);

    Task DeleteCategoryAsync(string name);
}
