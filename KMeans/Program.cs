using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = InputDataParser.ParseInputData("kMeansData/normal/normal.txt");
            
            int k = 3;
            Point[] centroids = new Point[k];
            KMeansUtility.InitialializeRandomCentroids(centroids);
            int iteration = 1;
            do
            {
                Console.WriteLine(iteration++);
               KMeansUtility.MatchPointsAndCentroids(data, centroids);
               var newCentroids = KMeansUtility.RecalculateCentroids(data, centroids);
               bool shouldBreak = false;

               for (int i = 0; i < centroids.Length; i++)
               {
                    if(centroids[i].XCoordinate == newCentroids[i].XCoordinate && centroids[i].YCoordinate == newCentroids[i].YCoordinate){
                        shouldBreak = true;
                        break;
                    }
               }

               centroids = newCentroids;

               if (shouldBreak)
               {
                    for (int i = 0; i < centroids.Length; i++)
                    {
                        Console.WriteLine("i=" + i + " " + data.Where(x=>x.BelongingCentroidIndex == i).ToArray().Length);
                    }
                    Console.WriteLine();
                   break;
               }
            } while (true);

            PointsArrayToImageWriter.WritePointsToImage(data, centroids);
            Console.ReadLine();
        }

        
    }
}
