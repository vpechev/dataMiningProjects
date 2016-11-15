using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    public class GenericAlgorithmUtil
    {
        private static Random random = new Random(DateTime.Now.Second);
        private static int KnapsackMaxWeight {get; set;}

        public GenericAlgorithmUtil(int maxWeight)
        {
            KnapsackMaxWeight = maxWeight;
        }

        public bool CreateNewGeneration(ref List<Cell> population, List<Chromosome> inputChromosomes)
        {
            var isBestSame = Select(ref population);

            Crossover(ref population, 0, 1);
            for (int i = 2; i <= Constants.NEW_CHILDREN_COUNT; i++)
            {
                Crossover(ref population, 0, i);
                Crossover(ref population, 1, i);
            }

            Mutate(ref population, inputChromosomes);

            return isBestSame;
        }

        private bool Select(ref List<Cell> population)
        {
            var oldBest = population[0];
            DoFitness(ref population);
            var newBest = population[0];

            population = population.Take(Constants.POPULATION_CAPACITY).ToList();

            return oldBest == newBest;
        }

        private void Crossover(ref List<Cell> population, int firstCellIndex, int secondCellIndex)
        {
            var firstParentChromosomes = population[firstCellIndex].Chromosomes;
            var secondParentChromosomes = population[secondCellIndex].Chromosomes;
            var randomIndex = random.Next(1, Math.Min(firstParentChromosomes.Count(), secondParentChromosomes.Count()));

            var firstChild = CreateChild(population[firstCellIndex], population[secondCellIndex], randomIndex);
            var secondChild = CreateChild(population[secondCellIndex], population[firstCellIndex], randomIndex);

            //Console.WriteLine("FIRST PARENT " + population[firstCellIndex] + "Chromosomes count: " + population[firstCellIndex].Chromosomes.Count);
            //Console.WriteLine("SECOND PARENT " + population[secondCellIndex] + "Chromosomes count: " + population[firstCellIndex].Chromosomes.Count);
            //Console.WriteLine("FIRST CHILD " + firstChild + "Chromosomes count: " + population[firstCellIndex].Chromosomes.Count);
            //Console.WriteLine("SECOND CHILD " + firstChild + "Chromosomes count: " + population[firstCellIndex].Chromosomes.Count);
            //Console.WriteLine();
            //Console.WriteLine();
            //Thread.Sleep(2500);

            if (firstChild.TotalCi > 0 && firstChild.TotalMi <= KnapsackMaxWeight)
                population.Add(firstChild);

            if (secondChild.TotalCi > 0 && secondChild.TotalMi <= KnapsackMaxWeight)
                population.Add(secondChild);
        }

        private void Mutate(ref List<Cell> population, List<Chromosome> chromosomes)
        {
            Random random = new Random();
            int randInt = random.Next(0, 100);

            if (randInt < Constants.MUTATIONS_PROBABILITY_COEF)
            {
                population.Sort();
                Console.WriteLine("Mutation occured...");
                var mutatedChromosomesCount = random.Next(1, population.Count / 2);
                for (int i = 0; i < mutatedChromosomesCount; i++)
                {
                    var currentChromosomesConfiguration = population[population.Count - 1 - i].Chromosomes;
                    Chromosome newChromosome;
                    do {
                        newChromosome = chromosomes[random.Next(chromosomes.Count)];
                    } while(currentChromosomesConfiguration.Contains(newChromosome));
                    currentChromosomesConfiguration[random.Next(currentChromosomesConfiguration.Count)] = newChromosome;
                }
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
            var modifiedSecondChromosomesOrder = secondParentChromosomes.GetRange(randomIndex, secondParentChromosomes.Count - randomIndex); //startIndex - count
            modifiedSecondChromosomesOrder.Sort();
            for (int i = 0; i < modifiedSecondChromosomesOrder.Count; i++)
            {
                AddChromosomeToCell(childCell, modifiedSecondChromosomesOrder[i]);
            }


            //add additional chromosomes
            //if (childCell.TotalMi < KnapsackMaxWeight)
            //{
            //    var modifiedAdditionalChromosomesOrder = firstParentChromosomes.GetRange(randomIndex, firstParentChromosomes.Count - randomIndex);
            //    modifiedAdditionalChromosomesOrder.Sort();
            //    for (int i = randomIndex; i < modifiedAdditionalChromosomesOrder.Count(); i++)
            //    {
            //        AddChromosomeToCell(childCell, modifiedAdditionalChromosomesOrder[i]);

            //        if (childCell.TotalMi == KnapsackMaxWeight)
            //            return childCell;
            //    }
            //}

            //if (childCell.TotalMi < KnapsackMaxWeight)
            //{
            //    var modifiedAdditionalChromosomesOrder = secondParentChromosomes.GetRange(randomIndex, secondParentChromosomes.Count - randomIndex);
            //    modifiedAdditionalChromosomesOrder.Sort();
            //    for (int i = 0; i < modifiedAdditionalChromosomesOrder.Count; i++)
            //    {
            //        AddChromosomeToCell(childCell, modifiedAdditionalChromosomesOrder[i]);

            //        if (childCell.TotalMi == KnapsackMaxWeight)
            //            return childCell;
            //    }
            //}

            return childCell;
        }

        private void DoFitness(ref List<Cell> population)
        {
            population.Sort();
        }

        private void AddChromosomeToCell(Cell cell, Chromosome chromosome)
        {
            if (cell.TotalMi + chromosome.Mi <= KnapsackMaxWeight && !cell.Chromosomes.Contains(chromosome))
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
