using Barter.Domain.Models.Enum;

#nullable enable

namespace Barter.Domain.Models.Search.Context;

public class ItemSearchContext : SearchContextBase
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? OwnerId { get; set; }

    public ConditionStatus? Condition { get; set; }

    public ItemType? ItemType { get; set; }

    public int? Views { get; set; }
}
