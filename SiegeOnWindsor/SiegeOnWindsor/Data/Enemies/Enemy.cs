using SiegeOnWindsor.data;
using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Enemies
{
    public abstract class Enemy : IUpdate
    {
        private Textures.Texture graphic;

        public Enemy()
        {

        }

        public Enemy(Textures.Texture g)
        {
            this.graphic = g;
        }

        public abstract void Update();

        public virtual Textures.Texture GetGraphic()
        {
            return this.graphic != null ? this.graphic : Textures.emptyTile;
        }
    }
}
