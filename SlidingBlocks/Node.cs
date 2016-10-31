using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingBlocks
{
    public class Node : IComparable<Node>
    {
        //private int _evaluationValue;
        //private int _heuristic;
        //private int _distance;
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

        //public override bool Equals(object obj)
        //{
        //    if (obj == null) 
        //        return false;
        //    else if (BoardUtil.AreBoardsEqual(this.Board, ((Node)obj).Board))
        //        return true;
        //    else 
        //        return Equals(obj);
        //}

        public int CompareTo(Node other)
        {
            return this.GetEvaluationFuction().CompareTo(other.GetEvaluationFuction()); 
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null)
        //    {
        //        return false;
        //    }

        //    Node other = (Node)obj;

        //    if (BoardUtil.AreBoardsEqual(this.Board, ((Node)obj).Board))
        //        return true;
        //    else
        //        return false;
        //}

        //public override int GetHashCode()
        //{
        //    return this.GetEvaluationFuction();
        //} 
    }
}
