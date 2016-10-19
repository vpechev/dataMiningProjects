using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    public class FrogJumperUtil
    {
        private int[] ValuesArray;
        
        public FrogJumperUtil(int[] valuesArray)
        {
            if (valuesArray.Length > 0)
            {
                this.ValuesArray = valuesArray;
            }
            else
            {
                this.ValuesArray = new int[] { 0 };
            }
        }

        public void GenerateGraph(Node current)
        {
            if (current.Children.Count == 0)
                return;

            foreach (var child in current.Children)
            {
                if (IsExit(child.Combination))
                {
                    Console.WriteLine("EXIT: " + current.Children.IndexOf(child));
                    return;
                }
                else
                {
                    GenerateChildren(child);
                    GenerateGraph(child);
                }
            }
        }

        public void GenerateChildren(Node current)
        {
            int[] arr = new int[current.Combination.Length];
            Array.Copy(current.Combination, arr, current.Combination.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                if (this.ValuesArray[arr[i]] == Program.LeftFrogSymbol)
                {
                    JumpRight(current, arr, i);
                }
                else if (this.ValuesArray[arr[i]] == Program.RightFrogSymbol)
                {
                    JumpLeft(current, arr, i);
                }
            }
        }

        private void JumpRight(Node current, int[] arr, int i)
        {
            if (i < arr.Length - 1 && IsJumpAllowed(arr, i, i + 1))
            {
                JumpOneRight(arr, i);
                var newNode = new Node(current, arr);
                JumpOneLeft(arr, i + 1);
                if (HasMove(newNode.Combination))
                {
                    current.Children.Add(newNode);
                    Console.WriteLine(string.Join(" ", newNode.Combination));
                }

            }

            if (i < arr.Length - 2 && IsJumpAllowed(arr, i, i + 2))
            {
                JumpTwoRight(arr, i);
                var newNode = new Node(current, arr);
                JumpTwoLeft(arr, i + 2);
                if (HasMove(newNode.Combination))
                {
                    current.Children.Add(newNode);
                    Console.WriteLine(string.Join(" ", newNode.Combination));
                }
            }
        }

        private void JumpLeft(Node current, int[] arr, int i)
        {
            if (i >= 1 && IsJumpAllowed(arr, i, i - 1))
            {
                JumpOneLeft(arr, i);
                var newNode = new Node(current, arr);
                JumpOneRight(arr, i - 1);
                if (HasMove(newNode.Combination))
                {
                    current.Children.Add(newNode);
                    Console.WriteLine(string.Join(" ", newNode.Combination));
                }

            }
            if (i >= 2 && IsJumpAllowed(arr, i, i - 2))
            {
                JumpTwoLeft(arr, i);
                var newNode = new Node(current, arr);
                JumpTwoRight(arr, i - 2);
                if (HasMove(newNode.Combination))
                {
                    current.Children.Add(newNode);
                    Console.WriteLine(string.Join(" ", newNode.Combination));
                }
            }
        }

        private void JumpOneRight(int[] array, int currentIndex)
        {
            Jump(array, currentIndex, 1);
        }

        private void JumpTwoRight(int[] array, int currentIndex)
        {
            Jump(array, currentIndex, 2);
        }

        private void JumpOneLeft(int[] array, int currentIndex)
        {
            Jump(array, currentIndex, -1);
        }

        private void JumpTwoLeft(int[] array, int currentIndex)
        {
            Jump(array, currentIndex, -2);
        }

        private void Jump(int[] array, int currentIndex, int step)
        {
            int currentValue = array[currentIndex];
            array[currentIndex] = array[currentIndex + step];
            array[currentIndex + step] = currentValue;
        }

        public bool IsJumpAllowed(int[] array, int currentIndex, int stepIndex)
        {
            int currentValue = array[currentIndex],
                stepValue = this.ValuesArray[array[stepIndex]];

            if (stepValue == 0)
                return true;

            return false;
        }

        public bool HasMove(int[] list)
        {
            for (int i = 1; i < list.Length - 2; i++)
            {
                if (this.ValuesArray[list[i-1]] == Program.LeftFrogSymbol && this.ValuesArray[list[i-1]] == this.ValuesArray[list[i]] && this.ValuesArray[list[i + 1]] != Program.FreePlaceSymbol && this.ValuesArray[list[i + 2]] != Program.FreePlaceSymbol // 1 1 2 2
                    || this.ValuesArray[list[i]] == Program.RightFrogSymbol && this.ValuesArray[list[i]] == this.ValuesArray[list[i + 1]] && this.ValuesArray[list[i - 1]] != Program.FreePlaceSymbol && this.ValuesArray[list[i + 2]] != Program.FreePlaceSymbol)  // 1 2 2 2
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsExit(int[] list)
        {
            if (ValuesArray[list[list.Length / 2]] != 0)
            {
                return false;
            }

            for (int i = 0; i < list.Length/2; i++)
            {
                if (ValuesArray[list[i]] != Program.RightFrogSymbol || ValuesArray[list[list.Length / 2 + i + 1]] != Program.LeftFrogSymbol) { 
                    return false;
                }
            }

            return true;
        }
    }
}
