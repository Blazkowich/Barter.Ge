using Barter.Ge.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Barter.Ge.DAL.Context.EntitiesConfig;

public class ItemConfig : IEntityTypeConfiguration<ItemEntity>
{
    public void Configure(EntityTypeBuilder<ItemEntity> builder)
    {
        builder.HasKey(x => x.Id);

        SeedItems(builder);
    }

    private static void SeedItems(EntityTypeBuilder<ItemEntity> builder)
    {
        var items = new[]
        {
            new ItemEntity
            {
                Id = Guid.Parse("0e036fa2-6532-4e20-92f8-fcf0265f49f2"),
                Name = "Electronic Item",
                Description = "This is an example item.",
                CategoryId = Guid.Parse("74892eba-ae27-467b-8f4b-5060b46fd76c"),
                OwnerId = Guid.Parse("363e30bc-5062-47ea-a3a7-ac50fb85b5a0"),
                ImageUrl = "image1.jpg",
                Condition = 0,
                ItemType = 0,
                Views = 10,
            },
            new ItemEntity
            {
                Id = Guid.Parse("9def0b9a-4bf7-4f58-87b4-317357f164e6"),
                Name = "Clothing Item",
                Description = "This is an example item.",
                CategoryId = Guid.Parse("da221366-a4ad-45d0-a6ab-9716bd4e8625"),
                OwnerId = Guid.Parse("adc71544-77af-4102-ad2b-45e6a6bef40d"),
                ImageUrl = "image2.jpg",
                Condition = 1,
                ItemType = 1,
                Views = 20,
            },
        };

        builder.HasData(items);
    }
}
