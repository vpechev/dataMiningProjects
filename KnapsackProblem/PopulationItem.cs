using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    public class PopulationItem
    {
        public PopulationItem()
        {
            TotalCi = 0;
            TotalMi = 0;
            WeightCombinations = new List<double>();
        }

        public double TotalCi{ get; set; }
        public double TotalMi { get; set; }
        public List<double> WeightCombinations { get; set; }
    }
}
