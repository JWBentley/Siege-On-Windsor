using Pathfinding.Pathfinding;
using Pathfinding.Pathfinding.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathfinding
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        Label[,] grid;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void ClearGrid()
        {
            if (this.grid != null)
                for (int x = 0; x < this.grid.GetLength(0); x++)
                {
                    for (int y = 0; y < this.grid.GetLength(1); y++)
                    {
                        if (grid[x, y] != null)
                            this.Controls.Remove(grid[x, y]);
                        grid[x, y] = null;
                    }
                }

            this.grid = null;
        }

        public void DrawGrid(int width, int height)
        {
            this.ClearGrid();

            this.grid = new Label[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid[x, y] = new Label()
                    {
                        Location = new System.Drawing.Point(x * 50, y * 50),
                        Name = "label1",
                        Size = new System.Drawing.Size(50, 50),
                        TabIndex = 0,
                        //Text = rand.Next(1, 10).ToString(),
                        Text = "1",
                        TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                    };

                    if (x == width / 2 && y == height / 2)
                    {
                        grid[x, y].Text = "0";
                        grid[x, y].BackColor = Color.Black;
                        grid[x, y].ForeColor = Color.White;
                    }

                    this.Controls.Add(grid[x, y]);
                }
            }

            this.ClientSize = new System.Drawing.Size(width*50, height*50);
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 's')
            {
                new Form2(this).Show();
            }
            else if (e.KeyChar == 'g')
            {
                CreateGraph();
            }
        }

        private void CreateGraph()
        {
            int[,] data = new int[this.grid.GetLength(0), this.grid.GetLength(1)];
            for (int x = 0; x < this.grid.GetLength(0); x++)
            {
                for (int y = 0; y < this.grid.GetLength(1); y++)
                {
                    data[x, y] = int.Parse(this.grid[x, y].Text);
                }
            }

            GridGraph gridGraph = new GridGraph(data);

                PathFinder.AStar aStar = new PathFinder.AStar(gridGraph);


                Location start = new Location(0,0);
                Location goal = new Location(this.grid.GetLength(0) / 2, this.grid.GetLength(1) / 2);

                aStar.Run(gridGraph.GetNodeFromLocation(start), gridGraph.GetNodeFromLocation(goal));
                Stack<GraphNode<Location>> path = aStar.ReconstructPath(gridGraph.GetNodeFromLocation(start), gridGraph.GetNodeFromLocation(goal));

                

                while (path.Count > 0)
                {
                    Location a = path.Pop().Value;
                    this.grid[a.x, a.y].BackColor = Color.Green;
                }



            /*
            Graph<Location> gridGraph = new Graph<Location>();
            GraphNode<Location>[,] gridNodes = new GraphNode<Location>[this.grid.GetLength(0), this.grid.GetLength(1)];

            for (int x = 0; x < this.grid.GetLength(0); x++)
            {
                for (int y = 0; y < this.grid.GetLength(1); y++)
                {
                    GraphNode<Location> node = new GraphNode<Location>(new Location(x, y));
                    gridGraph.AddNode(node);
                    gridNodes[x, y] = node;
                }
            }

            for (int x = 0; x < gridNodes.GetLength(0); x++)
            {
                for (int y = 0; y < gridNodes.GetLength(1); y++)
                {
                    List<Location> adj = new List<Location>();

                    if (x + 1 < gridNodes.GetLength(0))
                        gridGraph.AddDirectedEdge(gridNodes[x, y], gridNodes[x + 1, y], int.Parse(this.grid[x + 1, y].Text));
                    if (x - 1 >= 0)
                        gridGraph.AddDirectedEdge(gridNodes[x, y], gridNodes[x - 1, y], int.Parse(this.grid[x - 1, y].Text));
                    if (y + 1 < gridNodes.GetLength(1))
                        gridGraph.AddDirectedEdge(gridNodes[x, y], gridNodes[x, y + 1], int.Parse(this.grid[x, y + 1].Text));
                    if (y - 1 >= 0)
                        gridGraph.AddDirectedEdge(gridNodes[x, y], gridNodes[x, y - 1], int.Parse(this.grid[x, y - 1].Text));
                }
            }

            
            GraphNode<Location> testNode = (GraphNode<Location>)gridGraph.Nodes.FindByValue(new Location(4, 3));
            GraphNode<Location> testNode2 = (GraphNode<Location>)gridGraph.Nodes.FindByValue(new Location(4, 4));
            int cost = testNode.Costs.ToArray<int>()[testNode.Neighbors.IndexOf(testNode2)];
            Console.WriteLine(cost.ToString());

            cost = testNode2.Costs.ToArray<int>()[testNode2.Neighbors.IndexOf(testNode)];
            Console.WriteLine(cost.ToString());

            AStar aStar = new AStar();
            Console.WriteLine(aStar.GetEstimatedCost(new Pathfinding.Location(1, 1), new Pathfinding.Location(3, 3)));

            foreach(GraphNode<Location> child in testNode.Neighbors)
            {
                Console.WriteLine("X:{0}, Y:{1}", child.Value.x, child.Value.y);
            }*/
        }
    }
}
