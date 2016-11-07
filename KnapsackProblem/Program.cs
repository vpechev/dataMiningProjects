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
            int M = 10; //kilos
            int N = 3; //items
            int populationCapacity = 10;
            string input = @"5 3
2 3
5 1
3 2";
            List<Item> inputItems = new List<Item>();
            string[] inputs = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var currentInput in inputs)
            {
                string[] parts = currentInput.Split(' ');
                inputItems.Add(new Item()
                {
                    Mi = Double.Parse(parts[0]),
                    Ci = Double.Parse(parts[1])
                        });
            }

            Knapsack knapsack = new Knapsack();
            knapsack.Populаtion = GeneratePopulation(inputItems, M, N);

            Console.ReadLine();
        }

        private static List<PopulationItem> GeneratePopulation(List<Item> inputItems, int maxMi, int maxN)
        {
            List<PopulationItem> populationItems = new List<PopulationItem>();
            PopulationItem currentPopulationItem = new PopulationItem();

            for (int i = 0; i < inputItems.Count; i++)
            {
                if (currentPopulationItem.TotalMi + inputItems[i].Mi <= maxMi && currentPopulationItem.WeightCombinations.Count + 1 <= maxN)
                {
                    currentPopulationItem.TotalCi += inputItems[i].Ci;
                    currentPopulationItem.TotalMi += inputItems[i].Mi;
                    currentPopulationItem.WeightCombinations.Add(inputItems[i].Mi);
                }
                else
                {
                    populationItems.Add(currentPopulationItem);
                    currentPopulationItem = new PopulationItem();
                    i--;
                }
            }

            if (currentPopulationItem.TotalMi > 0) { 
                populationItems.Add(currentPopulationItem);
            }

            return populationItems;
        }

        private static void doFitness()
        {

        }
    }
}
