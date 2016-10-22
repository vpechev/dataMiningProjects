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
        public Node ExitNode { get; set; }

        public bool ExitFound { get; set; }
        
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

        public void GenerateGraph(Node current, List<Node> children)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();

            if (children.Count == 0 || ExitFound)
                return;

            foreach (var child in children)
            {
                if (ExitFound)
                {
                    for (int i = 0; i < child.Combination.Length; i++)
                    {
                        Console.Write("{0} ", ValuesArray[child.Combination[i]]);
                    }
                    Console.WriteLine();
                    return;
                }

                if (IsExit(child.Combination))
                {
                    child.IsExit = true;
                    ExitNode = child;
                    ExitFound = true;
                    Console.WriteLine("EXIT: ");
                    return;
                }
                else
                {
                    List<Node> preChildren = GenerateChildren(child);
                    if (preChildren != null && preChildren.Count > 0)
                        GenerateGraph(child, preChildren);
                }
            }
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("Inside GenerateGraph: {0} ms", elapsedMs);
        }

        public List<Node> GenerateChildren(Node current)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();

            if (ExitFound)
                return null;

            List<Node> children = new List<Node>();
            int[] arr = new int[current.Combination.Length];
            Array.Copy(current.Combination, arr, current.Combination.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                if (this.ValuesArray[arr[i]] == Program.LeftFrogSymbol)
                {
                    JumpRight(current, children, arr, i);
                }
                else if (this.ValuesArray[arr[i]] == Program.RightFrogSymbol)
                {
                    JumpLeft(current, children, arr, i);
                }
            }
            return children;
            
            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("Generate children: {0} ms", elapsedMs);
        }

        private void JumpRight(Node current, List<Node> children, int[] arr, int i)
        {
            if (i < arr.Length - 1 && IsJumpAllowed(arr, i, i + 1))
            {
                JumpOneRight(arr, i);
                var newNode = new Node(current, arr);
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
                var newNode = new Node(current, arr);
                JumpTwoLeft(arr, i + 2);
                if (HasMove(newNode.Combination))
                {
                    children.Add(newNode);
                    //Console.WriteLine(string.Join(" ", newNode.Combination));
                }
            }
        }

        private void JumpLeft(Node current, List<Node> children, int[] arr, int i)
        {
            if (i >= 1 && IsJumpAllowed(arr, i, i - 1))
            {
                JumpOneLeft(arr, i);
                var newNode = new Node(current, arr);
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
                var newNode = new Node(current, arr);
                JumpTwoRight(arr, i - 2);
                if (HasMove(newNode.Combination))
                {
                    children.Add(newNode);
                    //Console.WriteLine(string.Join(" ", newNode.Combination));
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
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            //var elapsedMs = -1L;
            int freeSymbolIndex = ArrayUtil.FindIndexOfFreePlace(list, Program.FreePlaceSymbol);

            if (this.ValuesArray[list[0]] == freeSymbolIndex && this.ValuesArray[list[1]] == Program.LeftFrogSymbol
                || this.ValuesArray[list[list.Length-1]] == freeSymbolIndex && this.ValuesArray[list[list.Length - 1]] == Program.RightFrogSymbol)
            {
                //watch.Stop();
                //elapsedMs = watch.ElapsedMilliseconds;
                //Console.WriteLine("Has Move: {0} ms", elapsedMs);
                return false;
            }

            // .... 0 1 1 2
            for (int i = freeSymbolIndex + 1; i < list.Length-1; i++)
            {
                if (this.ValuesArray[list[i]] == Program.LeftFrogSymbol && this.ValuesArray[list[i+1]] == Program.LeftFrogSymbol)
                {
                    for (int j = i+1; j < list.Length; j++)
                    {
                        if (this.ValuesArray[list[i]] == Program.RightFrogSymbol)
                        {
                            //watch.Stop();
                            //elapsedMs = watch.ElapsedMilliseconds;
                            //Console.WriteLine("Has Move: {0} ms", elapsedMs);
                            return false;
                        }
                    }
                }
            }

            // .. 1 2 2 ....
            for (int i = 0; i < freeSymbolIndex; i++)
            {
                if (this.ValuesArray[list[i]] == Program.RightFrogSymbol && this.ValuesArray[list[i + 1]] == Program.RightFrogSymbol)
                {
                    for (int j = i + 1; j < freeSymbolIndex; j++)
                    {
                        if (this.ValuesArray[list[i]] == Program.LeftFrogSymbol)
                        {
                            //watch.Stop();
                            //elapsedMs = watch.ElapsedMilliseconds;
                            //Console.WriteLine("Has Move: {0} ms", elapsedMs);
                            return false;
                        }
                    }
                }
            }
            
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("Has Move: {0} ms", elapsedMs);
            return true;
        }

        public bool IsExit(int[] list)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            //var elapsedMs = -1L;
            if (ValuesArray[list[list.Length / 2]] != 0)
            {
                //watch.Stop();
                //elapsedMs = watch.ElapsedMilliseconds;
                //Console.WriteLine("IsExit: {0} ms", elapsedMs);
                return false;
            }

            for (int i = 0; i < list.Length/2; i++)
            {
                if (ValuesArray[list[i]] != Program.RightFrogSymbol || ValuesArray[list[list.Length / 2 + i + 1]] != Program.LeftFrogSymbol) {
                    //watch.Stop();
                    //elapsedMs = watch.ElapsedMilliseconds;
                    //Console.WriteLine("IsExit: {0} ms", elapsedMs);
                    return false;
                }
            }
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("IsExit: {0} ms", elapsedMs);
            return true;
        }
    }
}
