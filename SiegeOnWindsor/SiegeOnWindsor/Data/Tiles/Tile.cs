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
        public List<Enemy> Enemies;
        public Defence Defence;

        public World World;
        public Vector2 Location;

        public Tile(World w, Vector2 l) : this(w, l, null)
        {
            
        }

        public Tile(World w, Vector2 l, Defence d)
        {
            this.World = w;

            this.Location = l;

            this.Defence = d;

            if(this.Defence != null)
                this.Defence.Tile = this;

            this.Enemies = new List<Enemy>(); 
        }

        public void Update(GameTime gameTime)
        {
            if (this.Defence != null)
                this.Defence.Update(gameTime);

            //foreach (Enemy enemy in this.enemies)
            //{
            //    enemy.Update(gameTime);
            //}
        }

        public void AddDefence(Defence def)
        {
            this.Defence = def;
            def.Tile = this;
        }

        public void ClearDefence()
        {
            this.Defence = null;
        }

        public virtual int GetBaseRiskValue()
        {
            int risk = 1;

            if (this.Defence != null)
                risk += this.Defence.GetBaseRiskValue();

            return risk;
        }

        public virtual Textures.Texture GetGraphic()
        {
            return Textures.emptyTile;
        }
    }
}
