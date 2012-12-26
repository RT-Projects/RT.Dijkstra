using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Util
{
    /// <summary>Encapsulates an edge in the graph for breadth-first search, containing a label and a target node.</summary>
    /// <typeparam name="TLabel">Type that can be used to identify this edge.</typeparam>
    public sealed class Edge<TLabel>
    {
        /// <summary>The label on this edge.</summary>
        public TLabel Label { get; private set; }

        /// <summary>The node this edge points to.</summary>
        public Node<TLabel> Node { get; private set; }

        /// <summary>Initializes a new instance of <see cref="Edge{TLabel}"/>.</summary>
        /// <param name="label">The label on this edge.</param>
        /// <param name="node">The node this edge points to.</param>
        public Edge(TLabel label, Node<TLabel> node)
        {
            Label = label;
            Node = node;
        }
    }
}
