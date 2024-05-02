using Barter.Ge.DAL.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barter.Ge.DAL.Context;

public class BarterDbContext : DbContext
{
    public BarterDbContext(DbContextOptions<BarterDbContext> options)
        : base(options)
    {
    }

    public DbSet<CategoryEntity> Categories { get; set; }

    public DbSet<ExchangeEntity> Exchanges { get; set; }

    public DbSet<ItemEntity> Items { get; set; }

    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BarterDbContext).Assembly);
    }
}
