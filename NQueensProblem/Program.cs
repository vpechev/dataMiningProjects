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
        public static int COUNTER = 0;
        static void Main(string[] args)
        {
            int n = 10;
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
            //PrintBoard(board);
            int[] queensConflictsCount = InitQueensConflictsArray(board);

            if (!HasConflicts(queensConflictsCount))
            {
                Console.WriteLine("Yeah the queens are places");
                PrintBoard(board);
                return;
            }

            int limit = n * n;
            for (int i = 0; i < limit; i++)
            {
                queensConflictsCount = SwapQueens(board, queensConflictsCount);

                if (!HasConflicts(queensConflictsCount))
                {
                    Console.WriteLine("Yeah the queens are places");
                    PrintBoard(board);
                    return;
                }
            }

            if (HasConflicts(queensConflictsCount))
            {
                Console.WriteLine("New Recursive call");
                PlaceQueens(n);
            }
        }

        private static void RandomInitBoard(int[] board)
        {
            List<int> placedValues = new List<int>();
            bool isPlaceAllowed = false;
            for (int i = 0; i < board.Length; i++)
            {
                do
                {
                    var newValue = random.Next(board.Length);
                    isPlaceAllowed = isPlacingAllowed(placedValues, newValue, board, i);
                } while (!isPlaceAllowed);
            }
        }

        private static bool isPlacingAllowed(List<int> placedValues, int newValue, int[] board, int index)
        {
            if (!placedValues.Contains(newValue))
            {
                board[index] = newValue;
                placedValues.Add(newValue);
                return true;
            }
            return false;
        }

        private static int GetMinConflictsQueenRow(int[] swapQueensConflictsArr)
        {
            int minIndex = Array.IndexOf(swapQueensConflictsArr, swapQueensConflictsArr.Min());
            var indexes = swapQueensConflictsArr.Where(x => x == minIndex).ToList();
            return indexes[random.Next(indexes.Count-1)];
        }

        private static int GetMaxConflictsQueenRow(int[] swapQueensConflictsArr)
        {
            int max = swapQueensConflictsArr.Max();
            var indexes = swapQueensConflictsArr.Where(x => x == max).ToList();
            return indexes[random.Next(indexes.Count - 1)];
        }

        private static int[] SwapQueens(int[] board, int[] queensConflictsCountArray)
        {
            int minConflictsColumnIndex;
            int maxConflictsColumnIndex = GetMaxConflictsQueenRow(queensConflictsCountArray);
            
            do
            {
                minConflictsColumnIndex = random.Next(board.Length-1);
            } while (minConflictsColumnIndex == maxConflictsColumnIndex);

            int minColumn = board[minConflictsColumnIndex];
            
            //decrease conflicts indexes
            //DereaseConflictsCount(board, minConflictsColumnIndex, queensConflictsCountArray);
            //DereaseConflictsCount(board, board[maxConflictsColumnIndex], queensConflictsCountArray);

            //swap 
            board[minConflictsColumnIndex] = board[maxConflictsColumnIndex];
            board[maxConflictsColumnIndex] = minColumn;

            //increase conflicts indexes
            //IncreaseConflictsCount(board, minConflictsColumnIndex, queensConflictsCountArray);
            //IncreaseConflictsCount(board, maxConflictsColumnIndex, queensConflictsCountArray);

            queensConflictsCountArray = InitQueensConflictsArray(board);

            return queensConflictsCountArray;
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
                IncreaseConflictsCount(board, i, queensConflictsCount);
            }

            return queensConflictsCount;
        }

        private static void IncreaseConflictsCount(int[] board, int index, int[] queensConflictsCountArr)
        {
            int currentElement = board[index];
            for (int j = 0; j < board.Length; j++)
            {
                if (index != j)
                {
                    //current column(i) has other queen
                    //unpossible because the columns are array indexes and they are unique

                    if (queensConflictsCountArr[index] < board.Length - 1)
                    {
                        //current row(board[i]) has other queen
                        if (board[j] == currentElement)
                            queensConflictsCountArr[index]++;
                    }

                    if (queensConflictsCountArr[index] < board.Length - 1)
                    {
                        //current diagonal has other queen
                        if (Math.Abs(currentElement - board[j]) == Math.Abs(index - j))
                            queensConflictsCountArr[index]++;
                    }
                }
            }
        }

        private static void DereaseConflictsCount(int[] board, int index, int[] queensConflictsCountArr)
        {
            int currentElement = board[index];
            for (int j = 0; j < board.Length; j++)
            {
                if (index != j)
                {
                    //current column(i) has other queen
                    //unpossible because the columns are array indexes and they are unique

                    if (queensConflictsCountArr[index] > 0) { 
                        //current row(board[i]) has other queen 
                        if (board[j] == currentElement)
                            queensConflictsCountArr[index]--;
                    }

                    if (queensConflictsCountArr[index] > 0)
                    {
                        //current diagonal has other queen
                        if (Math.Abs(currentElement - board[j]) == Math.Abs(index - j))
                            queensConflictsCountArr[index]--;
                    }
                }
            }
        }

        private static bool HasConflicts(int[] queensConflitsCountArr)
        {
            //for (int i = 0; i < queensConflitsCountArr.Length; i++)
            //{
            //    if (queensConflitsCountArr[i] > 0)
            //        return true;
            //}

            //return false;

            return queensConflitsCountArr.Count(x => x > 0) > 0;
        }

        private static void PrintBoard(int[] board)
        {
            Console.WriteLine(string.Join(" ", board));
        }
    }
}