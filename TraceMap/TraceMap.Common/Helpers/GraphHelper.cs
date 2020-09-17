using System.Linq;
using TraceMap.Common.Models;

namespace TraceMap.Common.Helpers
{
    public class GraphHelper
    {
        public int MahChainLength(Vertex rootVertex)
        {
            return GetMaxLengthFromThisNode(rootVertex);
        }

        private static int GetMaxLengthFromThisNode(
            Vertex currentVertex,
            int lengthToRootVertexFromThisVertex = 0)
        {
            return currentVertex.ChildVertexes
                .Select(vertex => GetMaxLengthFromThisNode(vertex, lengthToRootVertexFromThisVertex++))
                .Prepend(lengthToRootVertexFromThisVertex).Max();
        }
    }
}
