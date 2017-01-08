using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public static class KMeansUtility
    {
        public static void InitialializeRandomCentroids(Point[] centroids)
        {
            var rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < centroids.Length; i++)
            {
                centroids[i] = new Point()
                {
                    XCoordinate = rand.NextDouble(),
                    YCoordinate = rand.NextDouble()
                };
            }
        }

        public static Point[] RecalculateCentroids(List<Point> data, Point[] centroids)
        {
            Point[] newCentroidsArr = new Point[centroids.Length];

            for (int i = 0; i < centroids.Length; i++)
            {
                var matchingPoints = data.Where(x => x.BelongingCentroidIndex == i).ToList();
                double meanX = 0;
                double meanY = 0;
                foreach (var point in matchingPoints)
                {
                    meanX += point.XCoordinate;
                    meanY += point.YCoordinate;
                }

                meanX /= matchingPoints.Count();
                meanY /= matchingPoints.Count();

                newCentroidsArr[i] = new Point()
                {
                    XCoordinate = meanX,
                    YCoordinate = meanY
                };
            }

            return newCentroidsArr;
        }

        public static void MatchPointsAndCentroids(List<Point> data, Point[] centroids)
        {
            foreach (var currentPont in data)
            {
                currentPont.BelongingCentroidIndex = FindNearestCentroidIndex(currentPont, centroids);
            }
        }

        private static int FindNearestCentroidIndex(Point point, Point[] centroids)
        {
            int nearestCentroidIndex = 0;
            var nearestCentroidDistance = FindTwoPointsDistance(point, centroids[0]);
            for (int i = 1; i < centroids.Length; i++)
            {
                var currentDistance = FindTwoPointsDistance(point, centroids[i]);
                if (currentDistance < nearestCentroidDistance)
                {
                    nearestCentroidDistance = currentDistance;
                    nearestCentroidIndex = i;
                }
            }
            return nearestCentroidIndex;
        }

        private static double FindTwoPointsDistance(Point point1, Point point2)
        {
            var distance = Math.Sqrt(Math.Pow(point1.XCoordinate - point2.XCoordinate, 2)
                            + Math.Pow(point1.YCoordinate - point2.YCoordinate, 2));

            return distance;
        }
    }
}
