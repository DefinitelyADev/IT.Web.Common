using IT.Web.Common.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IT.Web.Common.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add the IT.Web.Common services to the application
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    public static void AddItWebCommonServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IWebAppFileProvider, WebAppFileProvider>();
        services.TryAddSingleton<ITypeFinder, WebAppTypeFinder>();
    }
}
