using System.ComponentModel.DataAnnotations;

namespace Barter.Ge.DAL.Context.Enum;

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
