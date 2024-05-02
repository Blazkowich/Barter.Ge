using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Barter.Ge.DAL.Context.Entities;

namespace Barter.Ge.DAL.Context.EntitiesConfig;

public class UserConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }

    private static void SeedUsers(EntityTypeBuilder<UserEntity> builder)
    {
            var users = new[]
            {
                new UserEntity
            {
                Id = Guid.Parse("363e30bc-5062-47ea-a3a7-ac50fb85b5a0"),
                Username = "Vaja",
                Email = "user1@example.com",
                Password = "password1",
                MobileNumber = 1234567890,
                Address = "Address1",
                ProfilePicture = "profile1.jpg"
            },
            new UserEntity
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

