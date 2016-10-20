using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    class Program
    {
        public const int FreePlaceSymbol = 0;
        public const int LeftFrogSymbol = 1;
        public const int RightFrogSymbol = 2;
        static void Main(string[] args)
        {
            int n = 2;
            int [] valuesArray = ArrayUtil.InitializeArray(n);
            int[] indexesArray = new int[valuesArray.Length];
            System.Diagnostics.Stopwatch watch;
            long elapsedMs;

            for (int i = 0; i < indexesArray.Length; i++)
            {
                indexesArray[i] = i;
            }

            watch = System.Diagnostics.Stopwatch.StartNew();
            var inputNode = new Node()
            {
                Parent = null,
                Combination = indexesArray,
                Children = new List<Node>(),
                IsExit = false
            };

            var frogJumperUtil = new FrogJumperUtil(valuesArray);

            frogJumperUtil.GenerateChildren(inputNode);
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("Generate root with first level children: {0} ms", elapsedMs);

            //watch = System.Diagnostics.Stopwatch.StartNew();
            frogJumperUtil.GenerateGraph(inputNode);
            //watch.Stop();
            //elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine("Generate rest of the graph: {0} ms", elapsedMs);

            //watch = System.Diagnostics.Stopwatch.StartNew();
            Node exit = frogJumperUtil.ExitNode;
            List<int[]> list = new List<int[]>();
            list.Add(exit.Combination);
            var currentParent = exit.Parent;
            do
            {
                list.Add(currentParent.Combination);
                currentParent = currentParent.Parent;
            }
            while (currentParent != null);

            for (int i = list.Count -1 ; i >=0; i--)
            {
                int[] currentRow = list[i];
                for (int j = 0; j < currentRow.Length; j++)
                {
                    Console.Write("{0} ", valuesArray[currentRow[j]]);
                }
                Console.WriteLine();
            }
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Print result: {0} ms", elapsedMs);

            //Dfs(array.ToList(), array[0]); 
            Console.ReadLine();
        }

        private static void Dfs(List<int[]> vertices, int v)
        {
            Dfs(vertices, v, new HashSet<int>());
        }

        private static void Dfs(List<int[]> vertices, int v, HashSet<int> visited)
        {
            if (visited.Contains(v))
            {
                return;
            }

            Console.WriteLine(v + 1);
            visited.Add(v);

            foreach (var item in vertices[v])
            {
                Dfs(vertices, item, visited);
            }
        }
    }
}
