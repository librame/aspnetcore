## LibrameCore

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://github.com/librame/LibrameCore/blob/master/LICENSE)
[![Available on NuGet https://www.nuget.org/packages?q=Librame.AspNetCore](https://img.shields.io/nuget/v/Librame.AspNetCore.svg?style=flat-square)](https://www.nuget.org/packages?q=Librame.AspNetCore) [![Join the chat at https://gitter.im/librame/aspnetcore](https://badges.gitter.im/librame/aspnetcore.svg)](https://gitter.im/librame/aspnetcore?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Open Source

* Official releases are on [NuGet](https://www.nuget.org/packages?q=LibrameCore).

## Use

    // Install-Package Microsoft.Extensions.DependencyInjection
    var services = new ServiceCollection();
    
    // Install-Package Librame.AspNetCore
    // Register LibrameCore Entry
    services.AddLibrameCore();
    
    // Build ServiceProvider
    var serviceProvider = services.BuildServiceProvider();
    ......
