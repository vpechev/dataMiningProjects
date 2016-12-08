using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    public static class Constants
    {
        public const int POPULATION_CAPACITY = 50;
        public const int DISCARD_CELLS_COUNT = 40 * POPULATION_CAPACITY / 100;
        public const int NEW_CHILDREN_COUNT = 20 * POPULATION_CAPACITY / 100;
        public const int ITERATIONS_COUNT = 1000;
        public const int MUTATIONS_PROBABILITY_COEF = 30;
        public const int MAX_EQUAL_BEST_LIMIT = 25;
    }
}
