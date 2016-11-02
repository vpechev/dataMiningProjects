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
            
            RandomInitBoard(board);

            int[] queensConflictsCount = InitQueensConflictsArray(board);

            int limit = 4 * n;
            for (int i = 0; i < limit; i++)
            {
                SwapQueens(board, queensConflictsCount);

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

        private static void RandomInitBoard(int[] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                var newValue = random.Next(board.Length);
                board[i] = newValue;
            }
        }

        //private static int GetMinConflictsColumn(int[] board)
        //{
        //    int minConflictsColCount = GetCertainColumnConflictsCount(board, 0);
        //    int minConflictsColCountIndex = 0;

        //    for (int i = 1; i < board.Length; i++)
        //    {
        //        int currentConflictsCount = GetCertainColumnConflictsCount(board, i);
        //        if (minConflictsColCount > currentConflictsCount)
        //        {
        //            minConflictsColCount = currentConflictsCount;
        //            minConflictsColCountIndex = i;
        //        }
        //    }

        //    return minConflictsColCountIndex;
        //}

        private static int GetMinConflictsQueenRow(int[] swapQueensConflictsArr)
        {
            return Array.IndexOf(swapQueensConflictsArr, swapQueensConflictsArr.Min());
        }

        private static int GetMaxConflictsQueenRow(int[] swapQueensConflictsArr)
        {
            return Array.IndexOf(swapQueensConflictsArr, swapQueensConflictsArr.Max());
        }

        private static void SwapQueens(int[] board, int[] queensConflictsCountArray)
        {
            int minConflictsColumnIndex = random.Next(board.Length);
            int maxConflictsColumnIndex;

            //do
            //{
                //minConflictsColumnIndex = GetMinConflictsQueenRow(queensConflictsCountArray);
                maxConflictsColumnIndex = GetMaxConflictsQueenRow(queensConflictsCountArray);
            //} while (minConflictsColumnIndex == maxConflictsColumnIndex);


            int minConflictsColumn = board[minConflictsColumnIndex];
            board[minConflictsColumnIndex] = board[maxConflictsColumnIndex];
            board[maxConflictsColumnIndex] = minConflictsColumn;

            queensConflictsCountArray = InitQueensConflictsArray(board);
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

        private static int[] InitQueensConflictsArray(int[] board)
        {
            int[] queensConflictsCount = new int[board.Length];
            for (int i = 0; i < board.Length; i++)
            {
                int currentElement = board[i];
                for (int j = 0; j < board.Length; j++)
                {
                    if (i != j)
                    {
                        //current column(i) has other queen
                        //unpossible because the columns are array indexes and they are unique

                        //current row(board[i]) has other queen
                        if (board[j] == currentElement)
                            queensConflictsCount[i]++;

                        //current diagonal has other queen
                        if (Math.Abs(currentElement - board[j]) == Math.Abs(i - j))
                            queensConflictsCount[i]++;
                    }
                }
            }

            return queensConflictsCount;
        }

        private static bool HasConflicts(int[] queensConflitsCountArr)
        {
            for (int i = 0; i < queensConflitsCountArr.Length; i++)
            {
                if (queensConflitsCountArr[i] > 0)
                    return true;
            }

            return false;
        }

        private static void PrintBoard(int[] board)
        {
            Console.WriteLine(string.Join(" ", board));
        }
    }
}