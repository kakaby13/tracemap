﻿using CommandLine;
using TraceMap.Cli.Common.ExecutionHandlers;
using TraceMap.Cli.Common.Models;
using TraceMap.Common.Models;

namespace TraceMap.Cli.Linux
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(c => ParseHandler.RunOptions(c, TargetOperatingSystem.Linux))
                .WithNotParsed(ErrorHandler.HandleParseError);
        }
    }
}
