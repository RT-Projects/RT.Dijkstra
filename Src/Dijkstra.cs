using System;
using System.Collections.Generic;

namespace RT.Dijkstra
{
    /// <summary>
    ///     Provides a method to run Dijkstra’s Algorithm (a generalization of breadth-first search) on an arbitrary directed
    ///     graph with positive edge weights.</summary>
    public static class DijkstrasAlgorithm
    {
        /// <summary>
        ///     Runs Dijkstra’s Algorithm (a generalization of breadth-first search) on an arbitrary graph.</summary>
        /// <typeparam name="TWeight">
        ///     Type of the weight (or length or any other quantity to be minimized) of each edge between nodes.</typeparam>
        /// <typeparam name="TLabel">
        ///     Type that is used to identify edges.</typeparam>
        /// <param name="startNode">
        ///     Node to start the search at.</param>
        /// <param name="initialWeight">
        ///     The initial weight to start with (usually zero).</param>
        /// <param name="add">
        ///     Function to add two weights together.</param>
        /// <param name="totalWeight">
        ///     Receives the total weight of the path returned.</param>
        /// <returns>
        ///     The sequence of labels on the edges connecting the start node to the first node encountered that has <see
        ///     cref="Node{TWeight,TLabel}.IsFinal"/> set to true.</returns>
        /// <exception cref="InvalidOperationException">
        ///     There is no path from the <paramref name="startNode"/> to any final node.</exception>
        public static IEnumerable<Step<TWeight, TLabel>> Run<TWeight, TLabel>(Node<TWeight, TLabel> startNode, TWeight initialWeight, Func<TWeight, TWeight, TWeight> add, out TWeight totalWeight) where TWeight : IComparable<TWeight>
        {
            // Start with a priority queue containing just the start node
            var q = new PriorityQueue<Node<TWeight, TLabel>, TWeight>();
            q.Add(startNode, initialWeight);

            // Hashset to keep track of the nodes we’ve already visited
            var already = new HashSet<Node<TWeight, TLabel>>();

            // Dictionary to remember which node each edge came from,
            // so that we can reconstruct the path going back to the start node.
            var parentEdges = new Dictionary<Node<TWeight, TLabel>, Edge<TWeight, TLabel>>();

            while (q.Count > 0)
            {
                q.Extract(out var node, out var weight);
                if (!already.Add(node))
                    continue;

                if (node.IsFinal)
                {
                    // We’ve found a final node. Reconstruct the path back to the start node
                    var sequence = new List<Step<TWeight, TLabel>> { new Step<TWeight, TLabel>(node, default(TLabel)) };
                    while (!node.Equals(startNode))
                    {
                        var parentEdge = parentEdges[node];
                        sequence.Add(new Step<TWeight, TLabel>(parentEdge.Node, parentEdge.Label));
                        node = parentEdge.Node;
                    }

                    // Reverse the path so that it goes from the start node to the final node
                    sequence.Reverse();
                    totalWeight = weight;
                    return sequence;
                }

                // Compute all the outgoing edges from this node and put the target nodes into the priority queue
                foreach (var edge in node.Edges)
                {
                    var newWeight = add(weight, edge.Weight);
                    q.Add(edge.Node, newWeight);

                    // Remember the node we came from by putting it in the ‘parentEdges’ dictionary.
                    // (Note that this kinda abuses the Edge<,> class to store slightly different information than normally:
                    // instead of the node the edge is pointing to, we store the node the edge is *coming from*; and
                    // instead of the weight of a single edge, we store the total weight from the start node.)
                    if (!parentEdges.ContainsKey(edge.Node) || parentEdges[edge.Node].Weight.CompareTo(newWeight) > 0)
                        parentEdges[edge.Node] = new Edge<TWeight, TLabel>(newWeight, edge.Label, node);
                }
            }

            // There is no path from the start node to any final node.
            throw new DijkstraNoSolutionException<TWeight, TLabel>($"There is no path from the start node to any final node. {already.Count} nodes were visited.", already);
        }
    }
}
