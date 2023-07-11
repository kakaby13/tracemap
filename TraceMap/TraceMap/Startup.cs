using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TraceMap;

public static class Startup
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services
                    .AddSingleton<TraceMap>();
            });

        return hostBuilder;
    }
}