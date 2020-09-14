using System;
using System.Collections.Generic;
using System.Linq;
using TraceMap.Common.Models;

namespace TraceMap.Common.Helpers
{
    public class GraphHelper
    {
        public int MahChainLength(List<Vertex> vertices)
        {
            return GetMaxLengthFromThisNode(vertices.Single(c => c.IsItRoot));
        }

        private int GetMaxLengthFromThisNode(
            Vertex currentVertex,
            Edge previousEdge = null,
            int lengthToRootVertexFromThisVertex = 0)
        {
            var nextEdges = currentVertex.GetNextEdges(previousEdge);
            var currentMaxPath = lengthToRootVertexFromThisVertex;

            foreach (var nextEdge in nextEdges)
            {

                var maxLengthFromThisBranchToRoot = GetMaxLengthFromThisNode(
                        nextEdge.GetNextNode(currentVertex), 
                        nextEdge, 
                        lengthToRootVertexFromThisVertex++);

                currentMaxPath = Math.Max(maxLengthFromThisBranchToRoot, currentMaxPath);
            }

            return currentMaxPath;
        }
    }
}
