using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnnProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"iris.data.txt");
            var irisArray = InputDataTransformer.ParseInput(input);
            var rand = new Random(System.DateTime.Now.Millisecond);

            var testingIrisArray = new List<Iris>(Constants.TESTING_IRIS_COUNT);

            //separate testing irises
            for (int i = 0; i < Constants.TESTING_IRIS_COUNT; i++)
            {
                var elIndex = rand.Next(0, irisArray.Count);
                testingIrisArray.Add(irisArray[elIndex]);
                irisArray.RemoveAt(elIndex);
            }

            Console.WriteLine("Enter k: ");
            int k = Int32.Parse(Console.ReadLine());
            int machedCount = 0;
            foreach (var testItem in testingIrisArray)
            {
                if (PrintParticularBest(irisArray, testItem, k))
                    machedCount++;
            }
            Console.WriteLine("Accurancy: {0}%", (double)machedCount / (double)testingIrisArray.Count * 100);
            Console.ReadLine();
        }

        public static bool PrintParticularBest(List<Iris> irisArray, Iris testItem, int k)
        {
            Console.WriteLine("REAL iris type: {0} ", testItem.Type.ToString());
            
            var sortedSet = new SortedSet<Iris>();

            foreach (var item in irisArray)
            {
                item.Distance = ComputeEquideanDistanceTwoIris(item, testItem);
                sortedSet.Add(item);
            }

            var irisIntersect = sortedSet.Take(k).ToList();
            string suggestedType = FindIrisType(irisIntersect);
            
            Console.WriteLine("SUGGESTED iris type: {0} ", suggestedType);
            Console.WriteLine(Constants.CONSOLE_WRITER_SEPARATOR);

            if (testItem.Type.ToString().Equals(suggestedType))
                return true;
            return false;
        }

        public static string FindIrisType(List<Iris> irisIntersect)
        {
            string suggestedType = null;
            
            var setosaTypeArr = irisIntersect.Where(x => x.Type == IrisType.Setosa).ToList();
            var versicolourTypeArr = irisIntersect.Where(x => x.Type == IrisType.Versicolour).ToList();
            var virginicaTypeArr = irisIntersect.Where(x => x.Type == IrisType.Virginica).ToList();

            if (versicolourTypeArr.Count >= Math.Max(setosaTypeArr.Count, virginicaTypeArr.Count))
                suggestedType = IrisType.Versicolour.ToString();
            else if (virginicaTypeArr.Count >= Math.Max(versicolourTypeArr.Count, setosaTypeArr.Count))
                suggestedType = IrisType.Virginica.ToString();
            else if (setosaTypeArr.Count >= Math.Max(versicolourTypeArr.Count, virginicaTypeArr.Count))
                suggestedType = IrisType.Setosa.ToString();
            else
                suggestedType = "ERROR";

            return suggestedType;
        }

        public static double ComputeEquideanDistanceTwoIris(Iris first, Iris second)
        {
            double distance = 0;

            distance = Math.Sqrt(
                            Math.Pow(first.PetalLength - second.PetalLength, 2)
                          + Math.Pow(first.PetalWidth - second.PetalWidth, 2)
                          + Math.Pow(first.SepalLength - second.SepalLength, 2)
                          + Math.Pow(first.SepalWidth - second.SepalWidth, 2)
                                ); 

            return distance;
        }
    }
}
