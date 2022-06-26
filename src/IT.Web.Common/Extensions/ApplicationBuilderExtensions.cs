using IT.Web.Common.Abstractions;
using IT.Web.Common.Helpers;
using Microsoft.AspNetCore.Builder;

namespace IT.Web.Common.Extensions;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring a web application's request pipeline</param>
    public static void ConfigureRequestPipeline(this IApplicationBuilder application)
    {
        //configure services
        foreach (IAppStartup? instance in StartupExtensionsHelper.GetApplicationStartups())
        {
            instance?.Configure(application);
        }
    }
}
