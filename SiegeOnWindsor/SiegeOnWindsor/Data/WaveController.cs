using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;
using SiegeOnWindsor.Data.Defences;
using SiegeOnWindsor.Data.Enemies;
using SiegeOnWindsor.Data.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data
{
    public class WaveController
    {
        /// <summary>
        /// Reference to the world
        /// </summary>
        private World world;

        /// <summary>
        /// Random object
        /// </summary>
        private Random rand;

        /// <summary>
        /// Current wave number
        /// </summary>
        public int WaveNumber { get; private set; }

        /// <summary>
        /// Resources available for the current wave
        /// </summary>
        private int resources = 0;

        /// <summary>
        /// Queue of enemies to be spawned and where they should spawn
        /// </summary>
        private Queue<EnemySpawn> spawnQueue;

        /// <summary>
        /// If a defence is killing too many enemies it is set as a target
        /// </summary>
        public Defence target; 

        /// <summary>
        /// Cooldown until the next enemy can be spawned
        /// </summary>
        private float spawnCooldown = 1;

        /// <summary>
        /// Determines whether or not a wave is active
        /// </summary>
        public bool IsWaveActive { get; private set; }

        public WaveController(World world)
        {
            this.WaveNumber = 0;
            this.IsWaveActive = false;

            this.world = world;
            this.rand = new Random();
            this.spawnQueue = new Queue<EnemySpawn>();
        }

        /// <summary>
        /// Updates the wave controller
        /// </summary>
        /// <param name="gameTime">Time passed between updates</param>
        public void Update(GameTime gameTime)
        {
            if (this.IsWaveActive)
            {
                //Check if wave should be over
                if (this.spawnQueue.Count == 0 && this.world.Enemies.Count == 0)
                {
                    this.EndWave(); //Ends wave
                }
                else if (this.spawnQueue.Count > 0)
                {
                    //Cooldown / spawn
                    if (this.spawnCooldown <= 0)
                    {
                        EnemySpawn enemySpawn = this.spawnQueue.Dequeue(); //Gets next enemy to spawn
                        ((SpawnTile)this.world.GetTileAt(enemySpawn.SpawnLoc)).SpawnEnemy(enemySpawn.Enemy, this.target != null ? this.target.Tile.Location : this.world.GetCrownLocation()); //Spawns enemy

                        this.spawnCooldown = this.GetSpawnCooldown(this.WaveNumber); //Resets cooldown
                    }
                    else
                        this.spawnCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds; //Decreases delta time from cooldown
                }
            }
        }

        /// <summary>
        /// Starts the next wave
        /// </summary>
        public void StartNextWave()
        {
            this.WaveNumber++; //Increments wave number

            this.resources = GetWaveResources(this.WaveNumber); //Updates resources
            this.spawnCooldown = GetSpawnCooldown(this.WaveNumber); //Updates cooldown

            this.target = null; //Sets target to null

            foreach (Tile tile in this.world.Grid) //Looops through each tile
            {
                if (tile.Defence != null && tile.Defence.Kills > this.world.TotalKills * 0.6) //If the defence has 60% of kills
                    this.target = tile.Defence; //Its is targeted
            }

            //Adds as many powerful enemies as possible to the spawn queue
            foreach (Enemy enemy in Enemy.Types)
            {
                while(this.resources >= enemy.Cost) //Whilst the AI can afford
                {
                    this.resources -= enemy.Cost; //Reduces resources
                    Enemy newEnemy = (Enemy)Activator.CreateInstance(enemy.GetType()); //Creates a new enemy
                    Vector2 loc = this.GetRandomSpawnLoc(); //Location to spawn at
                    this.spawnQueue.Enqueue(new EnemySpawn(loc, newEnemy)); //Creates and adds pair to queue
                }
            }

            this.IsWaveActive = true; //Sets wave to active
        }

        /// <summary>
        /// Ends the current wave
        /// </summary>
        public void EndWave()
        {
            this.IsWaveActive = false; //Sets the wave as over
            this.world.AddMoney(this.GetWaveResources(this.WaveNumber) * 10); //Adds reward for completing a wave
            this.world.UpdateRiskMap(); //Updates risk map
        }


        /// <summary>
        /// Gets the starting resources for a given wave
        /// </summary>
        /// <param name="wave">Wave number</param>
        /// <returns>Amount of resources for that wave</returns>
        private int GetWaveResources(int wave)
        {
            return (int)(100 * Math.Pow(Math.E, 0.1 * (wave-1)));
        }

        /// <summary>
        /// Gets the cooldown for spawning enemies for a given wave
        /// </summary>
        /// <param name="wave">Wave number</param>
        /// <returns>Spawn cooldown</returns>
        private float GetSpawnCooldown(int wave)
        {
            return (5 - (wave / 10)) >= 1 ? (float)(5 - (wave / 10)) : 1;
        }

        /// <summary>
        /// Gets a random spawn location for an enemy
        /// </summary>
        /// <returns></returns>
        private Vector2 GetRandomSpawnLoc()
        {
            switch (rand.Next(4))
            {
                case 0:
                    return new Vector2(rand.Next(this.world.Grid.GetLength(0)), 0); //TOP
                case 1:
                    return new Vector2(this.world.Grid.GetLength(0) - 1, rand.Next(this.world.Grid.GetLength(1))); //RIGHT
                case 2:
                    return new Vector2(rand.Next(this.world.Grid.GetLength(0)), this.world.Grid.GetLength(1) - 1); //Bottom
                case 3:
                    return new Vector2(0, rand.Next(this.world.Grid.GetLength(1))); //LEFT

                default:
                    return new Vector2(0, 0);
            }
        }

        /// <summary>
        /// Gets wave number as text
        /// </summary>
        /// <returns>A string</returns>
        public string GetWaveNumberText()
        {
            return "Wave - " + this.WaveNumber.ToString();
        }

        /// <summary>
        /// Structure for an enemy and spawn location pair
        /// </summary>
        public struct EnemySpawn
        {
            public Vector2 SpawnLoc;
            public Enemy Enemy;

            public EnemySpawn(Vector2 spawnLoc, Enemy enemy)
            {
                this.SpawnLoc = spawnLoc;
                this.Enemy = enemy;
            }
        }
    }
}
