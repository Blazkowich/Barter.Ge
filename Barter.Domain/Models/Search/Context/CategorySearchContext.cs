using Barter.Domain.Models.Enum;

namespace Barter.Domain.Models.Search.Context;

#nullable enable

public class CategorySearchContext : SearchContextBase
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public SortingOptions Sorting { get; set; }
}
