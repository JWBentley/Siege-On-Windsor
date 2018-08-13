using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.data
{
    public class Tile
    {
        public bool isNull; //Temp bool used for the outer ring of values
        
        public Tile(bool iNull)
        {
            this.isNull = iNull;
        }

        public Textures.Texture GetGraphic()
        {
            return this.isNull ? Textures.nullTile : Textures.emptyTile;
        }
    }
}
