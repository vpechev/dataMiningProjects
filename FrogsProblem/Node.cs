using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    class Node
    {
        public Node Parent { get; set; }
        public List<Node> Children { get; set; }
        public int[] Combination { get; set; }
    }
}
