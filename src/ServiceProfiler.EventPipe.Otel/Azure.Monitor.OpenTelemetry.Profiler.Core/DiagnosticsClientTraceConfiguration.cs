//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using System.Diagnostics.Tracing;
using Azure.Monitor.OpenTelemetry.Profiler.Core.EventListeners;
using Microsoft.ApplicationInsights.Profiler.Shared.Contracts;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Azure.Monitor.OpenTelemetry.Profiler.Core;

internal class DiagnosticsClientTraceConfiguration : DiagnosticsClientTraceConfigurationBase
{
    public DiagnosticsClientTraceConfiguration(
        IOptions<UserConfigurationBase> userConfiguration,
        ILogger<DiagnosticsClientTraceConfigurationBase> logger)
        : base(userConfiguration, logger)
    {
    }

    /// <inheritdoc />
    protected override IEnumerable<EventPipeProvider> AppendEventPipeProviders()
    {

        var dict = new Dictionary<string, string>
        {
            ["FilterAndPayloadSpecs"] = "[AS]*"
        };
        // Two event pipe providers that is specific to OTel.

        // Open Telemetry SDK Event Source
        yield return new EventPipeProvider(TraceSessionListener.OpenTelemetrySDKEventSourceName, EventLevel.Verbose, keywords: 0xffffffffffff, arguments: dict);

        // Open Telemetry Profiler Data adapter event source so that trace analysis knows about the activities
        yield return new EventPipeProvider(AzureMonitorOpenTelemetryProfilerDataAdapterEventSource.EventSourceName, EventLevel.Verbose, keywords: 0xffffffffffff, arguments: null);

        yield return new EventPipeProvider(TraceSessionListener.DiagnosticSourceEventSourceName, EventLevel.Verbose, keywords: 0xffffffffffff, arguments: dict);
    }
}