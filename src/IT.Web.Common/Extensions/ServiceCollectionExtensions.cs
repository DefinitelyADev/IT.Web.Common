using IT.Web.Common.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace IT.Web.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterCommonWebServices(this IServiceCollection services)
    {
        services.AddSingleton<IWebAppFileProvider, WebAppFileProvider>();
        services.AddSingleton<ITypeFinder, WebAppTypeFinder>();

        return services;
    }
}
