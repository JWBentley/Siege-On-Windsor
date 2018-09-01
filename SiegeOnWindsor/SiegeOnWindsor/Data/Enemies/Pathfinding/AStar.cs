using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Enemies.Pathfinding
{
    public class AStar
    {
        GridGraph Graph;

        List<GraphNode<Vector2>> closedSet;
        List<GraphNode<Vector2>> priorityQueue;

        Dictionary<GraphNode<Vector2>, GraphNode<Vector2>> cameFrom;
        Dictionary<GraphNode<Vector2>, int> gCost;
        Dictionary<GraphNode<Vector2>, int> hCost;

        public AStar(GridGraph g)
        {
            this.Graph = g;
        }

        public Stack<Vector2> Run(Vector2 startNode, Vector2 goalNode)
        {
            return this.Run(this.Graph.GetNodeFromLocation(startNode), this.Graph.GetNodeFromLocation(goalNode));
        }

        public Stack<Vector2> Run(GraphNode<Vector2> startNode, GraphNode<Vector2> goalNode)
        {
            closedSet = new List<GraphNode<Vector2>>();
            priorityQueue = new List<GraphNode<Vector2>>();

            cameFrom = new Dictionary<GraphNode<Vector2>, GraphNode<Vector2>>();
            gCost = new Dictionary<GraphNode<Vector2>, int>();
            hCost = new Dictionary<GraphNode<Vector2>, int>();

            this.cameFrom.Add(startNode, null);
            this.gCost.Add(startNode, 0);
            this.hCost.Add(startNode, this.GetEstimatedCost(startNode.Value, goalNode.Value));
            this.Enqueue(startNode);

            while (this.priorityQueue[0] != goalNode)
            {
                GraphNode<Vector2> currentNode = this.Dequeue();

                foreach (GraphNode<Vector2> childNode in currentNode.Neighbors)
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

            return this.ReconstructPath(startNode, goalNode);
        }

        private Stack<Vector2> ReconstructPath(GraphNode<Vector2> startNode, GraphNode<Vector2> goalNode)
        {
            Stack<Vector2> path = new Stack<Vector2>();

            GraphNode<Vector2> node = goalNode;
            path.Push(node.Value);
            do
            {
                this.cameFrom.TryGetValue(node, out node);
                path.Push(node.Value);
            } while (node != startNode);

            return path;
        }

        private int GetEstimatedCost(Vector2 start, Vector2 goal)
        {
            return Math.Abs((int)((goal.X - start.X) + (goal.Y - start.Y)));
        }

        private int GetTotalCost(GraphNode<Vector2> node)
        {
            if (this.gCost.TryGetValue(node, out int g) && this.hCost.TryGetValue(node, out int h))
                return g + h;
            else
                return int.MaxValue;
        }

        private void Enqueue(GraphNode<Vector2> node)
        {
            foreach (GraphNode<Vector2> i in this.priorityQueue)
            {
                if (this.GetTotalCost(node) < this.GetTotalCost(i))
                {
                    this.priorityQueue.Insert(this.priorityQueue.IndexOf(i), node);
                    return;
                }
            }

            this.priorityQueue.Add(node);
        }

        private GraphNode<Vector2> Dequeue()
        {
            GraphNode<Vector2> node = this.priorityQueue[0];
            this.priorityQueue.RemoveAt(0);
            return node;
        }
    }
}

