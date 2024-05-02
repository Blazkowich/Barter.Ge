using Barter.Ge.BLL.Models;
using Barter.Ge.BLL.Models.Search;
using Barter.Ge.BLL.Models.Search.Context;

namespace Barter.Ge.BLL.Services.Interfaces;

public interface ICategoryService
{
    Task<Guid> AddCategoryAsync(Category category);

    Task<SearchResult<Category>> SearchCategoriesWithPagingAsync(CategorySearchContext context);

    Task<Category> UpdateCategoryAsync(Category category);

    Task DeleteCategoryAsync(string name);
}
