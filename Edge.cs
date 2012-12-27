﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Util
{
    /// <summary>Encapsulates an edge in the graph for Dijkstra’s Algorithm, containing a weight, a label and a target node.</summary>
    /// <typeparam name="TWeight">Type of the weight of each edge.</typeparam>
    /// <typeparam name="TLabel">Type that can be used to identify this edge.</typeparam>
    public sealed class Edge<TWeight, TLabel>
    {
        /// <summary>The weight of this edge.</summary>
        public TWeight Weight { get; private set; }

        /// <summary>The label on this edge.</summary>
        public TLabel Label { get; private set; }

        /// <summary>The node this edge points to.</summary>
        public Node<TWeight, TLabel> Node { get; private set; }

        /// <summary>Initializes a new instance of <see cref="Edge{TLabel}"/>.</summary>
        /// <param name="label">The label on this edge.</param>
        /// <param name="weight">The weight of this edge.</param>
        /// <param name="node">The node this edge points to.</param>
        public Edge(TWeight weight, TLabel label, Node<TWeight, TLabel> node)
        {
            Weight = weight;
            Label = label;
            Node = node;
        }
    }
}
