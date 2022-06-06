# IT.Web.Common

[![IT.Web.Common](https://img.shields.io/static/v1?label=IT.Web.Common&message=latest&color=blue&logo=NuGet)](https://www.nuget.org/packages/IT.Web.Common) [![IT.Web.Common.Abstractions](https://img.shields.io/static/v1?label=IT.Web.Common.Abstractions&message=latest&color=blue&logo=NuGet)](https://www.nuget.org/packages/IT.Web.Common.Abstractions)
[![.NET Build](https://github.com/DefinitelyADev/IT.Web.Common/actions/workflows/build-dotnet.yml/badge.svg)](https://github.com/DefinitelyADev/IT.Web.Common/actions/workflows/build-dotnet.yml)

A collection of usefull tools for web applications.

## 1. Getting Started

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
