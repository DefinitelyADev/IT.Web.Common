using System.Net;
using IT.Web.Common.Abstractions;
using IT.Web.Common.Helpers;
using Microsoft.Extensions.Configuration;
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

    /// <summary>
    /// Add services to the application and configure service provider
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        //let the operating system decide what TLS protocol version to use
        //see https://docs.microsoft.com/dotnet/framework/network-programming/tls
        ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

        //configure services
        foreach (IAppStartup? instance in StartupExtensionsHelper.GetApplicationStartups())
        {
            instance?.ConfigureServices(services, configuration);
        }

        services.AddItWebCommonServices();
    }
}
