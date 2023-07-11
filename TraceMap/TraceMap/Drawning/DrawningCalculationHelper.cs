using TraceMap.Models;
using TraceMap.Utils;

namespace TraceMap.Drawning;

public static class DrawningCalculationHelper
{
    public static int CalculateDepth(Node node, int currentDepth = 1)
    {
        return node.ChildrenNode.IsNullOrEmpty()
            ? currentDepth
            : node.ChildrenNode
                .Select(c => CalculateDepth(c, currentDepth + 1))
                .Max();
    }
}