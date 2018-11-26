﻿using Microsoft.Xna.Framework;
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
        /// <summary>
        /// Reference to the game
        /// </summary>
        public SiegeGame Game;
        /// <summary>
        /// 2D array of tiles that make up a grid
        /// </summary>
        public Tile[,] Grid { private set;  get; }
        /// <summary>
        /// List of all enemies in the world
        /// </summary>
        public List<Enemy> Enemies;

        /// <summary>
        /// Risk to the enemy of moving to each tile in the world
        /// </summary>
        public int[,] RiskMap;
        /// <summary>
        /// Astar pathfinding obj
        /// </summary>
        public AStar aStar;

        /// <summary>
        /// Bool stating if the game is running
        /// </summary>
        public bool IsRunning = false;
        /// <summary>
        /// Bool stating if the game is paused
        /// </summary>
        private bool isPaused = false;

        //Testing for playing the game
        public Vector2 SelectedTile; //Testing placing
        KeyboardState prevState = Keyboard.GetState();
        public Button spawnEnemy;
        public Button buildWall;
        public Button deployGuard;

        public World(SiegeGame g)
        {
            this.Game = g; //Sets game
            this.Enemies = new List<Enemy>(); //New list of enemies
            this.CreateGrid(17, 17); //Creates grid of size 17x17

            //this.GetTileAt(9, 8).AddDefence(new GuardDef());
            //this.GetTileAt(8, 9).AddDefence(new GuardDef());
            //this.GetTileAt(8, 7).AddDefence(new GuardDef());
            //this.GetTileAt(7, 8).AddDefence(new GuardDef());


            this.UpdateRiskMap(); //Updates the risk map

            //TESTING of spawning enemies
            //((SpawnTile)this.GetTileAt(16, 16)).SpawnEnemy(new PeasantEnemy(this), this.GetCrownLocation());
            //((SpawnTile)this.GetTileAt(16, 4)).SpawnEnemy(new PeasantEnemy(this), this.GetCrownLocation());
            //((SpawnTile)this.GetTileAt(0,0)).SpawnEnemy(new PeasantEnemy(this), this.GetCrownLocation());
            //((SpawnTile)this.GetTileAt(16, 16)).SpawnEnemy(new PeasantEnemy(this), this.GetCrownLocation());
            //((SpawnTile)this.GetTileAt(7, 9)).SpawnEnemy(new PeasantEnemy(this), this.GetCrownLocation());
            //((SpawnTile)this.GetTileAt(18, 19)).SpawnEnemy(new PeasantEnemy(this), this.GetCrownLocation());


            //TESTING - Creates buttons for placing: peasants, walls and guards
            this.SelectedTile = new Vector2(0, 0);
            this.spawnEnemy = new Button(this.Game.Content.Load<Texture2D>("UI/Buttons/blankButton"), this.Game.Content.Load<SpriteFont>("Fonts/default32"))
            {
                Position = new Vector2(970, 300),
                Text = "Spawn Enemy"
            };

            this.spawnEnemy.Click += (o, i) => { if(this.GetTileAt(this.SelectedTile) is SpawnTile)
                    ((SpawnTile)this.GetTileAt(this.SelectedTile)).SpawnEnemy(new PeasantEnemy(this), this.GetCrownLocation()); }; //Spawns enemy on selected tile

            this.buildWall = new Button(this.Game.Content.Load<Texture2D>("UI/Buttons/blankButton"), this.Game.Content.Load<SpriteFont>("Fonts/default32"))
            {
                Position = new Vector2(970, 400),
                Text = "Build Wall"
            };

            this.buildWall.Click += (o, i) => { if (!(this.GetTileAt(this.SelectedTile) is SpawnTile) && this.GetTileAt(this.SelectedTile).Defence == null)
                    this.GetTileAt(this.SelectedTile).AddDefence(new StoneWallDef()); }; //Places wall

            this.deployGuard = new Button(this.Game.Content.Load<Texture2D>("UI/Buttons/blankButton"), this.Game.Content.Load<SpriteFont>("Fonts/default32"))
            {
                Position = new Vector2(970, 500),
                Text = "Deploy Guard"
            };

            this.deployGuard.Click += (o, i) => { if (!(this.GetTileAt(this.SelectedTile) is SpawnTile) && this.GetTileAt(this.SelectedTile).Defence == null)
                    this.GetTileAt(this.SelectedTile).AddDefence(new GuardDef()); }; //Places guard


        }

        public void Update(GameTime gameTime)
        {
            if (IsRunning)
            {
                //GAME OVER
            }
            else if (isPaused)
            {
                //PAUSE MENU
            }
            else
            {
                foreach (Tile tile in this.Grid) //Runs through each tile
                {
                    tile.Update(gameTime); //Updates the tile
                }

                foreach (Enemy enemy in this.Enemies) // Fix for enemy movement error
                {
                    enemy.Update(gameTime); //Updates each enemy
                }

                //Updates buttons
                spawnEnemy.Update(gameTime);
                buildWall.Update(gameTime);
                deployGuard.Update(gameTime);

                //Handles selection of tiles
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

        /// <summary>
        /// Creates a new grid using the stated dimensions
        /// </summary>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        private void CreateGrid(int width, int height)
        {
            this.Grid = new Tile[width, width]; //Creates new tile grid of the specified size
            this.RiskMap = new int[width, width]; //Creates a risk map of the specified size

            //Loops through each grid ref
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1) //Outer edge
                        this.Grid[x, y] = new SpawnTile(this, new Vector2(x, y), null); //Creates spawn tiles
                    else if (x == width / 2 && y == height / 2) //Middle
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
            this.RiskMap = new int[this.Grid.GetLength(0), this.Grid.GetLength(1)]; //Creates new 2D int array

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

        /// <summary>
        /// Gets the crown location
        /// </summary>
        /// <returns>Crown coord as Vector2</returns>
        public Vector2 GetCrownLocation()
        {
            return new Vector2((int)this.Grid.GetLength(0) / 2, (int)this.Grid.GetLength(1) / 2);
        }

        /// <summary>
        /// Gets a tile using a location
        /// </summary>
        /// <param name="loc">Location</param>
        /// <returns>Tile</returns>
        public Tile GetTileAt(Vector2 loc)
        {
            return GetTileAt((int)loc.X, (int)loc.Y);
        }

        /// <summary>
        /// Gets a tile using a location
        /// </summary>
        /// <param name="x">X coord</param>
        /// <param name="y">y coord</param>
        /// <returns>Tile</returns>
        public Tile GetTileAt(int x, int y)
        {
            return this.Grid[x, y];
        }
    }
}
