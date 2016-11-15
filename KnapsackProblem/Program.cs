using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            int M = 10000; //kilos
            string input = System.IO.File.ReadAllText(@"KP_test_data.txt");

            string input2 = @"5 3
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
            var inputChromosomes = InputDataTransformer.TransformInputData(input);
            List<Cell> populаtion = GenerateInitialPopulation(inputChromosomes, M);
            int isBestSameCount = 0;

            for (int i = 1; i <= Constants.ITERATIONS_COUNT; i++)
            {
                if (populаtion.Count > 1) { 
                    var isBestSame = gaUtil.CreateNewGeneration(ref populаtion, inputChromosomes);
                    if(isBestSame)
                        isBestSameCount++;
                    else 
                        isBestSameCount = 0;

                    //if ( i % 10 == 0 ){
                        PrintBest(populаtion, i);
                    //}

                    if (isBestSameCount == Constants.MAX_EQUAL_BEST_LIMIT)
                        break;
                }
                else
                {
                    PrintBest(populаtion, i);
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

        private static void PrintBest(List<Cell> population, int step)
        {
            var best = population[0];
            Console.WriteLine("Evolution step: {0}", step);
            Console.WriteLine("\tTotal weight: {0}, value: {1}$", best.TotalMi, best.TotalCi);
            Console.WriteLine("\tDetails: ");
            Console.WriteLine(string.Join(" ", best.Chromosomes));
        }
    }
}
