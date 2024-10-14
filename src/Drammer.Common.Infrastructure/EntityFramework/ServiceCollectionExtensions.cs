using Microsoft.Extensions.DependencyInjection;

namespace Drammer.Common.Infrastructure.EntityFramework;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCreatedModifiedInterceptor(this IServiceCollection services)
    {
        services.AddSingleton<CreatedModifiedInterceptor>();
        return services;
    }
}