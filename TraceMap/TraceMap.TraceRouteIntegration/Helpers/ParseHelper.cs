using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TraceMap.TraceRouteIntegration.Common;

namespace TraceMap.TraceRouteIntegration.Helpers
{
    internal class ParseHelper
    {
        internal string ParseIp(string line)
        {
            var matches = Constants.IpRegex.Matches(line);
            if (matches.Count == 0)
            {
                throw new NotImplementedException();
            }

            var result = matches.Cast<Match>()
                .Select(m => m.Value)
                .First();

            return result.Substring(1, result.Length - 2);
        }

        internal double ParsePing(string line)
        {
            var matches = Constants.PingRegex.Matches(line);
            if (matches.Count == 0)
            {
                throw new NotImplementedException();
            }

            var pings = new List<double>();

            foreach (var match in matches.Cast<Match>().Select(m => m.Value))
            {
                var ping = match.Substring(0, match.Length - 3);
                pings.Add(Convert.ToDouble(ping));
            }

            return pings.Average();
        }
    }
}
