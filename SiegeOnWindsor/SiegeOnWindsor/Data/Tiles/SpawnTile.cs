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

        public void SpawnEnemy(Enemy enemy, Vector2 goal)
        {
            enemy.Location = this.Location; //Sets the enemies location to the spawn tile
            this.enemies.Add(enemy); //Adds the enemy to the tiles list
            enemy.UpdatePath(goal);
        }

        public override Textures.Texture GetGraphic()
        {
            return Textures.nullTile;
        }
    }
}
