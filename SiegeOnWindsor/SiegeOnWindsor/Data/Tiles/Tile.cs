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

        public Tile() : this(null)
        {
            
        }

        public Tile(Defence d)
        {
            this.defence = d;
            this.enemies = new List<Enemy>(); 
        }

        public void Update()
        {
            if (this.defence != null)
                this.defence.Update();

            foreach (Enemy enemy in this.enemies)
            {
                enemy.Update();
            }
        }

        public virtual Textures.Texture GetGraphic()
        {
            return this.defence != null ? this.defence.GetGraphic() : Textures.emptyTile;
        }
    }
}
