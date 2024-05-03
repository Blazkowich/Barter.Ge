using Barter.Application.Service;
using Barter.Application.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Barter.Application;

public static class ApplicationInjection
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
