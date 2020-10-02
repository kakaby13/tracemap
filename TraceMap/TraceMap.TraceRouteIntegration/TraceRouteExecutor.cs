using System.Collections.Generic;
using System.Linq;
using TraceMap.TraceRouteIntegration.Helpers;
using TraceMap.TraceRouteIntegration.Models;
#if !DEBUG
using TraceMap.Common.Helpers;
#endif
using TraceMap.Common.Models;
#if DEBUG
using System;
using TraceMap.TraceRouteIntegration.Common;
#endif

namespace TraceMap.TraceRouteIntegration
{
    public static class TraceRouteExecutor
    {
        public static Vertex Run(List<string> targets)
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
            return BuildGraph(traces).Single(c=>c.ParentVertex == null);
        }

#if !DEBUG
        private static IEnumerable<string> GetRawTraces(IEnumerable<string> targets)
        {
            return targets.Select(target => $"traceroute {target}".Bash()).ToList();
        }
#endif
        
        private static IEnumerable<Vertex> BuildGraph(IEnumerable<string> responses)
        {
            var hostNode = new Vertex {Value = "You"};
            var rawGraph = responses.Select(response => BuildBranch(response, hostNode)).ToList();

            return CompileMap(rawGraph, hostNode);
        }

        private static List<Vertex> BuildBranch(string response, Vertex rootNode)
        {
            var result = new List<Vertex>();
            var allLines = response.Split('\n');
            if (allLines.Length < 2)
            {
                return null;
            }

            var lines = allLines.Skip(1).ToList();

            foreach (var line in lines)
            {
                if(string.IsNullOrWhiteSpace(line) || line.Contains("* * *"))
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

        private static TraceRouteIntervalInfo ParseTraceRouteLine(string line)
        {
            return new TraceRouteIntervalInfo
            {
                NodeName = ParseHelper.ParseIp(line),
                Distance = ParseHelper.ParsePing(line),
            };
        }

        private static IEnumerable<Vertex> CompileMap(IEnumerable<List<Vertex>> amountOfTraces, Vertex rootNode)
        {
            var result = new List<Vertex> {rootNode};
            foreach (var trace in amountOfTraces)
            {
                
                result.AddRange(trace);
            }

            return result;
        }
    }
}
