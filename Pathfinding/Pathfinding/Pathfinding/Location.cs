using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.Pathfinding
{
    public struct Location
    {
        public readonly int x, y;

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            Location locB = (Location)obj;
            return this.x == locB.x && this.y == locB.y;
        }
    }
}
