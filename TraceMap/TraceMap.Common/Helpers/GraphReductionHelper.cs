using System.Collections.Generic;
using System.Linq;
using TraceMap.Common.Models;

namespace TraceMap.Common.Helpers
{
    public class GraphReductionHelper
    {
        public void ReduceGraph(Vertex rootNode)
        {
            RemoveChildrenDuplicatesForNode(rootNode);
        }

        private static void RemoveChildrenDuplicatesForNode(Vertex nodeToCheckChildrenForDuplicates)
        {
            if (nodeToCheckChildrenForDuplicates.ChildVertexes == null ||
                nodeToCheckChildrenForDuplicates.ChildVertexes.Count == 0)
            {
                return;
            }

            var uniqueValues = nodeToCheckChildrenForDuplicates.ChildVertexes.Select(c => c.Value).Distinct().ToList();

            foreach (var uniqueValue in uniqueValues)
            {
                var nodeToSave = nodeToCheckChildrenForDuplicates.ChildVertexes.First(c => c.Value == uniqueValue);
                var nodesToRemove =
                    nodeToCheckChildrenForDuplicates.ChildVertexes.Except(new List<Vertex> {nodeToSave})
                        .Where(c => c.Value == uniqueValue).ToList();
                foreach (var nodeToRemove in nodesToRemove)
                {
                    nodeToCheckChildrenForDuplicates.ChildVertexes.Remove(nodeToRemove);
                    nodeToRemove.ChildVertexes.ForEach(c => c.ParentVertex = nodeToSave);
                }
            }

            nodeToCheckChildrenForDuplicates.ChildVertexes.ForEach(RemoveChildrenDuplicatesForNode);
        }
    }
}
