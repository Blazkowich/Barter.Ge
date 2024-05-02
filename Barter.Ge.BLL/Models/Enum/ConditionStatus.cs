using System.ComponentModel.DataAnnotations;

namespace Barter.Ge.BLL.Models.Enum;

public enum ConditionStatus
{
    [Display(Name = "New")]
    New,

    [Display(Name = "Used")]
    Used
}
