using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Util
{
    /// <summary>Provides a method to perform a breadth-first search on an arbitrary graph.</summary>
    public class BreadthFirstSearch
    {
        /// <summary>Performs a breadth-first search on an arbitrary graph.</summary>
        /// <typeparam name="TLabel">Type that can be used to identify edges connecting nodes.</typeparam>
        /// <param name="startNode">Node to start the search at.</param>
        /// <returns>The sequence of labels on the edges connecting the start node to the first node encountered that has <see cref="Node{TLabel}.IsFinal"/> set to true.</returns>
        public static IEnumerable<TLabel> Run<TLabel>(Node<TLabel> startNode)
        {
            // Start with a queue containing just the start node
            var q = new Queue<Node<TLabel>>();
            q.Enqueue(startNode);

            // Hashset to keep track of the nodes we’ve already visited
            var already = new HashSet<Node<TLabel>>();

            // Dictionary to remember which node each edge came from,
            // so that we can reconstruct the path going back to the start node.
            var parentMove = new Dictionary<Node<TLabel>, Edge<TLabel>>();

            while (q.Count > 0)
            {
                var node = q.Dequeue();

                if (node.IsFinal)
                {
                    // We’ve found a final node. Reconstruct the path back to the start node
                    var sequence = new List<TLabel>();
                    while (!node.Equals(startNode))
                    {
                        var parentTuple = parentMove[node];
                        sequence.Add(parentTuple.Label);
                        node = parentTuple.Node;
                    }

                    // Reverse the path so that it goes from the start node to the final node
                    sequence.Reverse();
                    return sequence;
                }

                // Compute all the outgoing edges from this node and put the target nodes into the queue
                foreach (var edge in node.Edges)
                {
                    if (already.Add(edge.Node))
                    {
                        // Remember the node we came from. (Note that this actually stores the node the edge is *coming from*, not the node the edge is pointing to.)
                        parentMove[edge.Node] = new Edge<TLabel>(edge.Label, node);
                        q.Enqueue(edge.Node);
                    }
                }
            }

            // There is no path from the start node to any final node.
            throw new InvalidOperationException("There is no path from the start node to any final node.");
        }
    }
}
