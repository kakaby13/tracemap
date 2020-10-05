using System;
using System.Collections.Generic;
using System.Linq;
using TraceMap.Common.Exceptions;
using TraceMap.Common.Models;
using TraceMap.Integration.Common.Common;
using TraceMap.Integration.Common.Models;
using TraceMap.Integration.Tracert.Helpers;

namespace TraceMap.Integration.Tracert
{
    public class TracertIntegrator : IIntegrator
    {
        public List<TraceInfo> GetTraces(List<string> targets)
        {
            return GetRawTraces(targets).ToList();
        }

        public Vertex ParseRawTraces(List<TraceInfo> rawTraces)
        {
            var hostNode = new Vertex { Value = "You" };
            var rawGraph = rawTraces.Select(response => BuildBranch(response, hostNode)).ToList();

            return CompileMap(rawGraph, hostNode).ToList().Single(c => c.ParentVertex == null);
        }

        private static List<Vertex> BuildBranch(TraceInfo response, Vertex hostNode)
        {
            if (!response.RawTrace.Contains("Trace complete"))
            {
                Console.WriteLine($"{TextConstants.CannotParseTrace}{response.Target}");
                Console.WriteLine($"{response}");
                return null;
            }

            var result = new List<Vertex>();
            var allLines = response.RawTrace.Split('\n');

            foreach (var line in allLines)
            {
                try
                {
                    var intervalInfo = ParseTraceRouteLine(line);
                    var node = new Vertex
                    {
                        Value = intervalInfo.NodeName,
                        DistanceToParentVertex = 1, //TODO: implement scaling and then use: intervalInfo.Distance,
                        ParentVertex = result.Count == 0 ? hostNode : result.Last()
                    };
                    node.ParentVertex.ChildVertexes.Add(node);

                    result.Add(node);
                }
                catch (ParseTraceLineException)
                {
                }
            }
            result.Last().Value = response.Target;

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

        private static IEnumerable<TraceInfo> GetRawTraces(IEnumerable<string> targets)
        {
            return targets.Select(target => new TraceInfo(target, $"tracert {target}".Cmd())).ToList();
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
