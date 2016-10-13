﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    class ArrayUtil
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
    }
}