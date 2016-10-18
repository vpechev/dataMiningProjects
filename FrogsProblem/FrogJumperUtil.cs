using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    class FrogJumperUtil
    {
        public static void BuildGraph(Node current)
        {
            
            //current.Children = 
            //if (children.Count == 0)
            //{
            //    return;
            //}
            //else
            //{
            //    foreach (var item in children)
            //    {
            //        BuildGraph(item);
            //    }
            //}
            //Console.WriteLine(string.Join("{0}, ", current.Combination));
           // GetPer(current, 0, current.Combination.Length - 1);

            //if (current.Children.Count != 0)
            //{
            //    GetPer(current);
            //}
            
        }

        public static void Permute(Node current, int[] valuesArray)
        {
            List<Node> permutations = new List<Node>();
            permuteHelper(current, current.Combination, 0, permutations, valuesArray);

            //foreach (var item in permutations)
            //{
            //    Console.WriteLine(string.Join(" ",item));
            //}
            current.Children = permutations;
        }

        private static void permuteHelper(Node current, int[] arr, int index, List<Node> permutations, int[] valuesArray)
        {
            if(index > arr.Length - 1){ //If we are at the last element - nothing left to permute
                if (HasMove(arr, valuesArray))
                {
                    int[] newCombinationArray = new int[arr.Length];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        newCombinationArray[i] = arr[i];
                    }
                
                    permutations.Add(new Node(){
                                        Combination = newCombinationArray,
                                        Parent = current
                                    });
                }
            }

            for(int i = index; i < arr.Length; i++){ //For each index in the sub array arr[index...end]

                //Swap the elements at indices index and i
                int t = arr[index];
                arr[index] = arr[i];
                arr[i] = t;

                //Recurse on the sub array arr[index+1...end]
                permuteHelper(current, arr, index + 1, permutations, valuesArray);

                //Swap the elements back
                t = arr[index];
                arr[index] = arr[i];
                arr[i] = t;
            }
        }


        //private static void GetPer(int[] list, int start, int end, List<int[]> resultArr)
        //{
        //    if (start == end)
        //    {
        //        resultArr.Add(list);
        //    }
        //    else
        //        for (int i = start; i <= end; i++)
        //        {
        //            Swap(list[start], list[i]);
        //            GetPer(list, start + 1, end, resultArr);
        //            Swap(list[start], list[i]);
        //        }
        //}

        //public static void JumpOneRight(int[] array, int currentIndex)
        //{
        //    Jump(array, currentIndex, 1);
        //}

        //public static void JumpTwoRight(int[] array, int currentIndex)
        //{
        //    Jump(array, currentIndex, 2);
        //}

        //public static void JumpOneLeft(int[] array, int currentIndex)
        //{
        //    Jump(array, currentIndex, -1);
        //}

        //public static void JumpTwoLeft(int[] array, int currentIndex)
        //{
        //    Jump(array, currentIndex, -2);
        //}

        //public static bool IsJumpAllowed(int[] array, int currentIndex, int stepOneIndex, int stepTwoIndex)
        //{
        //    int currentValue = array[currentIndex],
        //        stepOneValue = array[stepOneIndex],
        //        stepTwoValue = array[stepTwoIndex];

        //    if (stepOneValue == 0 || stepTwoValue == 0)
        //        return true;
            
        //    return false;
        //}

        //private static void Jump(int[] array, int currentIndex, int step)
        //{
        //    int currentValue = array[currentIndex];
        //    array[currentIndex] = array[currentIndex + step];
        //    array[currentIndex + step] = currentValue;
        //}

        private static void Swap(int[] list, int a, int b)
        {
            if (a == b) 
                return;

            int c;
            c = list[a];
            list[a] = list[b];
            list[b] = c;
        }

        public static bool HasMove(int[] list, int[] valuesArray)
        {
            //int freePlaceIndex = FrogJumperUtil.FindIndexOfFreePlace(list, Program.FreePlaceSymbol);
            for (int i = 1; i < valuesArray.Length - 2; i++)
            {
                if (valuesArray[i] == Program.LeftFrogSymbol && valuesArray[i] == valuesArray[i + 1] && valuesArray[i + 1] != Program.FreePlaceSymbol       // 1 1 2
                    || valuesArray[i] == Program.rightFrogSymbol && valuesArray[i] == valuesArray[i + 1] && valuesArray[i - 1] != Program.FreePlaceSymbol)  // 1 2 2
                {
                    return false;
                }
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
