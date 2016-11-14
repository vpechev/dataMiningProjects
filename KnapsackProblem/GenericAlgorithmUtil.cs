using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    public class GenericAlgorithmUtil
    {
        public const int POPULATION_CAPACITY = 10;
        public const int DISCARD_CELLS_COUNT = 40 * POPULATION_CAPACITY / 100;
        private static Random random = new Random();
        private static int KnapsackMaxWeight {get; set;}

        public GenericAlgorithmUtil(int maxWeight)
        {
            KnapsackMaxWeight = maxWeight;
        }

        public void CreateNewGeneration(List<Cell> population)
        {
            Select(population);
            Crossover(population, 0, 1);
            //Crossover(population, 0, 2);
            //Crossover(population, 1, 2);
            Mutate(population);
        }

        private void Select(List<Cell> population)
        {
            DoFitness(population);
            population.Take(DISCARD_CELLS_COUNT);
        }

        private void Crossover(List<Cell> population, int firstCellIndex, int secondCellIndex)
        {
            var firstParentChromosomes = population[firstCellIndex].Chromosomes;
            var secondParentChromosomes = population[secondCellIndex].Chromosomes;
            var randomIndex = random.Next(1, Math.Min(firstParentChromosomes.Count(), secondParentChromosomes.Count()));

            var firstChild = CreateChild(population[firstCellIndex], population[secondCellIndex], randomIndex);
            var secondChild = CreateChild(population[secondCellIndex], population[firstCellIndex], randomIndex);

            if (firstChild.TotalMi < KnapsackMaxWeight)
                population.Add(firstChild);

            if (secondChild.TotalMi < KnapsackMaxWeight)
                population.Add(secondChild);
        }

        private void Mutate(List<Cell> population)
        {
            Random random = new Random();
            int randInt = random.Next(0, 100);

            if (randInt < 20)
            {
                population.Sort();
                Console.WriteLine("Mutation occured...");
                var lastItem = population.Last();
                var chromosomes = lastItem.Chromosomes;
                var chromosomesLen = chromosomes.Count;
                SwapChromosomes(chromosomes, chromosomes.Count / 2 - 1 , chromosomes.Count / 2);
            }
        }

        private Cell CreateChild(Cell firstParent, Cell secondParent, int randomIndex)
        {
            var firstParentChromosomes = firstParent.Chromosomes;
            var secondParentChromosomes = secondParent.Chromosomes;

            var childChromosomes = new List<Chromosome>();
            
            Cell childCell = new Cell()
            {
                Chromosomes = childChromosomes,
                TotalCi = 0,
                TotalMi = 0,

            };

            // take first half from the first parent
            for (int i = 0; i < randomIndex; i++)
            {
                AddChromosomeToCell(childCell, firstParentChromosomes[i]);
            }

            // take second half from the second parent
            var modifiedSecondChromosomesOrder = secondParentChromosomes.GetRange(randomIndex, secondParentChromosomes.Count - randomIndex);
            modifiedSecondChromosomesOrder.Sort();
            for (int i = 0; i < modifiedSecondChromosomesOrder.Count; i++)
            {
                AddChromosomeToCell(childCell, modifiedSecondChromosomesOrder[i]);
            }


            //add additional chromosomes
            if (childCell.TotalMi < KnapsackMaxWeight)
            {
                var modifiedAdditionalChromosomesOrder = firstParentChromosomes.GetRange(randomIndex, firstParentChromosomes.Count - randomIndex);
                modifiedAdditionalChromosomesOrder.Sort();
                for (int i = randomIndex; i < modifiedAdditionalChromosomesOrder.Count(); i++)
                {
                    AddChromosomeToCell(childCell, modifiedAdditionalChromosomesOrder[i]);

                    if (childCell.TotalMi == KnapsackMaxWeight)
                        return childCell;
                }
            }

            if (childCell.TotalMi < KnapsackMaxWeight)
            {
                var modifiedAdditionalChromosomesOrder = secondParentChromosomes.GetRange(randomIndex, secondParentChromosomes.Count - randomIndex);
                modifiedAdditionalChromosomesOrder.Sort();
                for (int i = 0; i < modifiedAdditionalChromosomesOrder.Count; i++)
                {
                    AddChromosomeToCell(childCell, modifiedAdditionalChromosomesOrder[i]);

                    if (childCell.TotalMi == KnapsackMaxWeight)
                        return childCell;
                }
            }

            return childCell;
        }

        private void DoFitness(List<Cell> population)
        {
            population.Sort();
        }

        private void AddChromosomeToCell(Cell cell, Chromosome chromosome)
        {
            if (cell.TotalMi + chromosome.Mi <= KnapsackMaxWeight)
            {
                cell.Chromosomes.Add(chromosome);
                cell.TotalMi += chromosome.Mi;
                cell.TotalCi += chromosome.Ci;
            }
        }

        private void SwapChromosomes(List<Chromosome> chromosomes, int firstIndex, int lastIndex)
        {
            if (chromosomes.Count > 0)
            {
                var temp = chromosomes[firstIndex];
                chromosomes[firstIndex] = chromosomes[lastIndex];
                chromosomes[lastIndex] = temp;
            }
        }
    }
}
