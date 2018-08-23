using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiegeOnWindsor.Graphics;

namespace SiegeOnWindsor.Data.Defences
{
    public class Defence
    {
        private Textures.Texture graphic;

        public Defence()
        {

        }

        public Defence(Textures.Texture g)
        {
            this.graphic = g;
        }

        public virtual Textures.Texture GetGraphic()
        {
            return this.graphic != null ? this.graphic : Textures.emptyTile;
        }
    }
}
