using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class StateUtil
    {
        public static void printState(int[] state)
        {
            Console.WriteLine("=======");
            printStateRow(state, 0, 3);
            printStateRow(state, 3, 6);
            printStateRow(state, 6, 9);
            Console.WriteLine("\n=======\n\n");
        }

        private static void printStateRow(int[] state, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                if (state[i] == Constants.MAX_PLAYER_SYMBOL)
                    Console.Write("x ");
                else if (state[i] == Constants.MIN_PLAYER_SYMBOL)
                    Console.Write("o ");
                else
                    Console.Write("_ ");
            }
            Console.WriteLine();
        }
    }
}
