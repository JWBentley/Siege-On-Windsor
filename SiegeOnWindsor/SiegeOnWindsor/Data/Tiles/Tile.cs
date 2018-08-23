using SiegeOnWindsor.Data.Defences;
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
        private Defence defence;

        public Tile()
        {

        }

        public Tile(Defence d)
        {
            this.defence = d;
        }

        public virtual Textures.Texture GetGraphic()
        {
            return this.defence != null ? this.defence.GetGraphic() : Textures.emptyTile;
        }
    }
}
