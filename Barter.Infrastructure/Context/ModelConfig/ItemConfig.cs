using Barter.Domain.Models;
using Barter.Domain.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barter.Infrastructure.Context.ModelConfig;

public class ItemConfig : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(x => x.Id);

        SeedItems(builder);
    }

    private static void SeedItems(EntityTypeBuilder<Item> builder)
    {
        var items = new[]
        {
            new Item
            {
                Id = Guid.Parse("0e036fa2-6532-4e20-92f8-fcf0265f49f2"),
                Name = "Electronic Item",
                Description = "This is an example item.",
                CategoryId = Guid.Parse("74892eba-ae27-467b-8f4b-5060b46fd76c"),
                OwnerId = Guid.Parse("363e30bc-5062-47ea-a3a7-ac50fb85b5a0"),
                ImageUrl = "image1.jpg",
                Condition = ConditionStatus.New,
                ItemType = ItemType.Exchange,
                Views = 10,
            },
            new Item
            {
                Id = Guid.Parse("9def0b9a-4bf7-4f58-87b4-317357f164e6"),
                Name = "Clothing Item",
                Description = "This is an example item.",
                CategoryId = Guid.Parse("da221366-a4ad-45d0-a6ab-9716bd4e8625"),
                OwnerId = Guid.Parse("adc71544-77af-4102-ad2b-45e6a6bef40d"),
                ImageUrl = "image2.jpg",
                Condition = ConditionStatus.Used,
                ItemType = ItemType.Gift,
                Views = 20,
            },
        };

        builder.HasData(items);
    }
}
