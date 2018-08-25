using Microsoft.Xna.Framework;
using SiegeOnWindsor.Data.Enemies;
using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Defences
{
    public class NullDef : Defence
    {
        public NullDef() : base(Textures.nullTile)
        {

        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public void SpawnEnemy(Enemy enemy)
        {
            if (this.Tile != null)
            {
                enemy.Location = this.Tile.Location;
                this.Tile.enemies.Add(enemy);
            }
        }
    }
}
