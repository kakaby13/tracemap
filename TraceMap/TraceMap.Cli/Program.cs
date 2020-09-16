using System;
using CommandLine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TraceMap.Draw;
using TraceMap.TraceRouteIntegration;

namespace TraceMap.Cli
{
    public class Program
    {
        public class Options
        {
            [Option('n', "name", Required = false, HelpText = "Output file name.")]
            public string Name { get; set; }

            [Option('p', "path", Required = false, HelpText = "Output directory path.")]
            public string Path { get; set; }

            [Option('e', "extension", Required = false, HelpText = "Output file extension.")]
            public string Extension { get; set; }

            [Value(1, Min= 1, Max= 99)]
            public IEnumerable<string> Urls { get; set; }
        }

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
        }


        static void RunOptions(Options opts)
        {
#if DEBUG
            Console.WriteLine($@"Urls: {string.Join(' ', opts.Urls)}");
            Console.WriteLine($@"Extension: {opts.Extension}");
            Console.WriteLine($@"Name: {opts.Name}");
            Console.WriteLine($@"Path: {opts.Path}");
#endif

            var traceRouteExecutor = new TraceRouteExecutor();
            var result = traceRouteExecutor.Run(opts.Urls.ToList());

            new Painter(result).Draw(outputFileName: opts.Name, fileExtension: opts.Extension, outputPath: opts.Path);
        }
        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
        }
    }
}
