using IT.Web.Common.Abstractions;

namespace IT.Web.Common.Helpers;

internal static class StartupExtensionsHelper
{
    public static IEnumerable<IAppStartup?> GetApplicationStartups()
    {
        //find startup configurations provided by other assemblies
        using ITypeFinder typeFinder = new WebAppTypeFinder();

        return typeFinder.FindClassesOfType<IAppStartup>()
            .Select(startup => (IAppStartup?)Activator.CreateInstance(startup))
            .OrderBy(startup => startup?.Order);
    }
}
