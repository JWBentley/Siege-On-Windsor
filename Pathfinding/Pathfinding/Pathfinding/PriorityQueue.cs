using Pathfinding.Pathfinding.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pathfinding.Pathfinding.AStar;

namespace Pathfinding.Pathfinding
{
    public class PriorityQueue
    {
        private List<ANode<Location>> queue;

        public PriorityQueue()
        {
            queue = new List<ANode<Location>>();
        }

        public void Enqueue(ANode<Location> node)
        {
            foreach (ANode<Location> i in this.queue)
            {
                if (node.TotalCost < i.TotalCost)
                {
                    this.queue.Insert(this.queue.IndexOf(i), node);
                    return;
                }
            }

            this.queue.Add(node);
        }

        public ANode<Location> Dequeue()
        {
            ANode<Location> node = this.queue.First();
            this.queue.Remove(node);
            return node;
        }

        public ANode<Location> GetFirst()
        {
            return this.queue.First();
        }

        public bool Contains(GraphNode<Location> node)
        {
            List<GraphNode<Location>> queueNodes = new List<GraphNode<Location>>();
            foreach (ANode<Location> item in this.queue)
            {
                queueNodes.Add(item.node);
            }

            return queueNodes.Contains(node);
        }

        public ANode<Location> Get(GraphNode<Location> child)
        {
            List<GraphNode<Location>> queueNodes = new List<GraphNode<Location>>();
            foreach (ANode<Location> item in this.queue)
            {
                if (item.node == child)
                    return item;
            }

            return null;
        }

        public bool UpdateCost(GraphNode<Location> child, int cost, ANode<Location> parent)
        {
            foreach (ANode<Location> item in this.queue)
            {
                if (item.node == child)
                {
                    item.gCost = cost;
                    item.cameFrom = parent;
                    return true;
                }
            }

            return false;
        }
    }
}
