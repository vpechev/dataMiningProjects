using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    class Program
    {
        public const char FreePlaceSymbol = '0';
        public const char LeftFrogSymbol = '1';
        public const char RightFrogSymbol = '2';
        static void Main(string[] args)
        {
            Console.Write("Enter N: ");
            int n = Int32.Parse(Console.ReadLine());
            Console.WriteLine();
            string valuesArray = ArrayUtil.InitializeArray(n);
            System.Diagnostics.Stopwatch watch;
            long elapsedMs;

            watch = System.Diagnostics.Stopwatch.StartNew();
            var inputNode = new Node()
            {
                //Parent = null,
                Combination = valuesArray,
                IsExit = false
            };

            var frogJumperUtil = new FrogJumperUtil();

            List<Node> children = frogJumperUtil.GenerateChildren(inputNode);

            frogJumperUtil.GenerateGraph(inputNode, children);

            Console.WriteLine(valuesArray);

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Print result: {0} ms", elapsedMs);

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
