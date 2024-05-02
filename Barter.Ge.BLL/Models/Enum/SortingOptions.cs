using System.ComponentModel.DataAnnotations;

namespace Barter.Ge.BLL.Models.Enum;

public enum SortingOptions
{
    [Display(Name = "Most popular")]
    MostPopular,

    [Display(Name = "Added ASC")]
    AddedAscending,

    [Display(Name = "Added DESC")]
    AddedDescending,
}
