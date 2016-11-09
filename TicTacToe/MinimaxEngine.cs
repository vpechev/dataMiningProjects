using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe
{
    public static class MinimaxEngine
    {
        public static int[] MinimaxDecision(int[] state)
        {
            int alfa = Int32.MinValue;
            int beta = Int32.MaxValue;

            List<State> successors = GenerateSuccessors(state);
            foreach (var s in successors)
            {
                int utilityValue = MinValue(s.StateConfiguration, alfa, beta);
                s.UtilityValue = utilityValue;
            }

            //return the a in Actions(state) - all possible moves maximizing Min-Value(Result(a, state))
            return successors.Max().StateConfiguration;
        }

        public static int MaxValue(int[] state, int alfa, int beta)
        {
            if (IsTerminal(state))
            {
                return ComputeUtilityValue(state);
            }
            else
            {
                int v = Int32.MinValue;
                foreach (State s in GenerateSuccessors(state))
                {
                    v = Math.Max(v, MinValue(s.StateConfiguration, alfa, beta));

                    if (v >= beta)
                        return v; //prunning 
                    else
                        alfa = Math.Max(alfa, v);
                }
                return v;
            }
        }

        private static int MinValue(int[] state, int alfa, int beta)
        {
            if (IsTerminal(state))
            {
                return ComputeUtilityValue(state);
            }
            else
            {
                int v = Int32.MinValue;
                foreach (State s in GenerateSuccessors(state))
                {
                    v = Math.Max(v, MaxValue(s.StateConfiguration, alfa, beta));

                    if (v <= alfa)
                        return v; //prunning 
                    else
                        beta = Math.Max(beta, v);
                }
                return v;
            }
        }

        private static List<State> GenerateSuccessors(int[] state)
        {
            List<State> successors = new List<State>();
            for (int i = 0; i < state.Length; i++)
            {
                if (state[i] == Constants.FREE_PLACE_SYMBOL)
                {
                    int[] newSuccessor = new int[state.Length];
                    Array.Copy(state, newSuccessor, state.Length);
                    newSuccessor[i] = Constants.MAX_PLAYER_SYMBOL;
                    successors.Add(new State() { StateConfiguration = newSuccessor });
                }
            }

            return successors;
        }

        private static int ComputeUtilityValue(int[] state)
        {
            int[,] threeInALine = {
                      { 0, 1, 2 },
                      { 3, 4, 5 },
                      { 6, 7, 8 },
                      { 0, 3, 6 },
                      { 1, 4, 7 },
                      { 2, 5, 8 },
                      { 0, 4, 8 },
                      { 2, 4, 6 }
            };

            int[,] heuristingArray = {
                {     0,   -10,  -100, -1000 },
                {    10,     0,     0,     0 },
                {   100,     0,     0,     0 },
                {  1000,     0,     0,     0 }
            };

            int utilityValue = 0;
  
            for (int i = 0; i < 8; i++)  {
                int maxPoints = 0, minPoints = 0;
                
                for (int j = 0; j < 3; j++)  {
                    var piece = state[threeInALine[i, j]];
      
                    if (piece == Constants.MAX_PLAYER_SYMBOL){
                        maxPoints++;
                    }
                    else if (piece == Constants.MIN_PLAYER_SYMBOL)
                    {
                        minPoints++;
                    }
                }

                utilityValue += heuristingArray[maxPoints, minPoints];
            }

            return utilityValue;
        }

        public static bool IsTerminal(int[] state)
        {
            //0 1 2
            //3 4 5
            //6 7 8
            if ((state[0] == state[1] && state[1] == state[2] && state[2] != Constants.FREE_PLACE_SYMBOL) // first row
                    || (state[3] == state[4] && state[4] == state[5] && state[5] != Constants.FREE_PLACE_SYMBOL) // second row
                    || (state[6] == state[7] && state[7] == state[8] && state[8] != Constants.FREE_PLACE_SYMBOL) // third row
                    || (state[0] == state[3] && state[3] == state[6] && state[6] != Constants.FREE_PLACE_SYMBOL) // first col
                    || (state[1] == state[4] && state[4] == state[7] && state[7] != Constants.FREE_PLACE_SYMBOL) // second col
                    || (state[2] == state[5] && state[5] == state[8] && state[8] != Constants.FREE_PLACE_SYMBOL) // third col
                    || (state[0] == state[4] && state[4] == state[8] && state[8] != Constants.FREE_PLACE_SYMBOL) // main diagonal
                    || (state[2] == state[4] && state[4] == state[6] && state[6] != Constants.FREE_PLACE_SYMBOL) // reversed diagonal
                    )
            {
                return true;
            }
            return false;
        }
    }
}
