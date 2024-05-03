using Barter.Domain.Models.Enum;

namespace Barter.Application.Models.Response;

public class ItemResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public Guid OwnerId { get; set; }

    public string ImageUrl { get; set; }

    public ConditionStatus Condition { get; set; }

    public ItemType ItemType { get; set; }

    public int Views { get; set; }
}
