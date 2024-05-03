using Barter.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barter.Infrastructure.Context.ModelConfig;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        SeedUsers(builder);
    }

    private static void SeedUsers(EntityTypeBuilder<User> builder)
    {
        var users = new[]
        {
                new User
            {
                Id = Guid.Parse("363e30bc-5062-47ea-a3a7-ac50fb85b5a0"),
                Username = "Vaja",
                Email = "user1@example.com",
                Password = "password1",
                MobileNumber = 1234567890,
                Address = "Address1",
                ProfilePicture = "profile1.jpg"
            },
            new User
            {
                Id = Guid.Parse("adc71544-77af-4102-ad2b-45e6a6bef40d"),
                Username = "Goga",
                Email = "user2@example.com",
                Password = "password2",
                MobileNumber = 9876543210,
                Address = "Address2",
                ProfilePicture = "profile2.jpg"
            }
        };

        builder.HasData(users);
    }
}

