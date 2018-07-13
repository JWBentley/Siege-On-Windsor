using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.data
{
    public class World :IUpdate
    {
        public Tile[,] Grid { private set;  get; }

        public World()
        {
            this.CreateGrid(15, 15);
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
                    this.Grid[x, y] = new Tile();

                    if (x == width / 2 && y == height / 2)
                        ; //Set Crown
                }
            }
        }

        public Tile GetTileAt(int x, int y)
        {
            return this.Grid[x, y];
        }
    }
}
