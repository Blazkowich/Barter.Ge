using System.ComponentModel.DataAnnotations;

namespace Barter.Ge.BLL.Models.Enum;

public enum ItemType
{
    [Display(Name = "Exchange")]
    Exchange,

    [Display(Name = "Gift")]
    Gift
}
