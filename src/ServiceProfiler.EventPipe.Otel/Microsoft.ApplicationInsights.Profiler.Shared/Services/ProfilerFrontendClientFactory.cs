// -----------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using Azure.Core;
using Microsoft.ApplicationInsights.Profiler.Shared.Contracts;
using Microsoft.ApplicationInsights.Profiler.Shared.Services.Abstractions;
using Microsoft.ApplicationInsights.Profiler.Shared.Services.Abstractions.Auth;
using Microsoft.ApplicationInsights.Profiler.Shared.Services.Auth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.ServiceProfiler.Agent.FrontendClient;
using Microsoft.ServiceProfiler.Utilities;
using System;

namespace Microsoft.ApplicationInsights.Profiler.Shared.Services;

/// <summary>
/// This helper function simplifies the registration process for the <see cref="IProfilerFrontendClient" />.
/// Please do NOT inject this directly. Instead, inject the <see cref="IProfilerFrontendClient"/>.
/// </summary>
internal class ProfilerFrontendClientFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceProfilerContext _serviceProfilerContext;
    private readonly ILogger _logger;
    private readonly UserConfigurationBase _userConfiguration;

    public ProfilerFrontendClientFactory(
        IServiceProvider serviceProvider,
        IServiceProfilerContext serviceProfilerContext,
        IOptions<UserConfigurationBase> userConfiguration,
        ILogger<ProfilerFrontendClientFactory> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _serviceProfilerContext = serviceProfilerContext ?? throw new ArgumentNullException(nameof(serviceProfilerContext));
        _userConfiguration = userConfiguration?.Value ?? throw new ArgumentNullException(nameof(userConfiguration));
    }

    public IProfilerFrontendClient CreateProfilerFrontendClient()
    {
        IAuthTokenProvider authTokenProvider = _serviceProvider.GetRequiredService<IAuthTokenProvider>();

        TokenCredential? credential = authTokenProvider.IsAADAuthenticateEnabled ?
            ActivatorUtilities.CreateInstance<AADAuthTokenCredential>(_serviceProvider) :
            null;
        
        _logger.LogDebug("Is Credential null? {result}.", credential is null);
        // TokenCredential? credential = ActivatorUtilities.CreateInstance<AADAuthTokenCredential>(_serviceProvider);

        return ActivatorUtilities.CreateInstance<ProfilerFrontendClient>(
            _serviceProvider,
            _serviceProfilerContext.StampFrontendEndpointUrl,
            _serviceProfilerContext.AppInsightsInstrumentationKey,
            _serviceProfilerContext.MachineName,
            "1.0.0",
            FormattableString.Invariant($"ServiceProfilerEventPipeAgent/{EnvironmentUtilities.ExecutingAssemblyInformationalVersion}"),
            credential,
            _userConfiguration.SkipEndpointCertificateValidation,
            false);
    }
}
