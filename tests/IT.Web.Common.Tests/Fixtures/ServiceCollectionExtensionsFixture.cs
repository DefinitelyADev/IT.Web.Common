using IT.Web.Common.Extensions;
using Microsoft.Extensions.Hosting;

namespace IT.Web.Common.Tests.Fixtures;

public class ServiceCollectionExtensionsFixture
{
    public IServiceProvider Services => _host.Services;
    private readonly IHost _host;

    public ServiceCollectionExtensionsFixture()
    {
        IHostBuilder hostBuilder = new HostBuilder();

        hostBuilder.ConfigureServices(services => services.AddItWebCommonServices());

        _host = hostBuilder.Build();
    }
}
