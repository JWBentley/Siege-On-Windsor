using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    public class AStar
    {
        GridGraph Graph;

        List<GridNode> closedSet;
        List<GridNode> priorityQueue;

        Dictionary<GridNode, GridNode> cameFrom;
        Dictionary<GridNode, int> gCost;
        Dictionary<GridNode, int> hCost;

        public AStar(GridGraph g)
        {
            this.Graph = g;
        }

        public Stack<Vector2> Run(Vector2 startNode, Vector2 goalNode)
        {
            return this.Run(this.Graph.GetNodeFromLocation(startNode), this.Graph.GetNodeFromLocation(goalNode));
        }

        public Stack<Vector2> Run(GridNode startNode, GridNode goalNode)
        {
            closedSet = new List<GridNode>();
            priorityQueue = new List<GridNode>();

            cameFrom = new Dictionary<GridNode, GridNode>();
            gCost = new Dictionary<GridNode, int>();
            hCost = new Dictionary<GridNode, int>();

            this.cameFrom.Add(startNode, null);
            this.gCost.Add(startNode, 0);
            this.hCost.Add(startNode, this.GetEstimatedCost(startNode.Location, goalNode.Location));
            this.Enqueue(startNode);

            while (this.priorityQueue[0] != goalNode)
            {
                GridNode currentNode = this.Dequeue();
                Console.WriteLine("Location:({0},{1})", currentNode.Location.X.ToString(), currentNode.Location.Y.ToString());
                foreach (GridNode childNode in currentNode.Neighbours)
                {
                    if (childNode != null)
                    {
                        if (this.priorityQueue.Contains(childNode))
                        {
                            if (this.gCost[currentNode] + childNode.COST < this.gCost[childNode])
                            {
                                this.cameFrom[childNode] = currentNode;
                                this.gCost[childNode] = this.gCost[currentNode] + childNode.COST;
                                this.hCost[childNode] = this.GetEstimatedCost(childNode.Location, goalNode.Location);

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
                            this.gCost.Add(childNode, childNode.COST + this.gCost[currentNode]);
                            this.hCost.Add(childNode, this.GetEstimatedCost(childNode.Location, goalNode.Location));
                            this.Enqueue(childNode);
                        }
                    }
                }
                this.closedSet.Add(currentNode);
            }

            Console.WriteLine("DONE");
            return this.ReconstructPath(startNode, goalNode);
        }

        private Stack<Vector2> ReconstructPath(GridNode startNode, GridNode goalNode)
        {
            Stack<Vector2> path = new Stack<Vector2>();

            GridNode node = goalNode;
            path.Push(node.Location);
            do
            {
                this.cameFrom.TryGetValue(node, out node);
                path.Push(node.Location);
            } while (node != startNode);

            return path;
        }

        private int GetEstimatedCost(Vector2 start, Vector2 goal)
        {
            return Math.Abs((int)Math.Sqrt(Math.Pow(goal.X - start.X, 2) + Math.Pow(goal.Y - start.Y, 2)));
        }

        private int GetTotalCost(GridNode node)
        {
            if (this.gCost.TryGetValue(node, out int g) && this.hCost.TryGetValue(node, out int h))
                return g + h;
            else
                return int.MaxValue;
        }

        private void Enqueue(GridNode node)
        {
            foreach (GridNode i in this.priorityQueue)
            {
                if (this.GetTotalCost(node) < this.GetTotalCost(i))
                {
                    this.priorityQueue.Insert(this.priorityQueue.IndexOf(i), node);
                    return;
                }
            }

            this.priorityQueue.Add(node);
        }

        private GridNode Dequeue()
        {
            GridNode node = this.priorityQueue[0];
            this.priorityQueue.RemoveAt(0);
            return node;
        }
    }
}
