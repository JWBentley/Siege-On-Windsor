using Pathfinding.Pathfinding.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.Pathfinding
{
    public class AStar
    {
        public Stack<GraphNode<Location>> Run(GraphNode<Location> start, GraphNode<Location> goal)
        {
            List<ANode<Location>> closed = new List<ANode<Location>>();
            PriorityQueue queue = new PriorityQueue();

            ANode<Location> startA = new ANode<Location>(start, null, 0, GetEstimatedCost(start.Value, goal.Value));

            queue.Enqueue(startA);

            while(!queue.GetFirst().node.Value.Equals(goal.Value))
            {
                if (queue.GetFirst() == null)
                    break;

                ANode<Location> current = queue.Dequeue();
                foreach (GraphNode<Location> child in current.node.Neighbors)
                {
                    int cost = current.gCost + current.node.Costs.ToArray<int>()[current.node.Neighbors.IndexOf(child)];
                    int heuristic = GetEstimatedCost(child.Value, goal.Value);

                    if (queue.Contains(child) && queue.Get(child).TotalCost > cost)
                    {
                        if (queue.UpdateCost(child, cost, current))
                            Console.WriteLine("Update success");
                        else
                            Console.WriteLine("Update failed");
                    }
                    else
                    {
                        //When the queue either doesn't contain the thing or the update fails (which should have it's own handling tbh)
                        queue.Enqueue(new ANode<Location>(child, current, cost, heuristic));
                    }
                }

                closed.Add(current);
            }

            Stack<GraphNode<Location>> path = new Stack<GraphNode<Location>>(); 
            ANode<Location> blah = queue.Dequeue();
            path.Push(blah.node);
            do
            {
                blah = blah.cameFrom;
                path.Push(blah.node);  
            } while (blah != startA);

            return path;
        }

       

        public int GetEstimatedCost(Location start, Location goal)
        {
            return Math.Abs((goal.x - start.x) + (goal.y - start.y));
        }

        public class ANode<V>
        {
            public GraphNode<V> node;
            public ANode<V> cameFrom;
            public int gCost; //Cost to node
            public int hCost; //Cost from node to goal
            public int TotalCost {
                get
                {
                    return this.gCost + this.hCost;
                }
            }

            public ANode(GraphNode<V> n, ANode<V> c, int g, int h)
            {
                this.node = n;
                this.cameFrom = c;
                this.gCost = g;
                this.hCost = h;
            }
        }
    }
}
