using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TraceMap.Scaner;
using TraceMap.Drawning;

namespace TraceMap;

public static class Startup
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services
                    .AddTransient<ScanerCore>()
                    .AddSingleton<TraceMap>()
                    .AddTransient<IDrawningCore, DrawningCore>();
            });

        return hostBuilder;
    }
}