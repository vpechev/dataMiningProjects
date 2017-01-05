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

            List<State> successors = GenerateSuccessors(state, Constants.MAX_PLAYER_SYMBOL);
            foreach (var s in successors)
            {
                int utilityValue = MinValue(s.StateConfiguration, alfa, beta);
                s.UtilityValue = utilityValue;
            }

            //return the a in Actions(state) - all possible moves maximizing Min-Value(Result(a, state))
            var successorsMax = successors.Max();
            var maxSuccessorsList = successors.Where(x => x.UtilityValue == successorsMax.UtilityValue).ToList();
            var random = new Random(System.DateTime.Now.Millisecond);
            return maxSuccessorsList[random.Next(0, maxSuccessorsList.Count)].StateConfiguration;
        }

        public static int MaxValue(int[] state, int alfa, int beta)
        {
            if (IsTerminal(state))
            {
                return ComputeUtilityValue(state, true);
            }
            else
            {
                int v = Int32.MinValue;
                foreach (State s in GenerateSuccessors(state, Constants.MAX_PLAYER_SYMBOL))
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
                return ComputeUtilityValue(state, false);
            }
            else
            {
                int v = Int32.MinValue;
                foreach (State s in GenerateSuccessors(state, Constants.MIN_PLAYER_SYMBOL))
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

        private static List<State> GenerateSuccessors(int[] state, int playerSymbol)
        {
            List<State> successors = new List<State>();
            for (int i = 0; i < state.Length; i++)
            {
                if (state[i] == Constants.FREE_PLACE_SYMBOL)
                {
                    int[] newSuccessor = new int[state.Length];
                    Array.Copy(state, newSuccessor, state.Length);
                    newSuccessor[i] = playerSymbol;
                    successors.Add(new State() { StateConfiguration = newSuccessor });
                }
            }

            return successors;
        }

        private static int ComputeUtilityValue(int[] state, bool isMax)
        {
            int[,] threeInALineArr = {
                      { state[0], state[1], state[2] },  //First row
                      { state[3], state[4], state[5] },  //Second row
                      { state[6], state[7], state[8] },  //Third row
                      { state[0], state[3], state[6] },  //First column
                      { state[1], state[4], state[7] },  //Second column
                      { state[2], state[5], state[8] },  //Third column
                      { state[0], state[4], state[8] },  //Main diagonal
                      { state[2], state[4], state[6] }   //Reversed diagonal
            };

            int threeInALineValue = 100,
                twoInALineValue = 10,
                oneInALineValue = 1,
                emptyValue = 0;

            int utilityValue = 0;

            for (int i = 0; i < threeInALineArr.GetLength(0); i++)
            {
                if (threeInALineArr[i, 0] == threeInALineArr[i, 1] && threeInALineArr[i, 0] == threeInALineArr[i, 2] && threeInALineArr[i, 1] == threeInALineArr[i, 2])
                {
                    if (threeInALineArr[i, 0] == Constants.MAX_PLAYER_SYMBOL && isMax)
                        utilityValue += threeInALineValue;
                    else if (threeInALineArr[i, 0] == Constants.MIN_PLAYER_SYMBOL && !isMax)
                        utilityValue -= threeInALineValue;
                    break;
                }
                else if (twoEqualPlaces(threeInALineArr[i, 0], threeInALineArr[i, 1], threeInALineArr[i, 2]))
                {
                    if (threeInALineArr[i, 0] == Constants.MAX_PLAYER_SYMBOL && isMax)
                        utilityValue += twoInALineValue;
                    else if (threeInALineArr[i, 0] == Constants.MIN_PLAYER_SYMBOL && !isMax)
                        utilityValue -= twoInALineValue;
                }
                else if (oneEqualPlaces(threeInALineArr[i, 0], threeInALineArr[i, 1], threeInALineArr[i, 2]))
                {
                    if (threeInALineArr[i, 0] == Constants.MAX_PLAYER_SYMBOL && isMax)
                        utilityValue += oneInALineValue;
                    else if (threeInALineArr[i, 0] == Constants.MIN_PLAYER_SYMBOL && !isMax)
                        utilityValue -= oneInALineValue;
                }
                else
                {
                    utilityValue += emptyValue;
                }
            }

            return utilityValue;
        }

        private static bool twoEqualPlaces(int firstElement, int secondElement, int thirdElement)
        {
            if (   firstElement == secondElement && thirdElement == Constants.FREE_PLACE_SYMBOL && firstElement != Constants.FREE_PLACE_SYMBOL
                || firstElement == thirdElement && secondElement == Constants.FREE_PLACE_SYMBOL && firstElement != Constants.FREE_PLACE_SYMBOL
                || secondElement == thirdElement && firstElement == Constants.FREE_PLACE_SYMBOL && secondElement!= Constants.FREE_PLACE_SYMBOL)
                return true;

            return false;
        }

        private static bool oneEqualPlaces(int firstElement, int secondElement, int thirdElement)
        {
            if (   firstElement != Constants.FREE_PLACE_SYMBOL && secondElement == thirdElement && secondElement == Constants.FREE_PLACE_SYMBOL
                || secondElement != Constants.FREE_PLACE_SYMBOL && firstElement == thirdElement && firstElement == Constants.FREE_PLACE_SYMBOL
                || thirdElement != Constants.FREE_PLACE_SYMBOL && firstElement == secondElement && firstElement == Constants.FREE_PLACE_SYMBOL)
                return true;

            return false;
        }

        //private static int ComputeUtilityValue(int[] state)
        //{
        //    int[,] threeInALine = {
        //              { 0, 1, 2 },  //First row
        //              { 3, 4, 5 },  //Second row
        //              { 6, 7, 8 },  //Third row
        //              { 0, 3, 6 },  //First column
        //              { 1, 4, 7 },  //Second column
        //              { 2, 5, 8 },  //Third column
        //              { 0, 4, 8 },  //Main diagonal
        //              { 2, 4, 6 }   //Reversed diagonal
        //    };

        //    int[,] heuristingArray = {
        //        {     0,   -10,  -100, -1000 },
        //        {    10,     0,     0,     0 },
        //        {   100,     0,     0,     0 },
        //        {  1000,     0,     0,     0 }
        //    };

        //    int utilityValue = 0;
  
        //    for (int i = 0; i < state.Length; i++)
        //    {
        //        int maxPoints = 0, minPoints = 0;
        //        if (state[i] == Constants.MAX_PLAYER_SYMBOL){
        //                maxPoints++;
        //        }
        //        else if (state[i] == Constants.MIN_PLAYER_SYMBOL)
        //        {
        //            minPoints++;
        //        }
        //        utilityValue += heuristingArray[minPoints, maxPoints];
        //    }

        //    return utilityValue;
        //}

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
