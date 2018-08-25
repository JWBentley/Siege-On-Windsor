using Microsoft.Xna.Framework;
using SiegeOnWindsor.Data.Defences;
using SiegeOnWindsor.Data.Enemies;
using SiegeOnWindsor.Data.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.data
{
    public class World : IUpdate
    {
        public Tile[,] Grid { private set;  get; }

        /// <summary>
        /// Risk to the enemy of moving to each tile in the world
        /// </summary>
        private int[,] RiskMap;

        public World()
        {
            this.CreateGrid(17, 17);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Tile tile in this.Grid)
            {
                tile.Update(gameTime);
            }
        }

        private void CreateGrid(int width, int height)
        {
            this.Grid = new Tile[width, width]; //Creates new tile grid of the specified size
            this.RiskMap = new int[width, width]; //Creates a risk map of the specified size

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                        this.Grid[x, y] = new Tile(this, new Vector2(x, y), new NullDef());
                    else if (x == width / 2 && y == height / 2)
                        this.Grid[x, y] = new Tile(this, new Vector2(x,y), new CrownDef());
                    else this.Grid[x, y] = new Tile(this, new Vector2(x, y));
                }
            }

            ((NullDef)this.GetTileAt(0, 1).defence).SpawnEnemy(new PeasantEnemy(100));
            ((NullDef)this.GetTileAt(0, 2).defence).SpawnEnemy(new PeasantEnemy(50));



            this.UpdateRiskMap();
        }

        /// <summary>
        /// Updates the risk map for pathfinding, should be called on a world update (Defences being built/destroyed)
        /// </summary>
        private void UpdateRiskMap()
        {
            //Loops through each tile
            for (int x = 0; x < this.RiskMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.RiskMap.GetLength(1); y++)
                {
                    this.RiskMap[x, y] = this.GetTileAt(x, y).GetBaseRiskValue(); //Gets the base risk value of the tile and adds it to the map
                }
            }

            //Loops through each tile with a defence
            for (int x = 0; x < this.RiskMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.RiskMap.GetLength(1); y++)
                {
                    if (this.GetTileAt(x, y).defence != null)
                        foreach(Vector2 location in this.GetTileAt(x, y).defence.GetImpactedTiles()) //Gets and loops through each tile that the defence on the current tile can impact (attack)
                        {
                            this.RiskMap[(int)location.X, (int)location.Y] += this.GetTileAt(x, y).defence.GetImpactOnTile(location); //Adds that impact to the risk map
                        }
                }
            }
        }

        public Tile GetTileAt(int x, int y)
        {
            return this.Grid[x, y];
        }
    }
}
