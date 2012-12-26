using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Util
{
    /// <summary>Base class for nodes in a graph on which breadth-first search is performed by <see cref="BreadthFirstSearch.Run"/>.</summary>
    /// <typeparam name="TLabel">Type that can be used to identify edges connecting nodes.</typeparam>
    public abstract class Node<TLabel> : IEquatable<Node<TLabel>>
    {
        /// <summary>When overridden in a derived class, compares two nodes for equality.</summary>
        public abstract bool Equals(Node<TLabel> other);

        /// <summary>When overridden in a derived class, returns a hash code for this node.</summary>
        public abstract override int GetHashCode();

        /// <summary>
        /// When overridden in a derived class, determines whether this node is “final”. The final nodes are
        /// the nodes the breadth-first search is looking for. The first such node encountered in the search
        /// ends the search with success.
        /// </summary>
        public abstract bool IsFinal { get; }

        /// <summary>
        /// When overridden in a derived class, returns the set of edges going out from this node.
        /// Each edge consists of a label that identifies the edge, and the node that the edge points to.
        /// </summary>
        public abstract IEnumerable<Edge<TLabel>> Edges { get; }
    }
}
