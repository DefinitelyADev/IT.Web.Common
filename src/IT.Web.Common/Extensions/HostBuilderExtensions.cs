using IT.Web.Common.Abstractions;
using IT.Web.Common.Helpers;
using Microsoft.Extensions.Hosting;

namespace IT.Web.Common.Extensions;

public static class HostBuilderExtensions
{
    /// <summary>
    /// Configure host specific properties
    /// </summary>
    /// <param name="hostBuilder">The IHostBuilder for configuring host specific properties</param>
    public static void ConfigureHostBuilder(this IHostBuilder hostBuilder)
    {
        //configure the host builder
        foreach (IAppStartup? instance in StartupExtensionsHelper.GetApplicationStartups())
        {
            instance?.ConfigureHostBuilder(hostBuilder);
        }
    }
}
