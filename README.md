# Azure Monitor OpenTelemetry Profiler for .NET

| Continuous Integration | Status |
| ----------- | ----------- |
| Package | [![Nuget](https://img.shields.io/nuget/v/Azure.Monitor.OpenTelemetry.Profiler)](https://www.nuget.org/packages/Azure.Monitor.OpenTelemetry.Profiler/) |
| PR Build | [![Build Status](https://dev.azure.com/devdiv/OnlineServices/_apis/build/status%2FOneBranch%2FServiceProfiler%2FBuilds%2FEP-OTel-Profiler-PR?repoName=ServiceProfiler-EP-Profiler&branchName=refs%2Fpull%2F615631%2Fmerge)](https://dev.azure.com/devdiv/OnlineServices/_build/latest?definitionId=25440&repoName=ServiceProfiler-EP-Profiler&branchName=refs%2Fpull%2F615631%2Fmerge) |
| Official Build | [![Build Status](https://dev.azure.com/devdiv/OnlineServices/_apis/build/status%2FOneBranch%2FServiceProfiler%2FBuilds%2FEP-OTel-Profiler-Official?repoName=ServiceProfiler-EP-Profiler&branchName=main)](https://dev.azure.com/devdiv/OnlineServices/_build/latest?definitionId=25454&repoName=ServiceProfiler-EP-Profiler&branchName=main) |

## Description

Welcome! Enable profiler, integrate seamlessly with your Application Insights resource, and unlock powerful [Code Optimizations](https://learn.microsoft.com/azure/azure-monitor/insights/code-optimizations-profiler-overview#code-optimizations) for your .NET applications.

> ⭐ Not sure which `Profiler Agent` is right for you? Check out our [Profiler Agent Selection Guide](./docs/ProfilerAgentSelectionGuide.md) to help you choose the best option for your needs.

## Get Started

### Prerequisites

- **.NET 8.0 or later**: Install the latest .NET SDK from [here](https://dotnet.microsoft.com/download/dotnet).
- **Application Insights Resource**: Follow [this guide](https://learn.microsoft.com/azure/azure-monitor/app/create-workspace-resource#create-a-workspace-based-resource) to create a new Application Insights resource.
- **Azure Monitor OpenTelemetry**: Profiler works with Azure Monitor OpenTelemetry Activities for analysis.
  - If you are using `Microsoft.ApplicationInsights.AspNetCore`, please go to [Microsoft Application Insights Profiler for ASP.NET Core](https://github.com/microsoft/ApplicationInsights-Profiler-AspNetCore) for instructions.

### Walkthrough

Assuming you are building an **ASP.NET Core application**.

- Create a .NET Application

    If you don't have an app already, create a new web API project using the following command:

    ```sh
    dotnet new web
    ```

- Add NuGet Packages

    ```sh
    dotnet add package Azure.Monitor.OpenTelemetry.AspNetCore --prerelease
    dotnet add package Azure.Monitor.OpenTelemetry.Profiler --prerelease
    ```

    _Tips: optionally, update the package reference in project file to use [floating version](https://learn.microsoft.com/nuget/concepts/dependency-resolution#floating-versions) to stay on top of the latest package. For example, in the **csproj** file:_

    ```xml
    <ItemGroup>
        <PackageReference Include="Azure.Monitor.OpenTelemetry.AspNetCore" Version="[1.*-*, 2.0.0)" />
        <PackageReference Include="Azure.Monitor.OpenTelemetry.Profiler" Version="[1.*-*, 2.0.0)" />
    </ItemGroup>
    ```

- Enable Application Insights with OpenTelemetry

  - Follow the [instructions](https://learn.microsoft.com/azure/azure-monitor/app/opentelemetry-enable?tabs=aspnetcore#enable-opentelemetry-with-application-insights) to enable Azure Monitor OpenTelemetry for .NET.

  - Verify that the connection to Application Insights works -- [Confirm Data is Flowing](https://learn.microsoft.com/azure/azure-monitor/app/opentelemetry-enable?tabs=aspnetcore#confirm-data-is-flowing).

- Enable Profiler

    Append the call to `AddAzureMonitorProfiler()` in your code:

    ```csharp
    using Azure.Monitor.OpenTelemetry.AspNetCore;
    // Import the Azure.Monitor.OpenTelemetry.Profiler namespace.
    using Azure.Monitor.OpenTelemetry.Profiler;

    ...
    builder.Services.AddOpenTelemetry()
            .UseAzureMonitor()
            .AddAzureMonitorProfiler();  // Add Azure Monitor Profiler
    ...
    ```

- Run Your Application

    Run your application and check the log output. A successful execution will look like this:

    ```sh
    PS > dotnet run

    Building...

    info: Microsoft.Hosting.Lifetime[14]
        Now listening on: http://localhost:5143
    info: Microsoft.Hosting.Lifetime[0]
        Application started. Press Ctrl+C to shut down.
    info: Microsoft.Hosting.Lifetime[0]
        Hosting environment: Development
    info: Microsoft.Hosting.Lifetime[0]
        Content root path: C:\
    info: Azure.Monitor.OpenTelemetry.Profiler.ServiceProfilerAgentBootstrap[0]
        Starting application insights profiler with connection string: InstrumentationKey=5d…
    info: Azure.Monitor.OpenTelemetry.Profiler.Core.DumbTraceControl[0]
        Start writing trace file C:\Users\aaa\AppData\Local\Temp\SPTraces\...
    ...
    ```

- View Profiler Data

    You can view the profiler data by following [these instructions](https://learn.microsoft.com/azure/azure-monitor/profiler/profiler-data).

    ![sample trace](./images/sample-trace.png)

## Next

- [Setup the Role name](./docs/SetupCloudRoleName.md)
- [Read the examples](#examples)

## Examples

Learn more by following the examples:

- [Enable Azure Monitor OpenTelemetry Profiler in an ASP.NET Core WebAPI](https://github.com/Azure/azuremonitor-opentelemetry-profiler-net/tree/main/examples/aspnetcore-webapi)

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Data Collection

The software may collect information about you and your use of the software and send it to Microsoft. Microsoft may use this information to provide services and improve our products and services. You may turn off the telemetry as described in the repository. There are also some features in the software that may enable you and Microsoft to collect data from users of your applications. If you use these features, you must comply with applicable law, including providing appropriate notices to users of your applications together with a copy of Microsoft’s privacy statement. Our privacy statement is located at <https://go.microsoft.com/fwlink/?LinkID=824704>. You can learn more about data collection and use in the help documentation and our privacy statement. Your use of the software operates as your consent to these practices.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft trademarks or logos is subject to and must follow [Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.
