using System.ComponentModel.DataAnnotations.Schema;

namespace Barter.Ge.DAL.Context.Entities;

public class ItemEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    [ForeignKey("CategoryId")]
    public Guid CategoryId { get; set; }

    [ForeignKey("OwnerId")]
    public Guid OwnerId { get; set; }

    public int Condition { get; set; }

    public int ItemType { get; set; }

    public int Views { get; set; }
}
