using System.ComponentModel.DataAnnotations;

#nullable enable

namespace Barter.Application.Models.Request;

public class UpdateCategoryRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }
}
