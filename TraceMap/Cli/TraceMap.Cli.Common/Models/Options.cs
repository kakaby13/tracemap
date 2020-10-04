using CommandLine;
using System.Collections.Generic;

namespace TraceMap.Cli.Common.Models
{
    public class Options
    {
        [Option('n', "name", Required = false, HelpText = "Output file name.")]
        public string Name { get; set; }

        [Option('p', "path", Required = false, HelpText = "Output directory path.")]
        public string Path { get; set; }

        [Option('e', "extension", Required = false, HelpText = "Output file extension.")]
        public string Extension { get; set; }

        [Value(1, Min = 1, Max = 99)]
        public IEnumerable<string> Urls { get; set; }
    }
}
