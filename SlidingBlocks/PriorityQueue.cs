using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace SlidingBlocks
{
    public class PriorityQueue
    {
        private OrderedSet<Node> priorityQuery{ get; set; }

        public PriorityQueue()
        {
            priorityQuery = new OrderedSet<Node>();
        }

        public void Enque(Node item)
        {
            priorityQuery.Add(item);
        }

        public Node Deque()
        {   
            return priorityQuery.RemoveFirst();
        }

        public bool IsEmpty()
        {
            return priorityQuery.Count == 0;
        }
    }
}
