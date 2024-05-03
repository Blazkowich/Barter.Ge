using System.ComponentModel.DataAnnotations;

namespace Barter.Domain.Models.Enum;

public enum ConditionStatus
{
    [Display(Name = "New")]
    New,

    [Display(Name = "Used")]
    Used
}
