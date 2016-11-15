using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    public class Cell : IComparable<Cell>
    {
        public Cell()
        {
            TotalCi = 0;
            TotalMi = 0;
            Chromosomes = new List<Chromosome>();
        }

        public double TotalCi{ get; set; }
        public double TotalMi { get; set; }
        public List<Chromosome> Chromosomes { get; set; }

        public int CompareTo(Cell other)
        {
            //return this.TotalCi.CompareTo(other.TotalCi);
            var totalCiComparedValue = other.TotalCi.CompareTo(this.TotalCi);

            if (totalCiComparedValue == 0)
                return this.TotalMi.CompareTo(other.TotalMi);
            
            return totalCiComparedValue;
        }

        public override bool Equals(object obj)
        {
            Cell other = (Cell)obj;
            return Array.Equals(this.Chromosomes, other.Chromosomes);
        }

        public override string ToString()
        {
            return "\t\tCell -> Weight: " + TotalMi + " Value: " + TotalCi + "$\n";
        } 
    }   
}
