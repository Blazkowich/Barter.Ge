using System.ComponentModel.DataAnnotations;

namespace Barter.Domain.Models.Enum;

public enum UserRoles
{
    [Display(Name = "Admin")]
    Admin,

    [Display(Name = "User")]
    User,
}
