using System.Reflection;
using IT.Web.Common.Abstractions;

namespace IT.Web.Common;

/// <summary>
/// Provides information about types in the current web application.
/// Optionally this class can look at all assemblies in the bin folder.
/// </summary>
public class WebAppTypeFinder : AppDomainTypeFinder
{
    #region Fields

    private bool _binFolderAssembliesLoaded;

    #endregion

    #region Ctor

    public WebAppTypeFinder(IWebAppFileProvider? fileProvider = null) : base(fileProvider)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets whether assemblies in the bin folder of the web application should be specifically checked for being
    /// loaded on application load. This is need in situations where plugins need to be loaded in the AppDomain after the
    /// application been reloaded.
    /// </summary>
    public bool EnsureBinFolderAssembliesLoaded { get; set; } = true;

    #endregion

    #region Methods

    /// <summary>
    /// Gets a physical disk path of \Bin directory
    /// </summary>
    /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
    public virtual string GetBinDirectory()
    {
        return "~/bin";
    }

    /// <inheritdoc />
    public override IList<Assembly> GetAssemblies()
    {
        if (!EnsureBinFolderAssembliesLoaded || _binFolderAssembliesLoaded)
        {
            return base.GetAssemblies();
        }

        _binFolderAssembliesLoaded = true;
        string binPath = GetBinDirectory();
        LoadMatchingAssemblies(binPath);

        return base.GetAssemblies();
    }

    #endregion
}
