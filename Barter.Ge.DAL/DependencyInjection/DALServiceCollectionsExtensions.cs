using Barter.Ge.DAL.Repositories.Interfaces;
using Barter.Ge.DAL.Repositories.UnitOfWork;
using Barter.Ge.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Barter.Ge.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Barter.Ge.DAL.DependencyInjection;

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

