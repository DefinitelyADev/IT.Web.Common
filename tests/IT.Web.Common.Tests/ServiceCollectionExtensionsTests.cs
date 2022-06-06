using IT.Web.Common.Abstractions;
using IT.Web.Common.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IT.Web.Common.Tests;

public class ServiceCollectionExtensionsTests
{
    private readonly ServiceCollectionExtensionsFixture _fixture = new();

    [Fact]
    public void RegistersTheAppropriateServices()
    {
        IHostEnvironment? hostEnvironment = _fixture.Services.GetService<IHostEnvironment>();
        IWebAppFileProvider? webAppFileProvider = _fixture.Services.GetService<IWebAppFileProvider>();
        ITypeFinder? typeFinder = _fixture.Services.GetService<ITypeFinder>();

        Assert.NotNull(hostEnvironment);
        Assert.NotNull(webAppFileProvider);
        Assert.NotNull(typeFinder);

        Assert.IsType<WebAppFileProvider>(webAppFileProvider);
        Assert.IsType<WebAppTypeFinder>(typeFinder);
    }
}
