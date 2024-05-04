using Barter.Domain.Models;
using Barter.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Barter.Infrastructure.Context;

public class UserIdentityDbContext : IdentityDbContext<User>
{
    private readonly IClaimService _claimService;

    public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options, IClaimService claimService)
        : base(options)
    {
        _claimService = claimService;
    }

    public new DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(UserIdentityDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}