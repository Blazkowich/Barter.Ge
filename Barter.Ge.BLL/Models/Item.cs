namespace Barter.Ge.BLL.Models;

public class Item
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public Guid OwnerId { get; set; }

    public int Condition { get; set; }

    public int ItemType { get; set; }

    public int Views { get; set; }
}

