using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Tiles
{
    public class Tile
    {
        private Textures.Texture graphic = null;

        public Tile()
        {

        }

        public Tile(Textures.Texture g)
        {
            this.graphic = g;
        }

        public virtual Textures.Texture GetGraphic()
        {
            return this.graphic != null ? this.graphic : Textures.emptyTile;
        }
    }
}
