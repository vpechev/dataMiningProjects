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
            for (int i = 0; i < indexesArray.Length; i++)
            {
                indexesArray[i] = i;
            }

            var inputNode = new Node()
            {
                Parent = null,
                Combination = indexesArray,
                Children = new List<Node>()
            };

            var frogJumperUtil = new FrogJumperUtil(valuesArray);

            frogJumperUtil.GenerateChildren(inputNode);
            frogJumperUtil.GenerateGraph(inputNode);
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
