using Barter.Application.Repository;
using Barter.Application.Service.Interface;
using Barter.Application.UnitOfWork;
using Barter.Domain.Models;
using Barter.Infrastructure.Context;
using Barter.Infrastructure.Repositories;
using Barter.Shared;
using Barter.Shared.Impl;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Barter.Infrastructure;

public static class DALServiceCollectionsExtensions
{
    public static IServiceCollection AddDALRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BarterDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("BarterDbConnectionString")));

        services.AddDbContext<UserIdentityDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("UserDbConnectionString")));

        services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<UserIdentityDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IExchangeRepository, ExchangeRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IClaimService, ClaimService>();

        var serviceProvider = services.BuildServiceProvider();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userIdentityDbContext = serviceProvider.GetRequiredService<UserIdentityDbContext>();

        IdentityDbSeed.SeedDatabaseAsync(userIdentityDbContext, userManager, roleManager).Wait();

        return services;
    }
}

