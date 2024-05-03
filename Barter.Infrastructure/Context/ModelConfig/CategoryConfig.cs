using Barter.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barter.Infrastructure.Context.ModelConfig;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        SeedCategories(builder);
    }

    private static void SeedCategories(EntityTypeBuilder<Category> builder)
    {
        var categories = new[]
        {
        new Category { Id = Guid.Parse("74892eba-ae27-467b-8f4b-5060b46fd76c"), Name = "Electronics" },
        new Category { Id = Guid.Parse("da221366-a4ad-45d0-a6ab-9716bd4e8625"), Name = "Clothing" },
    };

        builder.HasData(categories);
    }
}
