using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class GameUtil
    {
        //0 1 2
        //3 4 5
        //6 7 8

        public static void PlayGame()
        {
            bool hasVictory = false;

            State state = new State()
            {
                StateConfiguration = new int[9]
            };


            int[] bestFirstChoise = { 0/*, 2, 4, 6, 8 */};
            Random random = new Random();
            if (true) { 
                state.StateConfiguration[bestFirstChoise[random.Next(bestFirstChoise.Length - 1)]] = Constants.MAX_PLAYER_SYMBOL;
            }

            Console.WriteLine("Computer: ");
            StateUtil.printState(state.StateConfiguration);

            for (int i = 0; i < 4; i++)
            {
                //Player turn
                MarkMinPlayerChoise(state); // the player who inputs from the keyboard

                Console.WriteLine("You: ");
                StateUtil.printState(state.StateConfiguration);

                if (IsWinner(state, false))
                {
                    hasVictory = true;
                    break;
                }

                //Computer turn
                state.StateConfiguration = MinimaxEngine.MinimaxDecision(state.StateConfiguration);
                Console.WriteLine("Computer: ");
                StateUtil.printState(state.StateConfiguration);

                if (IsWinner(state, true))
                {
                    hasVictory = true;
                    break;
                }
            }

            if (!hasVictory)
            {
                Console.WriteLine("Try again! Noone won :(");
            }
        }

        private static bool IsWinner(State state, bool IsComputer)
        {
            if (MinimaxEngine.IsTerminal(state.StateConfiguration))
            {
                if (IsComputer)
                    Console.WriteLine("You lose! ;(");
                else
                    Console.WriteLine("You won! :)");
                return true;
            }

            return false;
        }



        private static void MarkMinPlayerChoise(State state)
        {
            int minPlayerChoise = -1;
            do
            {
                Console.Write("Your turn: ");
                Int32.TryParse(Console.ReadLine(), out minPlayerChoise);
            } while (minPlayerChoise - 1 < 0 || minPlayerChoise - 1 > 8 || state.StateConfiguration[minPlayerChoise - 1] == Constants.MAX_PLAYER_SYMBOL); // have to choose valid combination

            state.StateConfiguration[minPlayerChoise - 1] = Constants.MIN_PLAYER_SYMBOL;
        }
    }
}
