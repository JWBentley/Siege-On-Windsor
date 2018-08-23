using SiegeOnWindsor.Data.Defences;
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

        public World()
        {
            this.CreateGrid(17, 17);
        }

        public void Update()
        {

        }

        private void CreateGrid(int width, int height)
        {
            this.Grid = new Tile[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                        this.Grid[x, y] = new Tile(new NullDef());
                    else if (x == width / 2 && y == height / 2)
                        this.Grid[x, y] = new Tile(new CrownDef());
                    else this.Grid[x, y] = new Tile();
                }
            }
        }

        public Tile GetTileAt(int x, int y)
        {
            return this.Grid[x, y];
        }
    }
}
