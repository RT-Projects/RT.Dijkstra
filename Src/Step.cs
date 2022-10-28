using System;

namespace RT.Dijkstra
{
    /// <summary>
    ///     Describes a step in the path returned by <see cref="DijkstrasAlgorithm.Run{TWeight, TLabel}(Node{TWeight, TLabel},
    ///     TWeight, Func{TWeight, TWeight, TWeight}, out TWeight)"/>.</summary>
    /// <typeparam name="TWeight">
    ///     Type of the weight (or length or any other quantity to be minimized) of each edge between nodes.</typeparam>
    /// <typeparam name="TLabel">
    ///     Type that is used to identify edges.</typeparam>
    public struct Step<TWeight, TLabel> where TWeight : IComparable<TWeight>
    {
        /// <summary>
        ///     The node from which this step originates. If this node has <see cref="Node{TWeight, TLabel}.IsFinal"/> equal
        ///     to <c>true</c>, this represents the end of the path.</summary>
        public Node<TWeight, TLabel> Node { get; private set; }
        /// <summary>
        ///     The label of the edge connecting this <see cref="Node"/> to the next. Note that if <see cref="Node"/> has <see
        ///     cref="Node{TWeight, TLabel}.IsFinal"/> equal to <c>true</c>, this value is meaningless and should be ignored.</summary>
        public TLabel Label { get; private set; }

        /// <summary>Constructor.</summary>
        public Step(Node<TWeight, TLabel> node, TLabel label)
        {
            Node = node;
            Label = label;
        }

        /// <summary>Deconstructor.</summary>
        public void Deconstruct(out Node<TWeight, TLabel> node, out TLabel label)
        {
            node = Node;
            label = Label;
        }
    }
}
