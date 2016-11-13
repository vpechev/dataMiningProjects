using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    class Program
    {
        public const int ITERATIONS_COUNT = 20;
        private static Random random = new Random();
        static void Main(string[] args)
        {
            int M = 100; //kilos
            string input2 = System.IO.File.ReadAllText(@"KP_test_data.txt");

            string input = @"5 3
5 1
7 5
4 3
2 3
2 1
3 2
1 2
4 6
2 800
1 1
3 2
1 2
3 3
2 1000
2 1
3 2
5 100
1 100
1 2
5 1
7 5
4 3
2 3
2 1
3 2
1 2
4 6
3 50
1 1
3 2
1 2
3 3
2 6
2 1
3 2
2 2500
1 500
1 2";

            GenericAlgorithmUtil gaUtil = new GenericAlgorithmUtil(M);
            Knapsack knapsack = new Knapsack();

            knapsack.Populаtion = GenerateInitialPopulation(InputDataTransformer.TransformInputData(input), M);

            for (int i = 1; i <= ITERATIONS_COUNT; i++)
            {
                if (knapsack.Populаtion.Count > 1) { 
                    gaUtil.CreateNewGeneration(knapsack.Populаtion);
                    if ( i % 10 == 0 ){
                        PrintBest(knapsack, i);
                    }
                }
                else
                {
                    PrintBest(knapsack, i);
                    break;
                }
            }

            Console.ReadLine();
        }

        private static List<Cell> GenerateInitialPopulation(List<Chromosome> inputItems, int maxMi)
        {
            List<Cell> populationItems = new List<Cell>();
            Cell currentCell = new Cell();

            for (int i = 0; i < inputItems.Count; i++)
            {
                if (inputItems[i].Mi < maxMi)
                {
                    if (currentCell.TotalMi + inputItems[i].Mi > maxMi)
                    {
                        populationItems.Add(currentCell);
                        currentCell = new Cell();
                    }

                    if (currentCell.TotalMi + inputItems[i].Mi <= maxMi)
                    {
                        currentCell.TotalCi += inputItems[i].Ci;
                        currentCell.TotalMi += inputItems[i].Mi;
                        currentCell.Chromosomes.Add(inputItems[i]);
                    }
                }
            }

            if (currentCell.TotalMi > 0 && currentCell.TotalMi < maxMi) { 
                populationItems.Add(currentCell);
            }

            return populationItems;
        }

        private static void PrintBest(Knapsack knapsack, int step)
        {
            var best = knapsack.Populаtion[0];
            Console.WriteLine("Evolution step: {0}", step);
            Console.WriteLine("\tTotal weight: {0}, value: {1}$", best.TotalMi, best.TotalCi);
            Console.WriteLine("\tDetails: ");
            Console.WriteLine(string.Join(" ", best.Chromosomes));
        }
    }
}
