using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NQueensProblem
{
    class Program
    {
        private static Random random = new Random();
        private static int QueenSymbol = 1;
        static void Main(string[] args)
        {
            int n = 4;
            if (n < 4)
            {
                Console.WriteLine("{0} queens cannot be placed in this board", n);
                return;
            }

            PlaceQueens(n);
            Console.ReadLine();
        }

        public static void PlaceQueens(int n)
        {
            int[] board = new int[n];
            int[,] conflitsCountCellBoard = new int[n,n];
            
            List<int> usedRows = RandomInitBoard(board);

            //for (int i = 0; i < board.Length; i++)
            //{
            //    if (usedRows.Contains(board[i]))
            //    {
            //        int currentQueenPlace = conflitsCountCellBoard[ board[i], i ];
                
            //        for (int j = 0; j < n; j++)
            //        {
            //            //increase column conflicts for current cell
            //            conflitsCountCellBoard[i, board[j]] += 1;
            //            //increase row conflicts for current cell
            //            conflitsCountCellBoard[board[j], i] += 1;
            //        }
                
            //        //increase diagonal conflicts for each cell
            //        for (int j = 0; j < board.GetLength(0); j++)
            //        {
            //            conflitsCountCellBoard[i, i+1] += 1;
            //        }
            //    }
            //}

            int limit = 4 * n;
            for (int i = 0; i < limit; i++)
            {
                SwapQueens(board);

                //PrintBoard(board);
                //Console.WriteLine("{0} ", GetNumberOfConflicts(board, 0));
                //Console.WriteLine("{0} ", GetNumberOfConflicts(board, 1));
                //Console.WriteLine("{0} ", GetNumberOfConflicts(board, 2));
                //Console.WriteLine("{0} ", GetNumberOfConflicts(board, 3));

                if (!HasConflicts(board))
                {
                    Console.WriteLine("Yeah the queens are places");
                    PrintBoard(board);
                    return;
                }
            }

            if (HasConflicts(board))
            {
                Console.WriteLine("New Recursive call");
                PlaceQueens(n);
            }
        }

        private static List<int> RandomInitBoard(int[] board)
        {
            List<int> usedRows = new List<int>();
            for (int i = 0; i < board.Length; i++)
            {
                var newValue = random.Next(board.Length);
                
                if(!usedRows.Contains(newValue)){
                    usedRows.Add(newValue);
                }

                board[i] = newValue;
            }

            return usedRows;
        }

        private static int GetMinConflictsColumn(int[] board)
        {
            int minConflictsColCount = GetCertainColumnConflictsCount(board, 0);
            int minConflictsColCountIndex = 0;

            for (int i = 1; i < board.Length; i++)
            {
                int currentConflictsCount = GetCertainColumnConflictsCount(board, i);
                if (minConflictsColCount > currentConflictsCount)
                {
                    minConflictsColCount = currentConflictsCount;
                    minConflictsColCountIndex = i;
                }
            }

            return minConflictsColCountIndex;
        }

        private static int GetMaxConflictsColumn(int[] board)
        {
            int maxConflictsColCount = GetCertainColumnConflictsCount(board, 0);
            int maxConflictsColCountIndex = 0;

            for (int i = 1; i < board.Length; i++)
            {
                int currentConflictsCount = GetCertainColumnConflictsCount(board, i);
                if (maxConflictsColCount < currentConflictsCount)
                {
                    maxConflictsColCount = currentConflictsCount;
                    maxConflictsColCountIndex = i;
                }
            }

            return maxConflictsColCountIndex;
        }

        private static void SwapQueens(int[] board)
        {
            int minConflictsColumnIndex;
            int maxConflictsColumnIndex;

            do
            {
                minConflictsColumnIndex = GetMinConflictsColumn(board);
                maxConflictsColumnIndex = GetMaxConflictsColumn(board);
            } while (minConflictsColumnIndex == maxConflictsColumnIndex);

            int minConflictsColumn = board[minConflictsColumnIndex];
            board[minConflictsColumnIndex] = board[maxConflictsColumnIndex];
            board[maxConflictsColumnIndex] = minConflictsColumn;
        }

        private static int GetCertainColumnConflictsCount(int[] board, int colIndex)
        {
            int conflictsCount = 0;

            for (int i = 0; i < board.Length; i++)
            {
                if (i != colIndex)
                {
                    if (board[i] == board[colIndex])
                        conflictsCount++;
                    else if (Math.Abs(board[colIndex] - board[i]) == Math.Abs(i - colIndex))
                        conflictsCount++;
                }
            }
            return conflictsCount;
        }

        private static bool HasConflicts(int[] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                int currentElement = board[i];
                for (int j = i + 1; j < board.Length; j++)
                {
                    if (board[j] == currentElement)
                        return true;
                    else if (Math.Abs(currentElement - board[j]) == Math.Abs(i - j))
                        return true;
                }
            }

            return false;
        }

        private static void PrintBoard(int[] board)
        {
            Console.WriteLine(string.Join(" ", board));
        }
    }
}