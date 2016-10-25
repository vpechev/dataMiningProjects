using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingBlocks
{
    public class FindDistanceUtil
    {
        public static int FindManhattanDistance(int[,] firstArray, int[,] secondArray)
        {
            int sum = 0;

            for (int x = 0; x < firstArray.GetLength(0); x += 1)
            {
                for (int y = 0; y < firstArray.GetLength(1); y += 1)
                {
                    int firstArrEl = firstArray[x, y];
                    Dictionary<char, int> place = FindPlaceOfElement(secondArray, firstArrEl);
                    foreach (KeyValuePair<char, int> kvp in place)
                    {
                        if (kvp.Key == 'x')
                        {
                            sum += Math.Abs(x - kvp.Value);
                        }
                        else if (kvp.Key == 'y')
                        {
                            sum += Math.Abs(y - kvp.Value);
                        }
                    }  
                }
            }

            return sum;
        }

        public static int FindHemingDistance(int[,] firstArray, int[,] secondArray)
        {
            int sum = 0;

            for (int x = 0; x < firstArray.GetLength(0); x += 1)
            {
                for (int y = 0; y < firstArray.GetLength(1); y += 1)
                {
                    if( firstArray[x, y] != secondArray[x, y] ) {
                        sum += 1;
                    }
                }
            }

            return sum;
        }

        private static Dictionary<char, int> FindPlaceOfElement(int[,] array, int element)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    if (array[x, y] == element)
                    {
                        return new Dictionary<char, int>
                        {
                            { 'x', x },
                            { 'y', y }
                        };
                    }

                }
            }
            return null;
        }
    }
}
