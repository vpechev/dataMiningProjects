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

        public Node(Node parent, int[] combination)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            this.Parent = parent;
            this.Combination = new int[combination.Length];
            for (int i = 0; i < combination.Length; i++)
            {
                this.Combination[i] = combination[i];
            }
            this.Children = new List<Node>();
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("New Node creation: {0} ms", elapsedMs);
        }

        public Node Parent { get; set; }
        public List<Node> Children { get; set; }
        public int[] Combination { get; set; }
        public bool IsExit { get; set; }
    }
}
