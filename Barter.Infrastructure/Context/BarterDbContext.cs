using Barter.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Barter.Infrastructure.Context;

public class BarterDbContext : DbContext
{
    public BarterDbContext(DbContextOptions<BarterDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Exchange> Exchanges { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BarterDbContext).Assembly);
    }
}
