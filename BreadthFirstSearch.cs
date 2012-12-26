using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Util
{
    public class BreadthFirstSearch
    {
        public static IEnumerable<TMove> Run<TMove>(TreeNode<TMove> startNode)
        {
            var q = new Queue<TreeNode<TMove>>();
            q.Enqueue(startNode);
            var already = new HashSet<TreeNode<TMove>>();
            var parentMove = new Dictionary<TreeNode<TMove>, Edge<TMove>>();

            while (q.Count > 0)
            {
                var node = q.Dequeue();
                if (node.IsFinal)
                {
                    var sequence = new List<TMove>();
                    while (!node.Equals(startNode))
                    {
                        var parentTuple = parentMove[node];
                        sequence.Add(parentTuple.Label);
                        node = parentTuple.Node;
                    }
                    sequence.Reverse();
                    return sequence;
                }
                foreach (var edge in node.Edges)
                {
                    if (already.Add(edge.Node))
                    {
                        parentMove[edge.Node] = new Edge<TMove>(edge.Label, node);
                        q.Enqueue(edge.Node);
                    }
                }
            }

            throw new InvalidOperationException("There is no path from the initial node to any final node.");
        }
    }
}
