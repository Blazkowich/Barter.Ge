using Barter.Ge.BLL.Services;
using Barter.Ge.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Barter.Ge.BLL.DependencyInjection;

public static class BLLServiceCollectionsExtensions
{
    public static IServiceCollection AddBLLServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IExchangeService, ExchangeService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
