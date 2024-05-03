using System.ComponentModel.DataAnnotations;

namespace Barter.Domain.Models.Enum;

public enum ExchangeStatus
{
    [Display(Name = "Pending")]
    Pending,

    [Display(Name = "Accepted")]
    Accepted,

    [Display(Name = "Rejected")]
    Rejected,

    [Display(Name = "Completed")]
    Completed,

    [Display(Name = "Cancelled")]
    Cancelled
}
