using System.ComponentModel.DataAnnotations;

namespace Barter.Domain.Models.Enum;

public enum ItemType
{
    [Display(Name = "Exchange")]
    Exchange,

    [Display(Name = "Gift")]
    Gift
}
