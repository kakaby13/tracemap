using System;
using System.Collections.Generic;
using System.Linq;
using TraceMap.Common.Helpers;
using TraceMap.Common.Models;
using TraceMap.TraceRouteIntegration.Common;
using TraceMap.TraceRouteIntegration.Helpers;
using TraceMap.TraceRouteIntegration.Models;

namespace TraceMap.TraceRouteIntegration
{
    public class TraceRouteExecutor
    {
        public List<Vertex> Run(List<string> targets)
        {
            var traces = new List<string>();

#if !DEBUG
            traces.AddRange(targets
                .Select(target => $"traceroute {target}")
                .Select(command => command.Bash())
                .ToList());
#endif

#if DEBUG
            Console.WriteLine("Debug version");

            traces.AddRange(new List<string>
            {
                DebugUtils.TutBy,
                DebugUtils.Google
            });
#endif
            return BuildGraph(traces);
        }

        private List<Vertex> BuildGraph(List<string> responses)
        {
            var rawGraph = new List<List<Vertex>>();
            var hostNode = new Vertex("You", true);
            foreach (var response in responses)
            {
                rawGraph.Add(BuildBranch(response, hostNode));
            }

            return CompileMap(rawGraph, hostNode);
        }

        private List<Vertex> BuildBranch(string response, Vertex rootNode)
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
                var intervalInfo = ParseTraceRouteLine(line);
                var node = new Vertex(intervalInfo.NodeName);
                var edge = new Edge(result.Count == 0 ? rootNode : result.Last(), node)
                {
                    Value = intervalInfo.Distance
                };
                result.Add(node);
            }

            return result;
        }

        private TraceRouteIntervalInfo ParseTraceRouteLine(string line)
        {
            var parseHelper = new ParseHelper();
            return new TraceRouteIntervalInfo
            {
                NodeName = parseHelper.ParseIp(line),
                Distance = parseHelper.ParsePing(line),
            };
        }

        private static List<Vertex> CompileMap(IEnumerable<List<Vertex>> amountOfTraces, Vertex rootNode)
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
