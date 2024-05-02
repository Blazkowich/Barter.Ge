using Barter.Ge.BLL.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Barter.Ge.Api.ApiModels.Response;

public class ItemResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public Guid OwnerId { get; set; }

    public ConditionStatus Condition { get; set; }

    public ItemType ItemType { get; set; }

    public int Views { get; set; }
}
