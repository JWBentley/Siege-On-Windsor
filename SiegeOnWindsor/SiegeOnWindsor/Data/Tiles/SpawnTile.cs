using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;
using SiegeOnWindsor.Data.Defences;
using SiegeOnWindsor.Graphics;
using SiegeOnWindsor.Data.Enemies;

namespace SiegeOnWindsor.Data.Tiles
{
    public class SpawnTile : Tile
    {
        public SpawnTile(World w, Vector2 l) : base(w, l)
        {
        }

        public SpawnTile(World w, Vector2 l, Defence d) : base(w, l, d)
        {

        }

        /// <summary>
        /// Spawns a new enemy on the tile
        /// </summary>
        /// <param name="enemy">Enemy to spawn</param>
        /// <param name="goal">Location the enemy should move towards</param>
        public void SpawnEnemy(Enemy enemy, Vector2 goal)
        {
            enemy.Location = this.Location; //Sets the enemies location to the spawn tile
            this.Enemies.Add(enemy); //Adds the enemy to the tiles list
            enemy.UpdatePath(goal); //Upates the path of the enemy
        }

        public override Graphics.Graphics.Graphic GetGraphic()
        {
            return Graphics.Graphics.spawnTile;
        }
    }
}
