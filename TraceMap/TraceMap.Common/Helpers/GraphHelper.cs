using System.Linq;
using TraceMap.Common.Models;

namespace TraceMap.Common.Helpers
{
    public class GraphHelper
    {
        public int MahChainLength(Vertex rootVertex)
        {
            var result = GetMaxChain(rootVertex);

            return result;
        }

        private static int GetMaxChain(Vertex node, int currentLvl = 0)
        {
            return node.ChildVertexes
                .Select(child => GetMaxChain(child, currentLvl + 1))
                .Prepend(currentLvl).Max();
        }
    }
}
