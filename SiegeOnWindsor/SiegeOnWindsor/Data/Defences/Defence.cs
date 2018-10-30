using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiegeOnWindsor.Graphics;
using SiegeOnWindsor.data;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.Data.Tiles;

namespace SiegeOnWindsor.Data.Defences
{
    public abstract class Defence : IUpdate
    {
        public int Health = 1;

        public Tile Tile;

        private Textures.Texture graphic;

        public Defence()
        {

        }

        public Defence(Textures.Texture g)
        {
            this.graphic = g;
        }

        public abstract void Update(GameTime gameTime);

        public void DealDamage(int damage)
        {
            if (this.Health - damage > 0)
                this.Health -= damage;
            else
                this.Die();
        }

        public virtual void Die()
        {
            this.Tile.ClearDefence();
        }

        public int GetBaseRiskValue()
        {
            return this.Health / 100;
        }

        public virtual List<Vector2> GetImpactedTiles()
        {
            return new List<Vector2>();
        }

        public int GetImpactOnTile(Vector2 location)
        {
            return 0;
        }

        public virtual Textures.Texture GetGraphic()
        {
            return this.graphic != null ? this.graphic : Textures.emptyTile;
        }
    }
}
