using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    public class FrogJumperUtil
    {
        public bool ExitFound { get; set; }
        
        public FrogJumperUtil() {}

        public void GenerateGraph(Node current, List<Node> children)
        {
            if (children.Count == 0 || ExitFound)
                return;

            foreach (var child in children)
            {
                if (IsExit(child.Combination))
                {
                    Console.WriteLine(child.Combination);
                    ExitFound = true;
                    Console.WriteLine("EXIT: ");
                    return;
                }
                else
                {
                    List<Node> preChildren = GenerateChildren(child);
                    if (preChildren != null && preChildren.Count > 0)
                    {
                        GenerateGraph(child, preChildren);
                    }

                    if (ExitFound)
                    {
                        Console.WriteLine(child.Combination);
                        return;
                    }
                }
            }
        }

        public List<Node> GenerateChildren(Node current)
        {
            if (ExitFound)
                return null;

            List<Node> children = new List<Node>();
            string combination = current.Combination;
            char[] arr = combination.ToCharArray();

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == Program.LeftFrogSymbol)
                {
                    JumpRight(current, children, arr, i);
                }
                else if (arr[i] == Program.RightFrogSymbol)
                {
                    JumpLeft(current, children, arr, i);
                }
            }
            return children;
        }

        private void JumpRight(Node current, List<Node> children, char[] arr, int i)
        {
            if (i < arr.Length - 1 && IsJumpAllowed(arr, i, i + 1))
            {
                JumpOneRight(arr, i);
                var newNode = new Node(arr);
                JumpOneLeft(arr, i + 1);
                if (HasMove(newNode.Combination))
                {
                    children.Add(newNode);
                    //Console.WriteLine(string.Join(" ", newNode.Combination));
                }

            }

            if (i < arr.Length - 2 && IsJumpAllowed(arr, i, i + 2))
            {
                JumpTwoRight(arr, i);
                var newNode = new Node(arr);
                JumpTwoLeft(arr, i + 2);
                if (HasMove(newNode.Combination))
                {
                    children.Add(newNode);
                    //Console.WriteLine(string.Join(" ", newNode.Combination));
                }
            }
        }

        private void JumpLeft(Node current, List<Node> children, char[] arr, int i)
        {
            if (i >= 1 && IsJumpAllowed(arr, i, i - 1))
            {
                JumpOneLeft(arr, i);
                var newNode = new Node(arr);
                JumpOneRight(arr, i - 1);
                if (HasMove(newNode.Combination))
                {
                    children.Add(newNode);
                    //Console.WriteLine(string.Join(" ", newNode.Combination));
                }

            }
            if (i >= 2 && IsJumpAllowed(arr, i, i - 2))
            {
                JumpTwoLeft(arr, i);
                var newNode = new Node(arr);
                JumpTwoRight(arr, i - 2);
                if (HasMove(newNode.Combination))
                {
                    children.Add(newNode);
                    //Console.WriteLine(string.Join(" ", newNode.Combination));
                }
            }
        }

        private void JumpOneRight(char[] array, int currentIndex)
        {
            Jump(array, currentIndex, 1);
        }

        private void JumpTwoRight(char[] array, int currentIndex)
        {
            Jump(array, currentIndex, 2);
        }

        private void JumpOneLeft(char[] array, int currentIndex)
        {
            Jump(array, currentIndex, -1);
        }

        private void JumpTwoLeft(char[] array, int currentIndex)
        {
            Jump(array, currentIndex, -2);
        }

        private void Jump(char[] array, int currentIndex, int step)
        {
            char currentValue = array[currentIndex];
            array[currentIndex] = array[currentIndex + step];
            array[currentIndex + step] = currentValue;
        }

        public bool IsJumpAllowed(char[] array, int currentIndex, int stepIndex)
        {
            char currentValue = array[currentIndex],
                stepValue = array[stepIndex];

            if (stepValue == Program.FreePlaceSymbol)
                return true;

            return false;
        }

        public bool HasMove(string list)
        {
            int freeSymbolIndex = ArrayUtil.FindIndexOfFreePlace(list, Program.FreePlaceSymbol);
            //Console.WriteLine(string.Join(" ", list));
            if (Program.FreePlaceSymbol == freeSymbolIndex && list[1] == Program.LeftFrogSymbol
                || list.Length-1 == freeSymbolIndex && list[list.Length - 1] == Program.RightFrogSymbol)
            {
                return false;
            }

            // .... 0 1 1 2
            for (int i = freeSymbolIndex + 1; i < list.Length-1; i++)
            {
                if (list[i] == Program.LeftFrogSymbol && list[i+1] == Program.LeftFrogSymbol)
                {
                    for (int j = i+1; j < list.Length; j++)
                    {
                        if (list[i] == Program.RightFrogSymbol)
                        {
                            return false;
                        }
                    }
                }
            }

            // .. 1 2 2 ....
            for (int i = 0; i < freeSymbolIndex; i++)
            {
                if (list[i] == Program.RightFrogSymbol && list[i + 1] == Program.RightFrogSymbol)
                {
                    for (int j = i + 1; j < freeSymbolIndex; j++)
                    {
                        if (list[i] == Program.LeftFrogSymbol)
                        {
                            return false;
                        }
                    }
                }
            }
            
            return true;
        }

        public bool IsExit(string list)
        {
            if (list[list.Length / 2] != Program.FreePlaceSymbol)
            {
                return false;
            }

            for (int i = 0; i < list.Length/2; i++)
            {
                if (list[i] != Program.RightFrogSymbol || list[list.Length / 2 + i + 1] != Program.LeftFrogSymbol) {
                    return false;
                }
            }
            return true;
        }
    }
}
