using Barter.Domain.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Barter.Application.Models.Request;

public class UpdateExchangeRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid InitiatorId { get; set; }

    [Required]
    public Guid ReceiverId { get; set; }

    [Required]
    public Guid ItemOfferedId { get; set; }

    [Required]
    public Guid ItemRequestedId { get; set; }

    [Required]
    public ExchangeStatus Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ExchangedAt { get; set; }
}
