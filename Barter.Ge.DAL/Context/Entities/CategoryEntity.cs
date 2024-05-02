namespace Barter.Ge.DAL.Context.Entities;

public class CategoryEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public ICollection<CategoryEntity> Subcategories { get; set; }
}
