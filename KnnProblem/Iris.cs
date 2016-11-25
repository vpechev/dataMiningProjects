using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnnProblem
{
    public class Iris : IComparable<Iris>
    {
        public double SepalLength { get; set; }
        public double SepalWidth { get; set; }
        public double PetalLength { get; set; }
        public double PetalWidth { get; set; }
        public IrisType Type { get; set; }

        public double Distance { get; set; }

        public int CompareTo(Iris other)
        {
            return this.Distance.CompareTo(other.Distance);
        }
    }
}
