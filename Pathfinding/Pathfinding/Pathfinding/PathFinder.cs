using Pathfinding.Pathfinding.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.Pathfinding
{
    public class PathFinder
    {
        public class AStar
        {
            GridGraph Graph;

            List<GraphNode<Location>> closedSet = new List<GraphNode<Location>>();
            List<GraphNode<Location>> priorityQueue = new List<GraphNode<Location>>();

            Dictionary<GraphNode<Location>, GraphNode<Location>> cameFrom = new Dictionary<GraphNode<Location>, GraphNode<Location>>();
            Dictionary<GraphNode<Location>, int> gCost = new Dictionary<GraphNode<Location>, int>();
            Dictionary<GraphNode<Location>, int> hCost = new Dictionary<GraphNode<Location>, int>();

            public AStar(GridGraph g)
            {
                this.Graph = g;
            }

            public void Run(GraphNode<Location> startNode, GraphNode<Location> goalNode)
            {
                this.cameFrom.Add(startNode, null);
                this.gCost.Add(startNode, 0);
                this.hCost.Add(startNode, this.GetEstimatedCost(startNode.Value, goalNode.Value));
                this.Enqueue(startNode);

                while (this.priorityQueue[0] != goalNode)
                {
                    GraphNode<Location> currentNode = this.Dequeue();

                    foreach (GraphNode<Location> childNode in currentNode.Neighbors)
                    {
                        if (this.priorityQueue.Contains(childNode))
                        {
                            if (this.Graph.GetCostOf(currentNode, childNode) + this.GetEstimatedCost(childNode.Value, goalNode.Value) < this.GetTotalCost(childNode))
                            {
                                this.cameFrom[childNode] = currentNode;
                                this.gCost[childNode] = this.Graph.GetCostOf(currentNode, childNode);
                                this.hCost[childNode] = this.GetEstimatedCost(childNode.Value, goalNode.Value);

                                this.priorityQueue.Remove(childNode);
                                this.Enqueue(childNode);
                            }
                        }
                        else if (this.closedSet.Contains(childNode))
                        {
                            Console.WriteLine("Trying to update cameFrom for something in closed set");
                        }
                        else
                        {
                            this.cameFrom.Add(childNode, currentNode);
                            this.gCost.Add(childNode, this.Graph.GetCostOf(currentNode, childNode));
                            this.hCost.Add(childNode, this.GetEstimatedCost(childNode.Value, goalNode.Value));
                            this.Enqueue(childNode);
                        }
                    }

                    this.closedSet.Add(currentNode);
                }
            }

            public Stack<GraphNode<Location>> ReconstructPath(GraphNode<Location> startNode, GraphNode<Location> goalNode)
            {
                Stack<GraphNode<Location>> path = new Stack<GraphNode<Location>>();

                GraphNode<Location> node = goalNode;
                path.Push(node);
                do
                {
                    this.cameFrom.TryGetValue(node, out node);
                    path.Push(node);
                } while (node != startNode);

                return path;
            }

            private int GetEstimatedCost(Location start, Location goal)
            {
                return Math.Abs((goal.x - start.x) + (goal.y - start.y));
            }

            private int GetTotalCost(GraphNode<Location> node)
            {
                if (this.gCost.TryGetValue(node, out int g) && this.hCost.TryGetValue(node, out int h))
                    return g + h;
                else
                    return int.MaxValue;
            }

            private void Enqueue(GraphNode<Location> node)
            {
                foreach (GraphNode<Location> i in this.priorityQueue)
                {
                    if (this.GetTotalCost(node) < this.GetTotalCost(i))
                    {
                        this.priorityQueue.Insert(this.priorityQueue.IndexOf(i), node);
                        return;
                    }
                }

                this.priorityQueue.Add(node);
            }

            private GraphNode<Location> Dequeue()
            {
                GraphNode<Location> node = this.priorityQueue[0];
                this.priorityQueue.RemoveAt(0);
                return node;
            }
        }
    }
}
