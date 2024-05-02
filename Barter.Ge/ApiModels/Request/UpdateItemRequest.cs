using Barter.Ge.BLL.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Barter.Ge.Api.ApiModels.Request;

public class UpdateItemRequest
{
    [Required]
    public Guid Id { get; set; }

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
