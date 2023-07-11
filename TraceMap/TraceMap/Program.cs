using Microsoft.Extensions.DependencyInjection;

using TraceMap;

Startup
    .CreateHostBuilder(args)
    .Build()
    .Services.GetService<TraceMap.TraceMap>()
    ?.Run();



