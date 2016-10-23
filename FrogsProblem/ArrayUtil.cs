using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    public class ArrayUtil
    {
        public static string InitializeArray(int n)
        {
            return new String(Program.LeftFrogSymbol, n) + Program.FreePlaceSymbol + new String(Program.RightFrogSymbol, n);
        }

        public static void PrintArray(string array)
        {
            Console.WriteLine(array);
        }

        public static int FindIndexOfFreePlace(string list, char freePlaceSymbol)
         {
             return list.IndexOf(freePlaceSymbol);
         }
    }
}
