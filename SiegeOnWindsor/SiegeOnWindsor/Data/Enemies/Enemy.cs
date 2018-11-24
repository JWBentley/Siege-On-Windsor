using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;
using SiegeOnWindsor.Data.Tiles;
//using SiegeOnWindsor.Data.Enemies.Pathfinding;
using SiegeOnWindsor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Data.Enemies
{
    public abstract class Enemy : IUpdate
    {
        /// <summary>
        /// Health of the enemy
        /// </summary>
        public int Health = 1;

        /// <summary>
        /// Damage the enemy deals
        /// </summary>
        public int Damage = 0;
        /// <summary>
        /// Cooldown between attacks
        /// </summary>
        protected int AttackCooldown = 0;
        /// <summary>
        /// Progress til next attack
        /// </summary>
        protected int AttackProgress = 0;

        /// <summary>
        /// Speed at which the enemy moves
        /// </summary>
        public int Speed = -1;
        /// <summary>
        /// Progress til next movement
        /// </summary>
        protected int MovementProgress = 0;

        /// <summary>
        /// Reference to the world
        /// </summary>
        public World World;
        /// <summary>
        /// Location of the enemy within the world
        /// </summary>
        public Vector2 Location;
        /// <summary>
        /// Path that the enemy will follow
        /// </summary>
        protected Stack<Vector2> Path;

        /// <summary>
        /// Default graphic of the enemy
        /// </summary>
        protected Graphics.Graphics.Graphic graphic;

        public Enemy(World w)
        {
            this.World = w; //Sets world
            this.World.Enemies.Add(this); //Registers enemy
            //Console.WriteLine(this.World.GetCrownLocation().X);
            //Console.WriteLine(this.World.GetCrownLocation().Y);
        }

        public Enemy(Vector2 l)
        {
            this.Location = l; //Sets location
        }

        public Enemy(Graphics.Graphics.Graphic g)
        {
            this.graphic = g; //Sets graphic
        }

        public virtual void Update(GameTime gameTime)
        {
            //Control of movement
            if (this.Speed != -1 && this.CanMove()) //If speed is not -1 and the enemy can move
            {
                this.MovementProgress++; //Progress is incremented

                if (this.MovementProgress == this.Speed) //Full movement cycle completed
                {
                    this.MovementProgress = 0; //Progress reset
                    //Move to next tile is complete
                    Vector2 newLoc = this.Path.Pop(); //Gets next tile location
                    Vector2 oldLoc = this.Location; //Saves old tile location
                    this.Location = newLoc; //Updates enemy location
                    this.World.GetTileAt((int)oldLoc.X, (int)oldLoc.Y).Enemies.Remove(this); //Removes enemy from old tile
                    this.World.GetTileAt((int)newLoc.X, (int)newLoc.Y).Enemies.Add(this); //Adds enemy to new tile
                }
            }
            else if(this.Damage > 0 && this.Path.Count > 0 && this.World.GetTileAt((int)this.Path.Peek().X, (int)this.Path.Peek().Y).Defence != null) //If the enemy is not moving and can attack
            {
                if (this.AttackProgress >= this.AttackCooldown) //Attack cycle completed
                {
                    this.World.GetTileAt((int)this.Path.Peek().X, (int)this.Path.Peek().Y).Defence.DealDamage(this.Damage); //Attacks targeted defence
                    this.AttackProgress = 0; //Resets progress
                }
                else
                    this.AttackProgress++; //Attack progress is incremented
            }

        }

        /// <summary>
        /// Reduces the enemy health by a set amount
        /// </summary>
        /// <param name="damage">Amount of damage</param>
        public void DealDamage(int damage)
        {
            if (this.Health - damage > 0)
                this.Health -= damage; //Health is reduced b damage
            else
                this.Die(); //If health is less than or equal to zero the enemy dies
        }

        /// <summary>
        /// Deletes the enemy from existance
        /// </summary>
        public virtual void Die()
        {
            this.World.GetTileAt(this.Location).Enemies.Remove(this); //Removes from tile
            this.World.Enemies.Remove(this); //Removes from world
        }


        /// <summary>
        /// Determines if the enemy can move
        /// </summary>
        /// <returns>bool: true if it can move, false if not</returns>
        private bool CanMove()
        {
            return this.Path.Count <= 0 ? false : this.World.GetTileAt((int)this.Path.Peek().X, (int)this.Path.Peek().Y).Defence == null; 
            //True is returned of there is a tile to move to in the path and there isn't a defence in the way
        }

        /// <summary>
        /// Updates the path of the enemy
        /// </summary>
        /// <param name="goal">Goal node</param>
        public void UpdatePath(Vector2 goal)
        {
            this.Path = this.World.aStar.Run(this.Location, goal); //Gets new path
            this.Path.Pop(); //removes current location from stack
            //Console.WriteLine("peeking {0},{1}", this.Path.Peek().X, this.Path.Peek().X);
        }

        /// <summary>
        /// Gets the location of the enemy including progress so that it can be correctly rendered on the screen
        /// </summary>
        /// <returns>Vector2 for the location</returns>
        public virtual Vector2 GetActualLocation()
        {
            //Here I had a problem where (progress / speed) returned zero, caused by them being ints FIX: cast to float
            if (this.CanMove()) //Enemy is moving, returns accurate location
                return new Vector2(this.Location.X + (Math.Sign(this.Path.Peek().X - this.Location.X)) * ((float)this.MovementProgress / (float)this.Speed),
                                   this.Location.Y + (Math.Sign(this.Path.Peek().Y - this.Location.Y)) * ((float)this.MovementProgress / (float)this.Speed));
            else
                return this.Location; //Enemy is not moving so the default location is returned
        }

        /// <summary>
        /// Gets the graphic for the enemy in its current state
        /// </summary>
        /// <returns>Texture</returns>
        public virtual Graphics.Graphics.Graphic GetGraphic()
        {
            return this.graphic != null ? this.graphic : Graphics.Graphics.emptyTile;
        }
    }
}
