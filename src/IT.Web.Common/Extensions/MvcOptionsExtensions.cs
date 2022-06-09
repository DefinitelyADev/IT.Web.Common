// Credit: https://stackoverflow.com/a/58406404/4857852

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace IT.Web.Common.Extensions;

public static class MvcOptionsExtensions
{
    /// <summary>
    /// Adds a RoutePrefixConvention to the MvcOptions conventions
    /// </summary>
    /// <param name="options">The mvc options</param>
    /// <param name="routeAttribute">The route template provider</param>
    public static void AddGeneralRoutePrefix(this MvcOptions options, IRouteTemplateProvider routeAttribute)
    {
        options.Conventions.Add(new RoutePrefixConvention(routeAttribute));
    }

    /// <summary>
    /// Adds a RoutePrefixConvention to the MvcOptions conventions
    /// </summary>
    /// <param name="options">The mvc options</param>
    /// <param name="prefixTemplate">The prefix template</param>
    public static void AddGeneralRoutePrefix(this MvcOptions options, string prefixTemplate)
    {
        options.AddGeneralRoutePrefix(new RouteAttribute(prefixTemplate));
    }
}
