using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    public class Chromosome
    {
        public double Mi { get; set; }
        public double Ci { get; set; }

        public override string ToString() {
            return "\t\tChromosome -> Weight: " + Mi + " Value: " + Ci + "$\n";
        }  
    }
}
