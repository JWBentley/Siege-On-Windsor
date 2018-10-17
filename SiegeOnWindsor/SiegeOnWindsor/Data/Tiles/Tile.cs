using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;
using SiegeOnWindsor.Data.Defences;
using SiegeOnWindsor.Data.Enemies;
using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Tiles
{
    public class Tile : IUpdate
    {
        public List<Enemy> enemies;
        public Defence defence;

        public World World;
        public Vector2 Location;

        public Tile(World w, Vector2 l) : this(w, l, null)
        {
            
        }

        public Tile(World w, Vector2 l, Defence d)
        {
            this.World = w;

            this.Location = l;

            this.defence = d;

            if(this.defence != null)
                this.defence.Tile = this;

            this.enemies = new List<Enemy>(); 
        }

        public void Update(GameTime gameTime)
        {
            if (this.defence != null)
                this.defence.Update(gameTime);

            //foreach (Enemy enemy in this.enemies)
            //{
            //    enemy.Update(gameTime);
            //}
        }

        public virtual int GetBaseRiskValue()
        {
            int risk = 1;

            if (this.defence != null)
                risk += this.defence.GetBaseRiskValue();

            return risk;
        }

        public virtual Textures.Texture GetGraphic()
        {
            return Textures.emptyTile;
        }
    }
}
