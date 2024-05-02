namespace Barter.Ge.Api.ApiModels.Response;

public class CategoryResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }
}
