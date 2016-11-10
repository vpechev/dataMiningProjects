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

        static void Main(string[] args)
        {
            int n = 1000;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (n < 4)
            {
                Console.WriteLine("{0} queens cannot be placed in this board", n);
                return;
            }

            PlaceQueens(n);
            
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Total time: {0} ms", elapsedMs);

            Console.ReadLine();
        }

        public static void PlaceQueens(int n)
        {
            int[] board = new int[n];
            
            RandomInitBoard(board);
            
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

        private static int GetMaxConflictsQueenRow(int[] swapQueensConflictsArr)
        {
            var indexes = new List<int>() { 0 };
            int max = swapQueensConflictsArr[0];
            for (int i = 1; i < swapQueensConflictsArr.Length; i++)
            {
                if (swapQueensConflictsArr[i] == max)
                {
                    indexes.Add(i);
                }
                else if (swapQueensConflictsArr[i] > max)
                {
                    max = swapQueensConflictsArr[i];
                    indexes = new List<int>() { i };
                }
            }
                
            return indexes[random.Next(indexes.Count)];
        }

        private static int[] SwapQueens(int[] board, int[] queensConflictsCountArray)
        {
            int maxConflictsColumnIndex = GetMaxConflictsQueenRow(queensConflictsCountArray);

            var currentQueenConflictsCount = queensConflictsCountArray[maxConflictsColumnIndex];
            var minConflictsRowsIndexesArr = new List<int>();
            var minConflicts = currentQueenConflictsCount;

            for (int i = 0; i < board.Length; i++) //current col
            {
                if (i != board[maxConflictsColumnIndex]) //row check
                {
                    board[maxConflictsColumnIndex] = i;
                    var conflictsCount = GetCertainColumnConflictsCount(board, maxConflictsColumnIndex);
                    if (conflictsCount < minConflicts)
                    {
                        minConflicts = conflictsCount;
                        minConflictsRowsIndexesArr.Clear();
                        minConflictsRowsIndexesArr.Add(i);
                    }
                    else if (conflictsCount == minConflicts)
                    {
                        minConflictsRowsIndexesArr.Add(i);
                    }
                }
            }

            //decrease conflicts indexes
            //DereaseConflictsCount(board, maxConflictsColumnIndex, queensConflictsCountArray);

            //swap 
            int swapIndex = random.Next(minConflictsRowsIndexesArr.Count);
            board[maxConflictsColumnIndex] = minConflictsRowsIndexesArr[swapIndex];

            //increase conflicts indexes
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
            return queensConflitsCountArr.Count(x => x > 0) > 0;
        }

        private static void PrintBoard(int[] board)
        {
            Console.WriteLine(string.Join(" ", board));
        }
    }
}