using SiegeOnWindsor.data;
using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Tiles
{
    class NullTile : Tile
    {
        public NullTile() : base(Textures.nullTile)
        {
        }
    }
}
