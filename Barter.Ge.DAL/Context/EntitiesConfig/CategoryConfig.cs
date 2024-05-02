using Barter.Ge.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static Azure.Core.HttpHeader;

namespace Barter.Ge.DAL.Context.EntitiesConfig;

public class CategoryConfig : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        SeedCategories(builder);
    }

    private static void SeedCategories(EntityTypeBuilder<CategoryEntity> builder)
    {
        var categories = new[]
        {
        new CategoryEntity { Id = Guid.Parse("74892eba-ae27-467b-8f4b-5060b46fd76c"), Name = "Electronics" },
        new CategoryEntity { Id = Guid.Parse("da221366-a4ad-45d0-a6ab-9716bd4e8625"), Name = "Clothing" },
    };

        builder.HasData(categories);
    }
}
