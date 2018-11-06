using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Enemies.Pathfinding
{
    public class GridNode
    {
        /// <summary>
        /// Location of the node
        /// </summary>
        public Vector2 Location;

        /// <summary>
        /// Cost of moving to the node
        /// </summary>
        public int COST;

        /// <summary>
        /// Adjacent
        /// </summary>
        public GridNode[] Neighbours;


        public GridNode(Vector2 v, int cost)
        {
            this.Location = v; //Sets the location
            this.COST = cost; //Sets the cost

            this.Neighbours = new GridNode[4]; //Creates an array for the neighbours
        }

        /// <summary>
        /// Evaluates a node to see if it is adjacent to the node
        /// </summary>
        /// <param name="n">Node to evaluate</param>
        /// <param name="xDIR">x distance between</param>
        /// <param name="yDIR">y distance between</param>
        public void SetNeighbour(GridNode n, int xDIR, int yDIR)
        {
            //NORTH
            if (xDIR == 0 && yDIR == -1)
                this.Neighbours[0] = n;

            //EAST
            if (xDIR == 1 && yDIR == 0)
                this.Neighbours[1] = n;

            //SOUTH
            if (xDIR == 0 && yDIR == 1)
                this.Neighbours[2] = n;

            //WEST
            if (xDIR == -1 && yDIR == 0)
                this.Neighbours[3] = n;
        }
    }
}
