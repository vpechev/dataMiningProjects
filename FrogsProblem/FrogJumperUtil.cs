using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    class FrogJumperUtil
    {
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

        private static void Jump(int[] array, int currentIndex, int step)
        {
            int currentValue = array[currentIndex];
            array[currentIndex] = array[currentIndex + step];
            array[currentIndex + step] = currentValue;
        }
    }
}
