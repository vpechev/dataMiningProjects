using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    public class ArrayUtil
    {
        public static int[] InitializeArray(int n)
        {
            int[] array = new int[2 * n + 1];
            int halfSize = array.Length / 2;
            for (int i = 0; i < halfSize; i++)
            {
                array[i] = 1;
                array[halfSize + i + 1] = 2;
            }
            array[halfSize] = 0;
            return array;
        }

        public static void PrintArray(int[] array)
        {
            Console.WriteLine(string.Join(" ", array));
        }

        public static bool AreEqual(int[] first, int[] second)
        {
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                    return false;
            }
            return true;
        }

        public static int FindIndexOfFreePlace(int[] list, int freePlaceSymbol)
         {
             for (int i = 0; i < list.Length; i++)
             {
                 if (list[i] == freePlaceSymbol)
                     return i;
             }
             throw new Exception();
         }
    }
}
