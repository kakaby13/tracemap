using System.Collections.Generic;
using System.Linq;
using TraceMap.Common.Models;
using TraceMap.Integration.Common.Models;
using TraceMap.Integration.TraceRoute.Helpers;
#if DEBUG
using TraceMap.Integration.TraceRoute.Common;
using System;
#endif

namespace TraceMap.Integration.TraceRoute
{
    public class TraceRouteIntegrator : IIntegrator
    {
        public List<string> GetTraces(List<string> targets)
        {
            var traces = new List<string>();

#if !DEBUG
            traces.AddRange(GetRawTraces(targets));
#endif
#if DEBUG
            Console.WriteLine("Debug version");

            traces.AddRange(new List<string>
            {
                DebugUtils.TutBy,
                DebugUtils.Google
            });
#endif
            return traces;
        }

        public Vertex ParseRawTraces(List<string> rawTraces)
        {
            var hostNode = new Vertex { Value = "You" };
            var rawGraph = rawTraces.Select(response => BuildBranch(response, hostNode)).ToList();

            return CompileMap(rawGraph, hostNode).ToList().Single(c => c.ParentVertex == null);
        }

#if !DEBUG
        private static IEnumerable<string> GetRawTraces(IEnumerable<string> targets)
        {
            return targets.Select(target => $"traceroute {target}".Bash()).ToList();
        }
#endif

        private static List<Vertex> BuildBranch(string response, Vertex rootNode)
        {
            var result = new List<Vertex>();
            var allLines = response.Split('\n');
            if (allLines.Length < 2) // TODO: magic variable
            {
                return null;
            }

            var lines = allLines.Skip(1).ToList();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.Contains("* * *"))
                {
                    continue;
                }
                var intervalInfo = ParseTraceRouteLine(line);
                var node = new Vertex
                {
                    Value = intervalInfo.NodeName,
                    DistanceToParentVertex = 1, //TODO: implement scaling and then use: intervalInfo.Distance,
                    ParentVertex = result.Count == 0 ? rootNode : result.Last()
                };

                node.ParentVertex.ChildVertexes.Add(node);

                result.Add(node);
            }

            return result;
        }

        private static TraceMapIntervalInfo ParseTraceRouteLine(string line)
        {
            return new TraceMapIntervalInfo
            {
                NodeName = ParseHelper.ParseIp(line),
                Distance = ParseHelper.ParsePing(line),
            };
        }

        private static IEnumerable<Vertex> CompileMap(IEnumerable<List<Vertex>> amountOfTraces, Vertex rootNode)
        {
            var result = new List<Vertex> { rootNode };
            foreach (var trace in amountOfTraces)
            {
                result.AddRange(trace);
            }

            return result;
        }
    }
}
