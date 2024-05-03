using Barter.Domain.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Barter.Application.Models.Request;

public class CreateExchangeRequest
{
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
}
