using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsProblem
{
    class Program
    {
        static void Main(string[] args)
        {
//            string input = @"5 4
//3 1
//3 4
//5 3
//1 2
//5 2
//3 6";
            //Dfs(vertices, 0);
            
            int [] array = ArrayUtil.InitializeArray(2);
            int[] indexesArray = new int[array.Length];
            for (int i = 0; i < indexesArray.Length; i++)
            {
                indexesArray[i] = i;
            }
            FrogJumperUtil.GetPer(indexesArray);
            //Dfs(array.ToList(), array[0]); 
            Console.ReadLine();
        }

        private static void Dfs(List<int>[] vertices, int v) {
            Dfs(vertices, v, new HashSet<int>());
        }

        private static void Dfs(List<int>[] vertices, int v, HashSet<int> visited)
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

        private static List<int>[] buildgraphGraph(string input, int count)
        {
            int n = count;
            //int m = 7;

            //build graph
            var edges = input.Split('\n');
            List<int>[] vertices = new List<int>[n];
            foreach (var edgeString in edges)
            {
                var parts = edgeString.Split(' ');
                var v1 = int.Parse(parts[0]) - 1;
                var v2 = int.Parse(parts[1]) - 1;

                if (vertices[v1] == null)
                    vertices[v1] = new List<int>();

                if (vertices[v2] == null)
                    vertices[v2] = new List<int>();

                vertices[v1].Add(v2);
                vertices[v2].Add(v1);

            }
            return vertices;
        }
        
    }
}
