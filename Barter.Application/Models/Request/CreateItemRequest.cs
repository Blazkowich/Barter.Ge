using Barter.Domain.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Barter.Application.Models.Request;

public class CreateItemRequest
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    [Required]
    public Guid OwnerId { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    public ConditionStatus Condition { get; set; }

    public ItemType ItemType { get; set; }

    public int Views { get; set; }
}
