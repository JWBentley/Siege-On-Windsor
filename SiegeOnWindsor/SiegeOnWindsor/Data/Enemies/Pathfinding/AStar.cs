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
        /// <summary>
        /// Graph to traverse
        /// </summary>
        GridGraph Graph;

        /// <summary>
        /// List of nodes that have been fully evaluated
        /// </summary>
        List<GridNode> closedSet;
        /// <summary>
        /// List of nodes the have not been fully evaluated and are in ascending order of cost
        /// </summary>
        List<GridNode> priorityQueue;

        /// <summary>
        /// Dictionary that links a node to the node it came from
        /// </summary>
        Dictionary<GridNode, GridNode> cameFrom;
        /// <summary>
        /// Dictionary that links a node to the cost of moving from the start to that node
        /// </summary>
        Dictionary<GridNode, int> gCost;
        /// <summary>
        /// Dictionary that links a node to the estimated cost of moving from the node to the goal
        /// </summary>
        Dictionary<GridNode, int> hCost;

        /// <summary>
        /// Creates a new AStar object
        /// </summary>
        /// <param name="g">Graph to traverse</param>
        public AStar(GridGraph g)
        {
            this.Graph = g; //Sets the graph
        }

        /// <summary>
        /// Runs the patfinding algorithm to get the cheapest path from one node to another
        /// </summary>
        /// <param name="startNode">The starting node location</param>
        /// <param name="goalNode">The goal node location</param>
        /// <returns>Path as a stack of Vector2 objects</returns>
        public Stack<Vector2> Run(Vector2 startNode, Vector2 goalNode)
        {
            return this.Run(this.Graph.GetNodeFromLocation(startNode), this.Graph.GetNodeFromLocation(goalNode)); //Runs the override
        }

        /// <summary>
        /// Runs the patfinding algorithm to get the cheapest path from one node to another
        /// </summary>
        /// <param name="startNode">The starting node</param>
        /// <param name="goalNode">The goal node</param>
        /// <returns></returns>
        public Stack<Vector2> Run(GridNode startNode, GridNode goalNode)
        {
            closedSet = new List<GridNode>(); //Clears the closed set
            priorityQueue = new List<GridNode>(); //Clears the priority queue

            cameFrom = new Dictionary<GridNode, GridNode>(); //Clears cameFrom
            gCost = new Dictionary<GridNode, int>(); //Clears gCost
            hCost = new Dictionary<GridNode, int>(); //Cleats hCost

            this.cameFrom.Add(startNode, null); //Adds the starting node to cameFrom with a null parent node
            this.gCost.Add(startNode, 0); //Adds a cost of 0 of moving to the starting node
            this.hCost.Add(startNode, this.GetEstimatedCost(startNode.Location, goalNode.Location)); //Estimates total cost to goal using distance
            this.Enqueue(startNode); //Adds the starting node to the priority queue

            //While loop runs until the goal node is at the top of the priority queue
            while (this.priorityQueue[0] != goalNode)
            {
                GridNode currentNode = this.Dequeue(); // Gets the node at the top of the priority queue
                //Console.WriteLine("Location:({0},{1})", currentNode.Location.X.ToString(), currentNode.Location.Y.ToString());
                foreach (GridNode childNode in currentNode.Neighbours) //Runs through each neigbour of the node
                {
                    if (childNode != null) //If the neighbour is not null
                    {
                        if (this.priorityQueue.Contains(childNode)) //If the child node is already in the priority queue
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
                        else if (!this.closedSet.Contains(childNode)) //If the node is not in the priority queue or closed set
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

