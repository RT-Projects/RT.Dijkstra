using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Util
{
    public abstract class TreeNode<TMove> : IEquatable<TreeNode<TMove>>
    {
        public abstract bool Equals(TreeNode<TMove> other);
        public abstract override int GetHashCode();

        public abstract bool IsFinal { get; }
        public abstract IEnumerable<Edge<TMove>> Edges { get; }
    }
}
