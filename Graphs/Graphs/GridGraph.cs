using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs
{
    public class GridGraph
    {
        private List<GridNode> nodes;
        private int width, height;

        public GridGraph(int width, int height)
        {
            this.nodes = new List<GridNode>();
            this.width = width;
            this.height = height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    this.nodes.Add(new GridNode(new Vector2(x, y), 1));
                }
            }

            //Neighbours
            foreach (GridNode NodeA in this.nodes)
            {
                foreach (GridNode NodeB in this.nodes)
                {
                    NodeA.SetNeighbour(NodeB, NodeB.Location.X - NodeA.Location.X, NodeB.Location.Y - NodeA.Location.Y);
                }
            }
        }

        public GridNode GetNodeFromLocation(Vector2 node)
        {
            foreach (GridNode n in this.nodes)
            {
                if (n.Location.X == node.X && n.Location.Y == node.Y)
                    return n;
            }

            return null;
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
