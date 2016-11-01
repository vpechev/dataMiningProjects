using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingBlocks
{
    public class SlidingBocksSolver
    {
        public static void SolveSlidingBocks(Node root)
        {
            PriorityQueue priorityQueue = new PriorityQueue();
            List<int[,]> visited = new List<int[,]>();
            priorityQueue.Enque(root);

            Console.WriteLine("The input board is: ");
            BoardUtil.PrintBoard(root.Board);

            do
            {
                Node currentElement = priorityQueue.Deque();
                visited.Add(currentElement.Board);
                
                if (currentElement.Heuristic == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Steps for solving the puzzle: ");
                    Console.WriteLine(currentElement.MoveDirection);
                    Console.WriteLine("------------");
                    Console.WriteLine("The result board is: ");
                    BoardUtil.PrintBoard(currentElement.Board);
                    Console.WriteLine("Total distance: " + currentElement.Distance);
                    Console.WriteLine("------------");
                    return;
                }
                else
                {
                    BoardUtil.GenerateChildren(currentElement);

                    foreach (var child in currentElement.ChildrenBoards)
                    {
                        if ( !BoardUtil.ContainsVisitedListCertainBoard(visited, child.Board) )
                        {
                            priorityQueue.Enque(child);
                        }
                    }
                }
            }
            while (!priorityQueue.IsEmpty());

            throw new Exception("This puzzle cannot be solved");
        }
    }
}
