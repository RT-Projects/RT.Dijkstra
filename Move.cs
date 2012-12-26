using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RT.Util
{
    public sealed class Edge<TLabel>
    {
        public TLabel Label { get; private set; }
        public TreeNode<TLabel> Node { get; private set; }
        public Edge(TLabel label, TreeNode<TLabel> node)
        {
            Label = label;
            Node = node;
        }
    }
}
