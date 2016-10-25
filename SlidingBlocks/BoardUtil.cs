using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingBlocks
{
    public class BoardUtil
    {
        public static void GenerateChildren(Node currentNode)
        {
            int x = 0, y = 0;
            foreach (KeyValuePair<char, int> kvp in FindEmptyElement(currentNode.Board))
            {
                if (kvp.Key == 'x')
                {
                    x = kvp.Value;
                }
                else if (kvp.Key == 'y')
                {
                    y = kvp.Value;
                }
            }

            if (x > 0)
            {   //up
                AddChildToNode(currentNode, x, y, x - 1, y);
            }

            if (x < currentNode.Board.GetLength(0) - 1)
            {   //down
                AddChildToNode(currentNode, x, y, x + 1, y);
            }

            if (y > 0)
            {   //left
                AddChildToNode(currentNode, x, y, x, y - 1);
            }

            if (y < currentNode.Board.GetLength(0) - 1)
            {   //right
                AddChildToNode(currentNode, x, y, x, y + 1);
            }
        }

        public static bool AreBoardsEqual(int[,] firstBoard, int[,] secondBoard)
        {
            for (int x = 0; x < firstBoard.GetLength(0); x++)
            {
                for (int y = 0; y < firstBoard.GetLength(1); y++)
                {
                    if (firstBoard[x, y] != secondBoard[x,y])
                    {
                        return false;
                    }

                }
            }
            return true;
        }

        public static bool ContainsVisitedListCertainBoard(List<int[,]> visited, int[,] board)
        {
            foreach (var currentVisitedBoard in visited)
            {
                if (AreBoardsEqual(currentVisitedBoard, board))
                    return true;
            }

            return false;
        }

        public static int[,] CopyBoard(int[,] board)
        {
            int[,] newBoard = new int[board.GetLength(0), board.GetLength(0)];

            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    newBoard[x, y] = board[x, y];
                }
            }
            return newBoard;
        }

        public static void SwapBoardValues(int[,] board, int currentX, int currentY, int newX, int newY)
        {
            int a = board[currentX, currentY];
            board[currentX, currentY] = board[newX, newY];
            board[newX, newY] = a;
            
        }

        private static void AddChildToNode(Node oldNode, int currentX, int currentY, int newX, int newY)
        {
            var newBoard = CopyBoard(oldNode.Board);
            SwapBoardValues(newBoard, currentX, currentY, newX, newY);
            var newNode = new Node(){
                Board = newBoard,
                Distance = oldNode.Distance + 1
            };
            oldNode.ChildrenBoards.Add(newNode);
        }
                

        public static Dictionary<char, int> FindEmptyElement(int[,] board)
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] == 0)
                    {
                        return new Dictionary<char, int>
                        {
                            { 'x', x },
                            { 'y', y }
                        };
                    }

                }
            }
            throw new Exception("Missing 0 elemnt");
        }

        public static void PrintBoard(int[,] board)
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(0); y++)
                {
                    Console.Write("{0} ", board[x,y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------");
        }
    }
}
