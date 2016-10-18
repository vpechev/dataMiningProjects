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
        public const int rightFrogSymbol = 2;
        static void Main(string[] args)
        {          
            int [] valuesArray = ArrayUtil.InitializeArray(2);
            int[] indexesArray = new int[valuesArray.Length];
            for (int i = 0; i < indexesArray.Length; i++)
            {
                indexesArray[i] = i;
            }

            var inputNode = new Node()
            {
                Parent = null,
                Combination = indexesArray,
            };
            FrogJumperUtil.Permute(inputNode, valuesArray);

            FrogJumperUtil.BuildGraph(inputNode);
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

        ////private static List<int>[] buildgraphGraph(List<int[]> input, int count, Node parent)
        //private static List<int>[] buildgraphGraph(Node current)
        //{
        //    FrogJumperUtil.GetPer(current);


        //    //build graph
        //    var edges = input.Split('\n');
        //    List<int>[] vertices = new List<int>[n];
        //    foreach (var edgeString in edges)
        //    {
        //        var parts = edgeString.Split(' ');
        //        var v1 = int.Parse(parts[0]) - 1;
        //        var v2 = int.Parse(parts[1]) - 1;

        //        if (vertices[v1] == null)
        //            vertices[v1] = new List<int>();

        //        if (vertices[v2] == null)
        //            vertices[v2] = new List<int>();

        //        vertices[v1].Add(v2);
        //        vertices[v2].Add(v1);

        //    }
        //    return vertices;
        //}
        
    }
}
