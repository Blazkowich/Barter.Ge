namespace Barter.Ge.BLL.Models;

public class Exchange
{
    public Guid Id { get; set; }

    public Guid InitiatorId { get; set; }

    public Guid ReceiverId { get; set; }

    public Guid ItemOfferedId { get; set; }

    public Guid ItemRequestedId { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime ExchangedAt { get; set; }
}
