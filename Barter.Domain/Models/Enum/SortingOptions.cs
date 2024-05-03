using System.ComponentModel.DataAnnotations;

namespace Barter.Domain.Models.Enum;

public enum SortingOptions
{
    [Display(Name = "Most popular")]
    MostPopular,

    [Display(Name = "Added ASC")]
    AddedAscending,

    [Display(Name = "Added DESC")]
    AddedDescending,
}
