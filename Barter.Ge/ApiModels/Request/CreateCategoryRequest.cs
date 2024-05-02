using Barter.Ge.Api.ApiModels.Response;
using System.ComponentModel.DataAnnotations;

#nullable enable

namespace Barter.Ge.Api.ApiModels.Request;

public class CreateCategoryRequest
{
    [Required]
    public required string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }
}
