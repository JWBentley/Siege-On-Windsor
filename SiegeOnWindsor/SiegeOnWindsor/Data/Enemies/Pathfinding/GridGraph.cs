using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Enemies.Pathfinding
{
    public class GridGraph
    {
        /// <summary>
        /// List of nodes that make up the graph
        /// </summary>
        private List<GridNode> nodes;
        /// <summary>
        /// Size of the grid
        /// </summary>
        private int width, height;


        public GridGraph(int[,] data)
        {
            this.nodes = new List<GridNode>(); //Creates a new list of nodes
            this.width = data.GetLength(0); //Sets the width
            this.height = data.GetLength(1); //Sets the height

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    this.nodes.Add(new GridNode(new Vector2(x, y), data[x, y])); //Creates a node for each position
                }
            }

            //Neighbours
            foreach (GridNode NodeA in this.nodes) //Runs through each node
            {
                foreach (GridNode NodeB in this.nodes) //Runs through every other node
                {
                    NodeA.SetNeighbour(NodeB, (int)(NodeB.Location.X - NodeA.Location.X), (int)(NodeB.Location.Y - NodeA.Location.Y)); //Evaluates to see if they are neighbours
                }
            }
        }

        /// <summary>
        /// Gets a node from a location
        /// </summary>
        /// <param name="node">Location as a vector</param>
        /// <returns>Returns the corresponding node</returns>
        public GridNode GetNodeFromLocation(Vector2 node)
        {
            foreach (GridNode n in this.nodes) //Runs through all the nodes
            {
                if (n.Location.X == node.X && n.Location.Y == node.Y) //If a location match is found
                    return n; //Returns the node
            }

            return null; //Not found - returns node
        }

        //Alternative method for gaining access to neighbours
        public List<GridNode> GetNeighbours(GridNode node)
        {
            List<GridNode> neighbours = new List<GridNode>();

            //NORTH
            if (node.Location.Y - 1 >= 0)
                neighbours.Add(this.GetNodeFromLocation(new Vector2(node.Location.X, node.Location.Y - 1)));

            //EAST
            if (node.Location.X + 1 < this.width)
                neighbours.Add(this.GetNodeFromLocation(new Vector2(node.Location.X + 1, node.Location.Y)));

            //SOUTH
            if (node.Location.Y + 1 < this.height)
                neighbours.Add(this.GetNodeFromLocation(new Vector2(node.Location.X, node.Location.Y + 1)));

            //WEST
            if (node.Location.X - 1 >= 0)
                neighbours.Add(this.GetNodeFromLocation(new Vector2(node.Location.X - 1, node.Location.Y)));

            return neighbours;
        }
    }
}

