using System;
using System.Collections.Generic;

namespace RT.Dijkstra
{
    /// <summary>
    ///     Indicates that no solution could be found when running <see cref="DijkstrasAlgorithm.Run{TWeight,
    ///     TLabel}(Node{TWeight, TLabel}, TWeight, Func{TWeight, TWeight, TWeight}, out TWeight)"/>.</summary>
    /// <typeparam name="TWeight">
    ///     Type of the weight (or length or any other quantity to be minimized) of each edge between nodes.</typeparam>
    /// <typeparam name="TLabel">
    ///     Type that is used to identify edges.</typeparam>
    public class DijkstraNoSolutionException<TWeight, TLabel> : Exception
    {
        /// <summary>Contains the nodes that were visited.</summary>
        public HashSet<Node<TWeight, TLabel>> VisitedNodes { get; private set; }
        /// <summary>Construtor.</summary>
        public DijkstraNoSolutionException() { }
        /// <summary>Construtor.</summary>
        public DijkstraNoSolutionException(string message) : base(message) { }
        /// <summary>Construtor.</summary>
        public DijkstraNoSolutionException(string message, HashSet<Node<TWeight, TLabel>> hashSet) : base(message) { VisitedNodes = hashSet; }
    }
}
