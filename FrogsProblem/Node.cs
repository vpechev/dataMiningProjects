using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    public class Node
    {
        public Node()
        {

        }

        public Node(char[] combination)
        {
            this.Combination = new String(combination);
        }

        public string Combination { get; set; }
        public bool IsExit { get; set; }
    }
}
