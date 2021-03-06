﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.Pathfinding.Graph
{
    public class GridGraph : Graph<Location>
    {
        GraphNode<Location>[,] gridNodes;

        public GridGraph(int[,] data) : this(data, null){ }

        public GridGraph(int[,] data, NodeList<Location> nodeSet) : base(nodeSet)
        {
            this.gridNodes = new GraphNode<Location>[data.GetLength(0), data.GetLength(1)];

            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    GraphNode<Location> node = new GraphNode<Location>(new Location(x, y));
                    this.AddNode(node);
                    this.gridNodes[x, y] = node;
                }
            }

            for (int x = 0; x < gridNodes.GetLength(0); x++)
            {
                for (int y = 0; y < gridNodes.GetLength(1); y++)
                {
                    List<Location> adj = new List<Location>();

                    if (x + 1 < gridNodes.GetLength(0))
                        this.AddDirectedEdge(gridNodes[x, y], gridNodes[x + 1, y], data[x + 1, y]);
                    if (x - 1 >= 0)
                        this.AddDirectedEdge(gridNodes[x, y], gridNodes[x - 1, y], data[x - 1, y]);
                    if (y + 1 < gridNodes.GetLength(1))
                        this.AddDirectedEdge(gridNodes[x, y], gridNodes[x, y + 1], data[x, y + 1]);
                    if (y - 1 >= 0)
                        this.AddDirectedEdge(gridNodes[x, y], gridNodes[x, y - 1], data[x, y - 1]);
                }
            }

            /*
            GraphNode<Location> testNode = this.GetNodeFromLocation(new Location(4, 3));
            GraphNode<Location> testNode2 = this.GetNodeFromLocation(new Loscation(4, 4));
            int cost = GetCostOf(testNode, testNode2);
            Console.WriteLine(cost.ToString());
            */
        }

        public int GetCostOf(GraphNode<Location> from, GraphNode<Location> to)
        {
            return from.Costs.ToArray<int>()[from.Neighbors.IndexOf(to)];
        }

        public GraphNode<Location> GetNodeFromLocation(Location loc)
        {
            return (GraphNode<Location>)this.Nodes.FindByValue(loc);
        }
    }
}
