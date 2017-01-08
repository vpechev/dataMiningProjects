using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public static class InputDataParser
    {
        public static List<Point> ParseInputData(string fileName)
        {
            var data = new List<Point>();
            var lines = File.ReadAllLines(fileName);

            foreach (var line in lines)
            {
                var parts = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                data.Add(new Point()
                {
                    XCoordinate = Double.Parse(parts[0]),
                    YCoordinate = Double.Parse(parts[1])
                });
            }

            return data;
        }
    }
}
