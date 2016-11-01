using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingBlocks
{
    public class Node : IComparable<Node>
    {
        private List<Node> _childrenBoards;

        public int Heuristic {
            get { return FindDistanceUtil.FindManhattanDistance(this.Board, Program.EXIT_BOARD); }
            set {  } 
        }
        public int Distance { get; set; } 

        public int[,] Board { get; set; }

        public string MoveDirection { get; set; }

        public List<Node> ChildrenBoards
        {
            get {
                if (_childrenBoards == null) {
                    _childrenBoards = new List<Node>();
                }
                return _childrenBoards;
            }
            set { }
        }

        public int GetEvaluationFuction()
        {
            return Heuristic + Distance;
        }

        public int CompareTo(Node other)
        {
            int val = this.GetEvaluationFuction().CompareTo(other.GetEvaluationFuction());
            if (val != 0)
            {
                return val;
            }
            else if (BoardUtil.AreBoardsEqual(this.Board, other.Board))
            {
                return 0;
            }
            else
                return -1;

        }
    }
}
