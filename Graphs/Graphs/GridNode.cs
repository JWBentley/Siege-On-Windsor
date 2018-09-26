using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    public class GridNode
    {
        public Vector2 Location;

        public int COST;

        public GridNode[] Neighbours;


        public GridNode(Vector2 v, int cost)
        {
            this.Location = v;
            this.COST = cost;

            this.Neighbours = new GridNode[4];
        }

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
