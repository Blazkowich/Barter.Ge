using Barter.Application.Repository;
using Barter.Application.UnitOfWork;
using Barter.Infrastructure.Context;
using Barter.Infrastructure.Repositories;
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

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IExchangeRepository, ExchangeRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

