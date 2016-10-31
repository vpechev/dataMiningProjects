using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingBlocks
{
    public class Program
    {
        public static int[,] EXIT_BOARD = {
                                {1, 2, 3},
                                {4, 5, 6},
                                {7, 8, 0}
                            };

        static void Main(string[] args)
        {
            //int inputNumber = 8;

            int[,] inputBoard2 = {
                                {6, 5, 3},
                                {2, 4, 8},
                                {7, 0, 1}
                            };

            int[,] inputBoard0 = {
                                {1, 2, 3},
                                {0, 7, 6},
                                {4, 7, 8}
                            };
            //int[,] inputBoard1 = {
            //                    {1, 2, 3},
            //                    {4, 5, 6},
            //                    {0, 7, 8}
            //                };


            //int[,] inputBoard3 = {
            //                    {1, 2, 3},
            //                    {4, 0, 5},
            //                    {6, 7, 8}
            //                };
            //int[,] inputBoard4 = {
            //                    {5, 4, 2},
            //                    {1, 0, 3},
            //                    {6, 7, 8}
            //                };

            var node = new Node()
            {
                Board = inputBoard2,
                Distance = 0,
                ChildrenBoards = new List<Node>()
            };

            SlidingBocksSolver.SolveSlidingBocks(node);

            Console.ReadLine();
        }
    }
}
