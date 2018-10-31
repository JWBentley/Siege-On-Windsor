using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SiegeOnWindsor.Data.Defences;
using SiegeOnWindsor.Data.Enemies;
using SiegeOnWindsor.Data.Enemies.Pathfinding;
using SiegeOnWindsor.Data.Tiles;
using SiegeOnWindsor.Graphics.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.data
{
    public class World : IUpdate
    {
        public SiegeGame Game;
        public Tile[,] Grid { private set;  get; }
        public List<Enemy> Enemies;

        /// <summary>
        /// Risk to the enemy of moving to each tile in the world
        /// </summary>
        public int[,] RiskMap;
        public AStar aStar;

        public bool HasEnded = false;
        private bool isPaused = false;

        //Testing for playing the game
        public Vector2 SelectedTile; //Testing placing
        KeyboardState prevState = Keyboard.GetState();
        public Button spawnEnemy;
        public Button buildWall;
        public Button deployGuard;

        public World(SiegeGame g)
        {
            this.Game = g;
            this.Enemies = new List<Enemy>();
            this.CreateGrid(17, 17);

            //this.GetTileAt(9, 8).AddDefence(new GuardDef());
            //this.GetTileAt(8, 9).AddDefence(new GuardDef());
            //this.GetTileAt(8, 7).AddDefence(new GuardDef());
            //this.GetTileAt(7, 8).AddDefence(new GuardDef());


            this.UpdateRiskMap();

            //TESTING of spawning enemies
            //((SpawnTile)this.GetTileAt(16, 16)).SpawnEnemy(new PeasantEnemy(this), this.GetCrownLocation());
            //((SpawnTile)this.GetTileAt(16, 4)).SpawnEnemy(new PeasantEnemy(this), this.GetCrownLocation());

            //TESTING
            this.SelectedTile = new Vector2(0, 0);
            this.spawnEnemy = new Button(this.Game.Content.Load<Texture2D>("UI/Buttons/blankButton"), this.Game.Content.Load<SpriteFont>("Fonts/default32"))
            {
                Position = new Vector2(900, 300),
                Text = "Spawn Enemy"
            };

            this.spawnEnemy.Click += (o, i) => { if(this.GetTileAt(this.SelectedTile) is SpawnTile) ((SpawnTile)this.GetTileAt(this.SelectedTile)).SpawnEnemy(new PeasantEnemy(this), this.GetCrownLocation()); };

            this.buildWall = new Button(this.Game.Content.Load<Texture2D>("UI/Buttons/blankButton"), this.Game.Content.Load<SpriteFont>("Fonts/default32"))
            {
                Position = new Vector2(900, 400),
                Text = "Build Wall"
            };

            this.buildWall.Click += (o, i) => { if (!(this.GetTileAt(this.SelectedTile) is SpawnTile) && this.GetTileAt(this.SelectedTile).Defence == null) this.GetTileAt(this.SelectedTile).AddDefence(new StoneWallDef()); };

            this.deployGuard = new Button(this.Game.Content.Load<Texture2D>("UI/Buttons/blankButton"), this.Game.Content.Load<SpriteFont>("Fonts/default32"))
            {
                Position = new Vector2(900, 500),
                Text = "Deploy Guard"
            };

            this.deployGuard.Click += (o, i) => { if (!(this.GetTileAt(this.SelectedTile) is SpawnTile) && this.GetTileAt(this.SelectedTile).Defence == null) this.GetTileAt(this.SelectedTile).AddDefence(new GuardDef()); };


        }

        public void Update(GameTime gameTime)
        {
            if (HasEnded)
            {
                //GAME OVER
            }
            else if (isPaused)
            {
                //PAUSE MENU
            }
            else
            {
                foreach (Tile tile in this.Grid)
                {
                    tile.Update(gameTime);
                }

                foreach (Enemy enemy in this.Enemies) // Fix for enemy movement error
                {
                    enemy.Update(gameTime);
                }

                spawnEnemy.Update(gameTime);
                buildWall.Update(gameTime);
                deployGuard.Update(gameTime);

                if (Keyboard.GetState().IsKeyDown(Keys.Left) && prevState.IsKeyUp(Keys.Left) && this.SelectedTile.X > 0)
                    this.SelectedTile = new Vector2(this.SelectedTile.X - 1, this.SelectedTile.Y);
                else if (Keyboard.GetState().IsKeyDown(Keys.Right) && prevState.IsKeyUp(Keys.Right) && this.SelectedTile.X < this.Grid.GetLength(0) - 1)
                    this.SelectedTile = new Vector2(this.SelectedTile.X + 1, this.SelectedTile.Y);
                else if (Keyboard.GetState().IsKeyDown(Keys.Up) && prevState.IsKeyUp(Keys.Up) && this.SelectedTile.Y > 0)
                    this.SelectedTile = new Vector2(this.SelectedTile.X, this.SelectedTile.Y - 1);
                else if (Keyboard.GetState().IsKeyDown(Keys.Down) && prevState.IsKeyUp(Keys.Down) && this.SelectedTile.Y < this.Grid.GetLength(1) - 1)
                    this.SelectedTile = new Vector2(this.SelectedTile.X, this.SelectedTile.Y + 1);
                this.prevState = Keyboard.GetState();
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
                        this.Grid[x, y] = new SpawnTile(this, new Vector2(x, y), null); //Creates spawn tiles
                    else if (x == width / 2 && y == height / 2)
                        this.Grid[x, y] = new Tile(this, new Vector2(x,y), new CrownDef()); //Creates tile with a crown def
                    else this.Grid[x, y] = new Tile(this, new Vector2(x, y)); //Creates an empty tile
                }
            }
        }

        /// <summary>
        /// Updates the risk map for pathfinding, should be called on a world update (Defences being built/destroyed)
        /// </summary>
        public void UpdateRiskMap()
        {
            this.RiskMap = new int[this.Grid.GetLength(0), this.Grid.GetLength(1)];

            //Loops through each tile
            for (int x = 0; x < this.RiskMap.GetLength(0); x++)
            {
                for (int y = 0; y < this.RiskMap.GetLength(1); y++)
                {
                    this.RiskMap[x, y] += this.GetTileAt(x, y).GetBaseRiskValue(); //Gets the base risk value of the tile and adds it to the map

                    if (this.GetTileAt(x, y).Defence != null)
                        foreach (Vector2 location in this.GetTileAt(x, y).Defence.GetImpactedTiles()) //Gets and loops through each tile that the defence on the current tile can impact (attack)
                        {
                            this.RiskMap[(int)location.X, (int)location.Y] += this.GetTileAt(x, y).Defence.GetImpactOnTile(location); //Adds that impact to the risk map
                        }
                }
            }

            /*
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
            } */

            this.aStar = new AStar(new GridGraph(this.RiskMap));
        }

        public Vector2 GetCrownLocation()
        {
            return new Vector2((int)this.Grid.GetLength(0) / 2, (int)this.Grid.GetLength(1) / 2);
        }

        public Tile GetTileAt(Vector2 loc)
        {
            return GetTileAt((int)loc.X, (int)loc.Y);
        }

        public Tile GetTileAt(int x, int y)
        {
            return this.Grid[x, y];
        }
    }
}
