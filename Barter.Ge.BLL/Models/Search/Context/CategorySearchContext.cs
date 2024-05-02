using Barter.Ge.BLL.Models.Enum;

namespace Barter.Ge.BLL.Models.Search.Context;

#nullable enable

public class CategorySearchContext : SearchContextBase
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public SortingOptions Sorting { get; set; }
}
