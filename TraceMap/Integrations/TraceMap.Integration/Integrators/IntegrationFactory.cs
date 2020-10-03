using System;
using TraceMap.Common.Models;
using TraceMap.Integration.TraceRoute;
using TraceMap.Integration.Tracert;

namespace TraceMap.Integration.Integrators
{
    internal static class IntegrationFactory
    {
        internal static IIntegrator GetIntegrator(TargetOperatingSystem os)
        {
            switch (os)
            {
                case TargetOperatingSystem.Linux:
                    return new TraceRouteIntegrator();
                case TargetOperatingSystem.Win10:
                    return new TracertIntegrator();
                default:
                    throw new ArgumentOutOfRangeException(nameof(os), os, null);
            }
        }
    }
}
