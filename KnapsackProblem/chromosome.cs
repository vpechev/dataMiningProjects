using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    public class Chromosome : IComparable<Chromosome>
    {
        public double Mi { get; set; }
        public double Ci { get; set; }

        public int CompareTo(Chromosome other)
        {
            //return this.TotalCi.CompareTo(other.TotalCi);
            int compareRes = other.Ci.CompareTo(this.Ci);
            if (compareRes == 0)
            {
                return this.Mi.CompareTo(other.Mi);
            }
            return other.Ci.CompareTo(this.Ci);
        }

        public override bool Equals(object obj)
        {
            var other = (Chromosome)obj;
            return this.Ci == other.Ci && this.Mi == other.Mi;
        }

        public override string ToString() {
            return "\t\tChromosome -> Weight: " + Mi + " Value: " + Ci + "$\n";
        }  
    }
}
