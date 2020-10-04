using System.Linq;
using TraceMap.Cli.Common.Models;
using TraceMap.Common.Models;
using TraceMap.Integration;
#if DEBUG
using System;
#endif

namespace TraceMap.Cli.Common.ExecutionHandlers
{
    public static class ParseHandler
    {
        public static void RunOptions(Options opts, TargetOperatingSystem os)
        {
#if DEBUG
            Console.WriteLine($@"Urls: {string.Join(" ", opts.Urls)}");
            Console.WriteLine($@"Extension: {opts.Extension}");
            Console.WriteLine($@"Name: {opts.Name}");
            Console.WriteLine($@"Path: {opts.Path}");
#endif
            var integrationRunner = new IntegrationRunner();
            integrationRunner.Run(os, opts.Urls.ToList(), opts.Name, opts.Extension, opts.Path);
        }
    }
}
