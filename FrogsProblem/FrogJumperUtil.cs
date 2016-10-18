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
            current.Children = new List<Node>();
            Console.WriteLine(string.Join("{0}, ", current.Combination));
            GetPer(current, 0, current.Combination.Length - 1);

            if (current.Children.Count != 0)
            {
                GetPer(current);
            }
        }

        public static void GetPer(Node current)
        {
            foreach (var child in current.Children)
	        {
                child.Children = new List<Node>();
                if (HasMove(child.Combination))
                {
                    GetPer(child, 0, child.Combination.Length - 1);
                }
	        } 
        }

        private static void GetPer(Node current, int start, int end)
        {
            if (start == end)
            {
                var newChild = new Node()
                {
                    Combination = current.Combination,
                    Parent = current,
                    Children = new List<Node>()
                };

                if (HasMove(newChild.Combination) && !ArrayUtil.AreEqual(current.Combination, newChild.Combination))
                {
                    Console.WriteLine(string.Join("{0}, ", newChild.Combination));
                    current.Children.Add(newChild);
                }
            }
            else
            {
                for (int i = start; i <= end; i++)
                {
                    Swap(ref current.Combination[start], ref current.Combination[i]);
                    GetPer(current, start + 1, end);
                    Swap(ref current.Combination[start], ref current.Combination[i]);
                }
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

        private static void Swap(ref int a, ref int b)
        {
            if (a == b) 
                return;

            a ^= b;
            b ^= a;
            a ^= b;
        }

        public static bool HasMove(int[] list)
        {
            int freePlace = FrogJumperUtil.FindIndexOfFreePlace(list, Program.FreePlaceSymbol);
            for (int i = 1; i < list.Length-2; i++)
            {
                if (list[i] == Program.LeftFrogSymbol && list[i] == list[i + 1] && list[i + 1] != Program.FreePlaceSymbol       // 1 1 2
                    || list[i] == Program.rightFrogSymbol && list[i] == list[i + 1] && list[i - 1] != Program.FreePlaceSymbol)  // 1 2 2
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
