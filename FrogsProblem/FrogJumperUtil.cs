using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    class FrogJumperUtil
    {
        public static List<int[]> GetPer(int[] list)
        {
            int x = list.Length - 1;
            List<int[]> resultArr = new List<int[]>(); 
            GetPer(list, 0, x, resultArr);
            return resultArr;
        }

        private static void GetPer(int[] list, int start, int end, List<int[]> resultArr)
        {
            if (start == end)
            {
                resultArr.Add(list);
            }
            else
                for (int i = start; i <= end; i++)
                {
                    Swap(ref list[start], ref list[i]);
                    GetPer(list, start + 1, end, resultArr);
                    Swap(ref list[start], ref list[i]);
                }
        }

        public static void JumpOneRight(int[] array, int currentIndex)
        {
            Jump(array, currentIndex, 1);
        }

        public static void JumpTwoRight(int[] array, int currentIndex)
        {
            Jump(array, currentIndex, 2);
        }

        public static void JumpOneLeft(int[] array, int currentIndex)
        {
            Jump(array, currentIndex, -1);
        }

        public static void JumpTwoLeft(int[] array, int currentIndex)
        {
            Jump(array, currentIndex, -2);
        }

        public static bool IsJumpAllowed(int[] array, int currentIndex, int stepOneIndex, int stepTwoIndex)
        {
            int currentValue = array[currentIndex],
                stepOneValue = array[stepOneIndex],
                stepTwoValue = array[stepTwoIndex];

            if (stepOneValue == 0 || stepTwoValue == 0)
                return true;
            
            return false;
        }

        private static void Jump(int[] array, int currentIndex, int step)
        {
            int currentValue = array[currentIndex];
            array[currentIndex] = array[currentIndex + step];
            array[currentIndex + step] = currentValue;
        }

        private static void Swap(ref int a, ref int b)
        {
            if (a == b) 
                return;

            a ^= b;
            b ^= a;
            a ^= b;
        }
    }
}
