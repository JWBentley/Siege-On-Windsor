using Pathfinding.Pathfinding;
using Pathfinding.Pathfinding.Graph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Testing
{
    class Program
    {
        static int interations = 1;
        static Random rand = new Random();

        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < interations; i++)
            {
                int[,] data = new int[17, 17];
                for (int x = 0; x < 17; x++)
                {
                    for (int y = 0; y < 17; y++)
                    {
                        data[x, y] = rand.Next(100);
                    }
                }

                GridGraph gridGraph = new GridGraph(data);


                AStar aStar = new AStar(gridGraph);


                Location start = new Location(rand.Next(17), rand.Next(17));
                Location goal = new Location(17 / 2, 17 / 2);

                aStar.Run(gridGraph.GetNodeFromLocation(start), gridGraph.GetNodeFromLocation(goal));
                Stack<GraphNode<Location>> path = aStar.ReconstructPath(gridGraph.GetNodeFromLocation(start), gridGraph.GetNodeFromLocation(goal));
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}
