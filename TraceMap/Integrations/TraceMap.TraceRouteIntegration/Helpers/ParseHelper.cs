using System;
using System.Linq;
using System.Text.RegularExpressions;
using TraceMap.Common.Exceptions;
using TraceMap.Integration.Common.Common;
using TraceMap.Integration.TraceRoute.Common;

namespace TraceMap.Integration.TraceRoute.Helpers
{
    internal static class ParseHelper
    {
        internal static string ParseIp(string line)
        {
            var matches = Constants.IpRegex.Matches(line);
            if (matches.Count == 0)
            {
                throw new ParseTraceLineException($"{TextConstants.CannotParseIp}{line}");
            }

            var result = matches.Cast<Match>()
                .Select(m => m.Value)
                .First();

            return result.Substring(1, result.Length - 2);
        }

        internal static double ParsePing(string line)
        {
            var matches = Constants.PingRegex.Matches(line);
            if (matches.Count == 0)
            {
                throw new ParseTraceLineException($"{TextConstants.CannotParsePing}{line}");
            }

            var pings = 
                matches.Cast<Match>()
                    .Select(m => m.Value)
                    .Select(match => match.Substring(0, match.Length - 3))
                    .Select(Convert.ToDouble).ToList();

            return pings.Average();
        }
    }
}
