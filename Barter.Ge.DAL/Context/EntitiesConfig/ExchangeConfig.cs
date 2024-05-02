using Barter.Ge.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Barter.Ge.DAL.Context.EntitiesConfig;

public class ExchangeConfig : IEntityTypeConfiguration<ExchangeEntity>
{
    public void Configure(EntityTypeBuilder<ExchangeEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
