using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class Point
    {
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public int BelongingCentroidIndex { get; set; }
    }
}
