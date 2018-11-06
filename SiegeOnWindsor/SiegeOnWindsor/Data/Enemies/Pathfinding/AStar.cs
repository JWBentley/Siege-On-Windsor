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
                            if (this.gCost[currentNode] + childNode.COST < this.gCost[childNode]) //If this path to the node is better than the current one
                            {
                                this.cameFrom[childNode] = currentNode; //Updates cameFrom
                                this.gCost[childNode] = this.gCost[currentNode] + childNode.COST; //Updates gCost
                                this.hCost[childNode] = this.GetEstimatedCost(childNode.Location, goalNode.Location); //Updates hCost (probably not needed but better safe than sorry)

                                //I must remove and add the node back to the queue so that it is positioned correctly within the queue
                                this.priorityQueue.Remove(childNode); //Removes item from queue
                                this.Enqueue(childNode); //Adds item back to the queue
                            }
                        }
                        else if (!this.closedSet.Contains(childNode)) //If the node is not in the priority queue or closed set
                        {
                            this.cameFrom.Add(childNode, currentNode); //cameFrom is set to the currentNode
                            this.gCost.Add(childNode, childNode.COST + this.gCost[currentNode]); //gCost is calculated
                            this.hCost.Add(childNode, this.GetEstimatedCost(childNode.Location, goalNode.Location)); //hCost is calculated
                            this.Enqueue(childNode); //childNode is added to the priority queue
                        }
                    }
                }
                this.closedSet.Add(currentNode); //The current node has now been fully evaluated, therefore it is added to the closed set
            }
            return this.ReconstructPath(startNode, goalNode); //Returns the reconstructed path
        }

        /// <summary>
        /// This reconstructs the path found in Run() by working its way backwards from the goal to the start
        /// </summary>
        /// <param name="startNode">Starting node</param>
        /// <param name="goalNode">Goal node</param>
        /// <returns>Path as a stack</returns>
        private Stack<Vector2> ReconstructPath(GridNode startNode, GridNode goalNode)
        {
            Stack<Vector2> path = new Stack<Vector2>(); //Creates new stack

            GridNode node = goalNode; //Sets the current node to the goal node
            path.Push(node.Location); //Adds the goal node to the stack
            do
            {
                this.cameFrom.TryGetValue(node, out node); //Gets the node the current node came from, so the previous node annd sets that as the new current node
                path.Push(node.Location); //Adds the node to the stack
            } while (node != startNode); //Runs unitl the start node has been reached 

            return path; //Returns the completed path
        }

        /// <summary>
        /// This function estimates the cost of moving from one node to another
        /// </summary>
        /// <param name="start">Starting node</param>
        /// <param name="goal">Goal node</param>
        /// <returns></returns>
        private int GetEstimatedCost(Vector2 start, Vector2 goal)
        {
            return Math.Abs((int)Math.Sqrt(Math.Pow(goal.X - start.X, 2) + Math.Pow(goal.Y - start.Y, 2))); //Uses Pythagoras' theorem to calcuate the distance between the two nodes
        }

        /// <summary>
        /// Gets the sum of the gCost and hCost for a node
        /// </summary>
        /// <param name="node">Node to get the cost of</param>
        /// <returns>gCost[node] + hCost[node]</returns>
        private int GetTotalCost(GridNode node)
        {
            if (this.gCost.TryGetValue(node, out int g) && this.hCost.TryGetValue(node, out int h)) //If the node has been evaluated
                return g + h; //Returns gCost plus hCost
            else
                return int.MaxValue; //Returns infinity as we do not want to underestimate as this could break the algorithm
        }

        /// <summary>
        /// Adds a node to the priority queue
        /// </summary>
        /// <param name="node">Node to add</param>
        private void Enqueue(GridNode node)
        {
            foreach (GridNode i in this.priorityQueue) //Runs though each node in the queue
            {
                if (this.GetTotalCost(node) < this.GetTotalCost(i)) //If the total cost of the node is cheaper than the node in the queue at point i
                {
                    this.priorityQueue.Insert(this.priorityQueue.IndexOf(i), node); //The node is placed in front of that point, the rest of the queue automatically shuffles down
                    return; //Exits the method
                }
            }

            this.priorityQueue.Add(node); //If the node has a greater cost than all nodes in the queue then it is added to the end of the queue
        }

        /// <summary>
        /// Pops a node off the queue
        /// </summary>
        /// <returns></returns>
        private GridNode Dequeue()
        {
            GridNode node = this.priorityQueue[0]; //Gets the first node in the queue
            this.priorityQueue.RemoveAt(0); //Removes the node from the queue, all other nodes shuffle up
            return node; //Returns the node
        }
    }
}

