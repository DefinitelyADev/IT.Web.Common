# IT.Web.Common

[![IT.Web.Common](https://img.shields.io/static/v1?label=IT.Web.Common&message=latest&color=blue&logo=NuGet)](https://www.nuget.org/packages/IT.Web.Common) [![IT.Web.Common.Abstractions](https://img.shields.io/static/v1?label=IT.Web.Common.Abstractions&message=latest&color=blue&logo=NuGet)](https://www.nuget.org/packages/IT.Web.Common.Abstractions)
[![.NET Build](https://github.com/DefinitelyADev/IT.Web.Common/actions/workflows/build-dotnet.yml/badge.svg)](https://github.com/DefinitelyADev/IT.Web.Common/actions/workflows/build-dotnet.yml)

A collection of usefull tools for web applications.

## 1. Getting Started (TypeFinder & WebAppFileProvider)

### 1.1 Using DI

1. Add the [IT.Web.Common NuGet package](https://www.nuget.org/packages/IT.Web.Common) to your ASP.NET Core 6 web project.
2. Register the default services to the service collection
    ``` csharp
    services.AddItWebCommonServices();
    ```
3. That's it! Just use dependecy injection as you whould using the interfaces e.g.
    ```cs
    var typeFinder = _host.Services.GetService<ITypeFinder>();
    Type[] types = typeFinder.FindClassesOfType<MyClass>().ToArray();
    ```

### 1.1 Manualy

1. You can just add the [IT.Web.Common.Abstractions NuGet package](https://www.nuget.org/packages/IT.Web.Common.Abstractions) to your ASP.NET Core 6 web project and create your own implementations of the `ITypeFinder` and the `IWebAppFileProvider`.
2. Register your implementations to the service collection. _Even though the `TryAddSingleton<>()` method is used be sure to NOT call `AddItWebCommonServices()` if you reference the `IT.Web.Common` package, unless you know what you're doing._

    ``` csharp
    services.TryAddSingleton<IWebAppFileProvider, MyWebAppFileProvider>();
    services.TryAddSingleton<ITypeFinder, MyTypeFinder>();
    ```
3. That's it! Just use dependecy injection as you whould using the interfaces e.g.
    ```cs
    var typeFinder = _host.Services.GetService<ITypeFinder>();
    Type[] types = typeFinder.FindClassesOfType<MyClass>().ToArray();
    ```

## 2 Application startup cleanup

Backstory: I've been using pattern of configuring by services and startup in general for about three years now. Even before [this](https://github.com/dotnet/docs/issues/27420) bs. I also don't like humungous Startups (Before Net 6), if you do though you can check section 2.2.

### 2.1 Multiple startups examples

Program.cs:
```csharp
using IT.Web.Common.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Host.ConfigureHostBuilder();

WebApplication app = builder.Build();

app.ConfigureRequestPipeline();

app.Run();
```
LoggingStartup:
```csharp
using IT.Web.Common.Abstractions;
using JetBrains.Annotations;
using Serilog;

[UsedImplicitly]
public class LoggingStartup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    public void ConfigureHostBuilder(IHostBuilder hostBuilder) =>
        hostBuilder.UseSerilog((webHostBuilderContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(webHostBuilderContext.Configuration));

    public void Configure(IApplicationBuilder application) => application.UseSerilogRequestLogging();

    public int Order => 500;
}
```

Example when a route builder is needed:
```csharp
using IT.Web.Common.Abstractions;
using JetBrains.Annotations;

namespace InNode.Invoicing.Host.WebApi.Startups;

[UsedImplicitly]
public class ControllersStartup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
    }

    public void ConfigureHostBuilder(IHostBuilder hostBuilder)
    {
    }

    public void Configure(IApplicationBuilder application)
    {
        if (application is not IEndpointRouteBuilder routeBuilder) // Till ms fixes their abstaction bs
            return;

        routeBuilder.MapControllers();
    }

    public int Order => 1000; // Endpoints should be loaded last
}
```

### 2.2 Single startup examples
Use the same Program.cs as before but create only one startup

Program.cs:
```csharp
using IT.Web.Common.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Host.ConfigureHostBuilder();

WebApplication app = builder.Build();

app.ConfigureRequestPipeline();

app.Run();
```
```csharp
using IT.Web.Common.Abstractions;
using JetBrains.Annotations;

namespace InNode.Invoicing.Host.WebApi.Startups;

[UsedImplicitly]
public class ApplicationStartup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
    }

    public void ConfigureHostBuilder(IHostBuilder hostBuilder) =>
        hostBuilder.UseSerilog((webHostBuilderContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(webHostBuilderContext.Configuration));

    public void Configure(IApplicationBuilder application)
    {
        application.UseExceptionHandler(webHostEnvironment.IsDevelopment() ? "/error-local-development" : "/error");

        if (application is not IEndpointRouteBuilder routeBuilder) // Till ms fixes their abstaction bs
        {
            routeBuilder.MapControllers();
        }

        application.UseSerilogRequestLogging();
    }

    public int Order => 0; // There is only one startup so this is irrelevant
}
```
